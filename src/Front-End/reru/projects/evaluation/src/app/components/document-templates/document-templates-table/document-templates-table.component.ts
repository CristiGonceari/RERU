import { Component, OnInit } from '@angular/core';
import { ObjectUtil } from '../../../utils/util/object.util';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DocumentTemplateService } from '../../../utils/services/document-template/document-template.service';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';
import { ConfirmModalComponent, PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-document-templates-table',
  templateUrl: './document-templates-table.component.html',
  styleUrls: ['./document-templates-table.component.scss']
})
export class DocumentTemplatesTableComponent implements OnInit {
  isLoading: boolean = true;
  documents: any[] = [];
  editDocument: string[];

  documentName: string;
	fileType: string = '';
	filters: any = {}
	pagination: PaginationModel = new PaginationModel();

  title: string;
	description: string;
  no: string;
	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
  pagedSummary: PaginationModel = new PaginationModel();
  
  constructor(private documentService: DocumentTemplateService,
    private modalService: NgbModal,
		public translate: I18nService,
    private notificationService: NotificationsService,) { }

  ngOnInit(): void {
    this.getList()
  }
  getHeaders(name: string, documentType: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'fileType'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
      documentType: documentType,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.filters.name || this.documentName || '',
			fileType: this.filters.fileType || 0
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
		]).subscribe(
			(items) => {
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.documentService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

  getList(data :any = {}): void {
    this.isLoading = true;

		let params = {
			name: this.documentName || '',
			fileType: this.fileType || '',
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
			...this.filters
		}

    this.documentService.list(params).subscribe(response => {
      if (response.success) {
        this.documents = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  resetFilters(): void {
		this.filters = {};
		this.fileType = '';
		this.pagination.currentPage = 1;
		this.getList();
	}

  setFilter(field: string, value): void {
		this.filters[field] = value;
		this.fileType = this.filters.fileType;
		this.pagination.currentPage = 1;
		this.getList();
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


