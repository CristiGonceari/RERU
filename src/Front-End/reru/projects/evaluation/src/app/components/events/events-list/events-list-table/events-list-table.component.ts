import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '../../../../../../../erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { PrintModalComponent } from '@erp/shared';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-events-list-table',
  templateUrl: './events-list-table.component.html',
  styleUrls: ['./events-list-table.component.scss']
})
export class EventsListTableComponent implements OnInit {
  events: Event;
	pagination: PaginationModel = new PaginationModel();
	isLoading: boolean = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];

	constructor(
		private service: EventService, 
		private router: Router, 
		private route: ActivatedRoute,
		public translate: I18nService,
		private modalService: NgbModal,
		private eventService: EventService,
		private notificationService: NotificationsService
		) { }

	ngOnInit(): void {
		this.list();
	}
     
	list(data: any = {}) {
		let params = {
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10
		}

		this.service.getEvents(params).subscribe(
			res => {
				if (res && res.data) {
					this.events = res.data.items;
					this.pagination = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

	openDeleteModal(id){
		forkJoin([
			this.translate.get('events.remove'),
			this.translate.get('events.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		 const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true});
		 modalRef.componentInstance.title = this.title;
		 modalRef.componentInstance.description = this.description;
		 modalRef.componentInstance.buttonNo = this.no;
		 modalRef.componentInstance.buttonYes = this.yes;
		 modalRef.result.then(() => this.delete(id), () => {});
	}

	delete(id){
		this.eventService.deleteEvent(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('events.succes-remove-event-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		})
	}
	
	navigate(id) {
		this.router.navigate(['event/', id, 'overview'], { relativeTo: this.route });
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'description', 'fromDate', 'tillDate'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2
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
		this.eventService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}
}
