import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent, PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { CandidatePositionModel } from '../../utils/models/candidate-position.model';
import { CandidatePositionService } from '../../utils/services/candidate-position/candidate-position.service';
import { NotificationUtil } from '../../utils/util/notification.util';
import { I18nService } from '../../utils/services/i18n/i18n.service';
import { PaginationModel } from '../../utils/models/pagination.model';
import { saveAs } from 'file-saver';

@Component({
	selector: 'app-positions',
	templateUrl: './positions.component.html',
	styleUrls: ['./positions.component.scss']
})
export class PositionsComponent implements OnInit {
	isLoading = true;
	positions: CandidatePositionModel[];
	pagination: PaginationModel = new PaginationModel();
	title: string;
	documentTitle: string;
	description: string;
	no: string;
	yes: string;
	keyword: string;

	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	
	constructor(
		private positionService: CandidatePositionService,
		public translate: I18nService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
	) { }

	ngOnInit(): void {
		this.getPositions();
	}
	
	getTitle(): string {
		this.documentTitle = document.getElementById('documentTitle').innerHTML;
		return this.documentTitle
	}

	getPositions(data: any = {}): void {
		this.keyword = data.name;
		let params: any = {
			name: this.keyword || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		};
		this.list(params);
	}

	list(params): void {
		console.warn('params', params);
		
		this.positionService.getList(params).subscribe(res => {
			if (res && res.data) {
				this.positions = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'isActive'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.keyword || ''
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
		this.positionService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	openRemoveModal(id: number, name: string): void {
		forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('position.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
		});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = `${this.description} (${name})?`;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.removePosition(id, name), () => { });
	}

	removePosition(id: number, name: string): void {
		this.positionService.delete(id).subscribe(res => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('position.success-delete'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, `${this.description} ${name}`, NotificationUtil.getDefaultMidConfig(),
				this.getPositions()
			);
		},
			() => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

}
