import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { FileService } from 'projects/personal/src/app/utils/services/file.service';
import { AddDocumentModalComponent } from 'projects/personal/src/app/utils/modals/add-document-modal/add-document-modal.component';
import { saveAs } from 'file-saver';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { ConfirmationDeleteDocumentComponent } from 'projects/personal/src/app/utils/modals/confirmation-delete-document/confirmation-delete-document.component';
import { I18nService } from 'projects/personal/src/app/utils/services/i18n.service';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { InitializerUserProfileService } from 'projects/personal/src/app/utils/services/initializer-user-profile.service';

@Component({
  selector: 'app-documents-table',
  templateUrl: './documents-table.component.html',
  styleUrls: ['./documents-table.component.scss']
})
export class DocumentsTableComponent implements OnInit {
  
  hasContractorId: number;

  @Input() contractor: Contractor;
  @Output() update: EventEmitter<void> = new EventEmitter<void>();
  documents: any[] = [];
  pagedSummary: PagedSummary = new PagedSummary();
  isLoading: boolean;
  selectedType: number = 4;
  constructor(private fileService: FileService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private initializerUserProfileService: InitializerUserProfileService,
              private contractorService: ContractorService,
              private translate: I18nService) { }

  ngOnInit(): void {
    this.retrieveDocuments();
    this.getUserContractorId();
  }

  getUserContractorId(){
    this.initializerUserProfileService.get().subscribe(res => {
      if(res.data) {
        this.hasContractorId = res.data.contractorId;
      }
    });
  }

  retrieveDocuments(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      contractorId: +this.contractor.id,
      type: this.selectedType == 0 ? null : this.selectedType || 4,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    });
    this.contractorService.getDocuments(request).subscribe(response => {
      this.documents = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
    })
  }

  downloadFile(item): void {
    this.fileService.get(item.id).subscribe(response => {
      const fileName = item.name;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  openDeleteDocumentModal(document: {name: string, id: number}): void {
    const modalRef = this.modalService.open(ConfirmationDeleteDocumentComponent);
    modalRef.componentInstance.name = document.name;
    modalRef.result.then(response => this.removeFile(document.id), () => {});
  }

  removeFile(id: number): void {
    this.isLoading = true;
    this.fileService.delete(id).subscribe(response => {
      this.notificationService.success('Success', 'Fișier șters!', NotificationUtil.getDefaultConfig());
      this.contractorService.fetchContractor.next();
      this.isLoading = false;
    });
  }

  upload(): void {
    const modalRef = this.modalService.open(AddDocumentModalComponent);
    modalRef.result.then(response => this.uploadTestFile(response), () => {});
  }

  uploadTestFile(data): void {
    this.isLoading = true;
    const request = new FormData();
    request.append('File', data.file);
    this.fileService.upload(request).subscribe(response => {
      this.notificationService.success('Success', 'PDF adăugat!', NotificationUtil.getDefaultConfig());
      const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
      const blob = new Blob([response.body], { type: 'application/pdf' });
			const file = new File([blob], fileName, { type: 'application/pdf' });
      saveAs(file);
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      if (error.status === 400) {
        error.messages.map(el => {
          if (el.code === '02003004') {
            this.translate.get(`validations.${el.code}`).subscribe(errorText => {
              this.notificationService.warn('Atenție', errorText, NotificationUtil.getDefaultConfig());
            })
          }
        });
        return;
      }
    });
  }

  openDocumentModal(): void {
    const modalRef = this.modalService.open(AddDocumentModalComponent);
    modalRef.result.then(response => this.uploadFile(response), () => {});
  }

  uploadFile(data): void {
    this.isLoading = true;
    const request = new FormData();
    request.append('contractorId', `${this.contractor.id}`);
    request.append('File', data.file);
    request.append('Type', '1');
    this.fileService.create(request).subscribe(response => {
      this.notificationService.success('Success', 'Fișier adăugat!', NotificationUtil.getDefaultConfig());
      this.contractorService.fetchContractor.next();
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      if (error.status === 400) {
        error.messages.map(el => {
          if (el.code === '02003004') {
            this.translate.get(`validations.${el.code}`).subscribe(errorText => {
              this.notificationService.warn('Atenție', errorText, NotificationUtil.getDefaultConfig());
            })
          }
        });
        return;
      }
    });
  }
}
