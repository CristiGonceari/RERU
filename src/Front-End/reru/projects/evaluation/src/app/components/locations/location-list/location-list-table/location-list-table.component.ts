import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { TestingLocationTypeEnum } from 'projects/evaluation/src/app/utils/enums/testing-location-type.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { LocationService } from '../../../../utils/services/location/location.service';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-location-list-table',
  templateUrl: './location-list-table.component.html',
  styleUrls: ['./location-list-table.component.scss']
})
export class LocationListTableComponent implements OnInit {

  	locations = [];
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	enum = TestingLocationTypeEnum;
	isLoading: boolean = true;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
  
	title: string;
	description: string;
	no: string;
	yes: string;

  	constructor(
    	private locationService: LocationService,
		public translate: I18nService,
    	private router: Router, 
    	private route: ActivatedRoute,
		private notificationService: NotificationsService,
		private modalService: NgbModal,
  	) { }

 	 ngOnInit(): void {
		this.list();
  	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'address', 'type', 'places'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.keyword || ''
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'xl' });
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
		this.locationService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(1).slice(0, -1);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

  	list(data: any = {}) {
		this.keyword = data.keyword;
		let params = {
			name: this.keyword || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}
		this.locationService.getLocations(params).subscribe( res => {
			if(res && res.data) {
				this.locations = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	navigate(id: number) {
		this.router.navigate(['location/', id, 'overview'], {relativeTo: this.route});
	}

	deleteLocation(id: number): void{
		this.locationService.deleteLocation(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('locations.succes-remove-location-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id: number): void {
		forkJoin([
			this.translate.get('locations.remove'),
			this.translate.get('locations.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteLocation(id), () => { });
	}
}
