import { Component, OnInit } from '@angular/core';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { DocumentsTemplateService } from '../../../utils/services/documents-template.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ConfirmModalComponent } from '../../../utils/modals/confirm-modal/confirm-modal.component';
import { DeleteDepartmentModalComponent } from '../../../utils/modals/delete-department-modal/delete-department-modal.component';
import { DeleteDocumentsTemplatesModalComponent } from '../../../utils/modals/delete-documents-templates-modal/delete-documents-templates-modal.component';


@Component({
  selector: 'app-documents-templates-table',
  templateUrl: './documents-templates-table.component.html',
  styleUrls: ['./documents-templates-table.component.scss']
})
export class DocumentsTemplatesTableComponent implements OnInit {
  isLoading: boolean = true;
  documents: any[] = [];
  editDocument: string[];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  constructor(private documentService: DocumentsTemplateService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,) { }

  ngOnInit(): void {
    this.getList()
  }
  
  getList(data :any = {}): void {
    this.isLoading = true;
    const request= ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize
    })
    this.documentService.list(request).subscribe(response => {
      if (response.success) {
        this.documents = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openDocumentDeleteModal(id: number) {
    this.documentService.getById(id).subscribe((res) => {
      const modalRef = this.modalService.open(DeleteDocumentsTemplatesModalComponent, { centered: true });
      modalRef.componentInstance.name = res.data.name;
      modalRef.result.then(() => this.deleteDocument(id), () => {});
    } )
  }

  deleteDocument(id: number): void {
    this.documentService.delete(id).subscribe(() => {
      this.getList();
      this.notificationService.success('Success', 'Document deleted!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'There was an error deleting the document!', NotificationUtil.getDefaultConfig());
    })
  }
}


