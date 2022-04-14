import { Component, OnInit } from '@angular/core';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DocumentTemplateService } from '../../../utils/services/document-template/document-template.service';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';
import { ConfirmModalComponent } from '@erp/shared';


@Component({
  selector: 'app-document-templates-table',
  templateUrl: './document-templates-table.component.html',
  styleUrls: ['./document-templates-table.component.scss']
})
export class DocumentTemplatesTableComponent implements OnInit {
  isLoading: boolean = true;
  documents: any[] = [];
  editDocument: string[];

  title: string;
	description: string;
  no: string;
	yes: string;

  pagedSummary: PaginationModel = new PaginationModel();
  
  constructor(private documentService: DocumentTemplateService,
    private modalService: NgbModal,
		public translate: I18nService,
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

  deleteDocument(id: number): void {
    this.documentService.delete(id).subscribe(() => {
      this.getList();
      this.notificationService.success('Success', 'Document deleted!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'There was an error deleting the document!', NotificationUtil.getDefaultConfig());
    })
  }

	openConfirmationDeleteModal(id): void {
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('document-test-modal.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
      this.yes = yes;
      this.no = no;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteDocument(id), () => { });
	}
}


