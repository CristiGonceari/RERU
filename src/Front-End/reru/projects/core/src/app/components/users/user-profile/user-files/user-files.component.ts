import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent, UploadFileModalComponent, PrintModalComponent } from '@erp/shared';
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
  no: string;
  yes: string;
	pagination: PaginationSummary = new PaginationSummary();

  headersToPrint = [];
	printTranslates: any[];
	canDownloadFile: boolean = false;

  title: string;
  description: string;

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
    this.detectUserId();
  }

  detectUserId(){
    this.userId = this.router.url.split('/')[2];
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
    forkJoin([
      this.translate.get('files.delete-document'),
      this.translate.get('files.remove-msg'),
      this.translate.get('button.no'),
      this.translate.get('button.yes'),
    ]).subscribe(([docTitle, docDescription, no, yes]) => {
      this.docTitle = docTitle;
      this.docDescription = docDescription;
      this.no = no;
      this.yes = yes;
    })
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = this.docTitle;
		modalRef.componentInstance.description = this.docDescription;
    modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(() => this.removeFile(id), () => {});
  }

  removeFile(id): void {
    forkJoin([
      this.translate.get('notification.title.success'),
			this.translate.get('notification.body.success.file-deleted')
    ]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
    });

    this.isLoading = true;
    this.userFilesService.delete(id).subscribe(() => {
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultConfig());
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
      formData.append('File.File', myFile);
      formData.append('File.Type', '5');
      this.myFiles.addFile(formData).subscribe(res => {
        if (res) this.subsribeForParams();
      })
    }
    forkJoin([
      this.translate.get('notification.title.success'),
			this.translate.get('files.success-add-doc')
    ]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
    });
    this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
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

    forkJoin([
      this.translate.get('notification.title.success'),
			this.translate.get('files.success-add-doc')
    ]).subscribe(([title, description]) => {
			this.title = title;
			this.description = description;
    });
    this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
  }

  getTitle(): string {
    return document.getElementById('title').innerHTML;
  }

  getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = [ '-', 'name'];
         
		for (let i = 1; i <= headersHtml.length - 2; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}

		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2
		};

		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format', 'max-print-rows']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
      this.translate.get('print.select-file-format'),
      this.translate.get('print.max-print-rows')
		]).subscribe(
			(items) => {
				for (let i = 0; i < this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
    let parameters = {
      fields : data.fields,
      orientation: data.orientation,
      tableExportFormat: data.tableExportFormat,
      tableName: data.tableName,
      userProfileId: this.userId
    }
		this.canDownloadFile = true;
    
    if(this.router.url.includes('/my-documents')){
      this.myFiles.print(data).subscribe(response => {
        if (response) {
          const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
          const blob = new Blob([response.body], { type: response.body.type });
          const file = new File([blob], data.tableName.trim(), { type: response.body.type });
          saveAs(file);
          this.canDownloadFile = false;
        }
      }, () => this.canDownloadFile = false);
    }else{
      this.userFilesService.print(parameters).subscribe(response => {
        if (response) {
          const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
          const blob = new Blob([response.body], { type: response.body.type });
          const file = new File([blob], data.tableName.trim(), { type: response.body.type });
          saveAs(file);
          this.canDownloadFile = false;
        }
      }, () => this.canDownloadFile = false);
    }
	}
}
