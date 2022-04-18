import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent, UploadFileModalComponent } from '@erp/shared';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { UserFilesModel } from 'projects/core/src/app/utils/models/user-files.model';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/core/src/app/utils/util/object.util';
import { UserFilesService } from '../../../../utils/services/user-files.service';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/core/src/app/utils/services/i18n.service';
import { MyProfileService } from 'projects/core/src/app/utils/services/my-profile.service';

@Component({
  selector: 'app-user-files',
  templateUrl: './user-files.component.html',
  styleUrls: ['./user-files.component.scss']
})
export class UserFilesComponent implements OnInit {

  isLoading = false;
	userId: string;
	fileId: string;
  userFiles: UserFilesModel[] = [];
  files: any;
  docTitle: string;
  docDescription: string;
	pagination: PaginationSummary = new PaginationSummary();

  constructor(
    private userFilesService: UserFilesService,
    private myFiles: MyProfileService, 
		private activatedRoute: ActivatedRoute,
		private router: Router,
    private notificationService: NotificationsService,
    private modalService: NgbModal,
		public translate: I18nService,
  ) { }

  ngOnInit(): void {
		this.isLoading = true;
    this.subsribeForParams();
  }
  
  subsribeForParams(data: any = {}) {
    if (this.router.url.includes('/my-documents')) {
      this.getMyFiles(data);
    } else {
      this.activatedRoute.parent.params.subscribe(params => {
        if (params.id) {
          this.userId = params.id;
          this.getUserFiles(data);
        }
      });
    }
	}

  getMyFiles(data): void {
    const request = ObjectUtil.preParseObject({
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
    });

    this.myFiles.getFiles(request).subscribe(response => {
      if(response) {
        this.pagination = response.data.pagedSummary;
        this.userFiles = response.data.items;
        this.isLoading = false;
      }
    })
  }

  getUserFiles(data): void {
    const request = ObjectUtil.preParseObject({
      userId: +this.userId,
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
    });

    this.userFilesService.getList(request).subscribe(response => {
      if(response) {
        this.pagination = response.data.pagedSummary;
        this.userFiles = response.data.items;
        this.isLoading = false;
      }
    })
  }

  downloadFile(item): void {
    this.userFilesService.get(item.fileId).subscribe(response => {
      const fileName = item.name;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  openDeleteDocumentModal(id: string): void {
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = "Sterge document";
		modalRef.componentInstance.description = "Sigur doriti sa stergeti acest document?";
    modalRef.componentInstance.buttonNo = "Nu";
		modalRef.componentInstance.buttonYes = "Da";
    modalRef.result.then(() => this.removeFile(id), () => {});
  }

  removeFile(id): void {
    this.isLoading = true;
    this.userFilesService.delete(id).subscribe(() => {
      this.notificationService.success('Success', 'Fișier șters!', NotificationUtil.getDefaultConfig());
      this.subsribeForParams();
      this.isLoading = false;
    });
  }

  openUploadDocumentModal(): void {
    forkJoin([
      this.translate.get('files.modal-title'),
			this.translate.get('files.modal-description')
    ]).subscribe(([title, description]) => {
			this.docTitle = title as string;
			this.docDescription = description as string;
    });

  	const modalRef: any = this.modalService.open(UploadFileModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.title = this.docTitle;
		modalRef.componentInstance.description = this.docDescription;
    modalRef.result.then(() => this.checkUser(modalRef.result.__zone_symbol__value), () => {});
  }

  checkUser(files): void {
    if (this.router.url.includes('/my-documents')) {
      this.uploadMyDocs(files);
    } else {
      this.uploadDocuments(files);
    }
  }

  uploadMyDocs(files): void {
    for(let myFile of files) {
      let formData = new FormData();
      formData.append('Data.File.File', myFile);
      formData.append('Data.File.Type', '5');
      this.myFiles.addFile(formData).subscribe(res => {
        if (res) this.subsribeForParams();
      })
    }
  }

  uploadDocuments(files): void {
    for(let userFile of files) {
      let formData = new FormData();
      formData.append('Data.UserId', this.userId);
      formData.append('Data.File.File', userFile);
      formData.append('Data.File.Type', '5');
      this.userFilesService.create(formData).subscribe(res => {
        if (res) this.subsribeForParams();
      })
    }
  }
}
