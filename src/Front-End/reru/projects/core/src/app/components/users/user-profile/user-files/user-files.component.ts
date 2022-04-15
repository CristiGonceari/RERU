import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent, UploadFileModalComponent } from '@erp/shared';
import { PaginationSummary } from 'projects/core/src/app/utils/models/pagination-summary.model';
import { UserFilesModel } from 'projects/core/src/app/utils/models/user-files.model';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/core/src/app/utils/util/object.util';
import { UserFilesService } from '../../../../utils/services/user-files.service';
import { saveAs } from 'file-saver';

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
	pagination: PaginationSummary = new PaginationSummary();

  constructor(
    private userFilesService: UserFilesService,
		private activatedRoute: ActivatedRoute,
    private notificationService: NotificationsService,
    private modalService: NgbModal,
  ) { }

  ngOnInit(): void {
		this.subsribeForParams();
  }

  subsribeForParams() {
		this.isLoading = true;
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.userId = params.id;
				this.getUserFiles();
			}
		});
	}

  getUserFiles(data: any = {}): void {
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
    this.userFilesService.get(item).subscribe(response => {
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
      this.getUserFiles();
      this.isLoading = false;
    });
  }

  openUploadDocumentModal(): void {
  	const modalRef: any = this.modalService.open(UploadFileModalComponent, { centered: true });
    modalRef.result.then(() => this.uploadDocument(modalRef.result.__zone_symbol__value), () => {});
  }

  uploadDocument(file): void {
    this.files = file;
    for(let userFile of this.files){
      let formData = new FormData();
      formData.append('Data.UserId', this.userId);
      formData.append('Data.File.File', userFile);
      formData.append('Data.File.Type', '5');
      this.userFilesService.create(formData).subscribe(res => {
        if (res) this.getUserFiles();
      })
    }
  }
}
