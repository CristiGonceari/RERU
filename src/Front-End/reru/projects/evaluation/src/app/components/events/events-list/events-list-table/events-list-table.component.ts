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
	isLoadingCalendar: boolean = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];

	displayMonth: string;
	displayYear: number;
	displayDate;

	countedEvents;
	fromDate;
	tillDate;

	dateTimeFrom: string;
	dateTimeTo: string;
	searchFrom: string;
	searchTo: string;
	filters: any = {};

	selectedDay;
	countedPlans;

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
	}

	setTimeToSearch(): void {
		if (this.dateTimeFrom) {
			const date = new Date(this.dateTimeFrom);
			this.searchFrom = new Date(date.getTime() - (new Date(this.dateTimeFrom).getTimezoneOffset() * 60000)).toISOString();
		} else if (this.dateTimeTo) {
			const date = new Date(this.dateTimeTo);
			this.searchTo = new Date(date.getTime() - (new Date(this.dateTimeTo).getTimezoneOffset() * 60000)).toISOString();
		}
	}

	getFilteredEvents(data: any = {}): void {
		this.setTimeToSearch();

		let params = {
			fromDate: this.searchFrom,
			tillDate: this.searchTo,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10
		}
		if (this.searchFrom != null || this.searchTo != null) {
			this.eventService.getEvents(params).subscribe(res => {
				if (res && res.data) {
					this.fromDate = res.data.fromDate;
					this.tillDate = res.data.tillDate;
					this.events = res.data.items || [];
					this.pagination = res.data.pagedSummary;
				}
			})
		}
	}

	clearFields() {

		this.dateTimeFrom = '';
		this.dateTimeTo = '';
		this.searchFrom = '';
		this.searchTo = '';

		this.getListByDate();
	}

	list(data: any = {}) {
		this.selectedDay = null;
    	this.isLoading = true;

		if (data.fromDate != null && data.tillDate != null) {
			this.tillDate = data.tillDate,
				this.fromDate = data.fromDate
		}
		if (data.displayMonth != null && data.displayYear != null) {
			this.displayMonth = data.displayMonth;
			this.displayYear = data.displayYear;
		}

		let params = {
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10,
			fromDate: this.parseDates(data.fromDate),
			tillDate: this.parseDates(data.tillDate)
		}

		this.service.getEvents(params).subscribe(
			res => {
				if (res && res.data) {
					this.events = res.data.items;
					this.pagination = res.data.pagedSummary;

					this.isLoading = false;
					this.selectedDay = null;
				}
			}
		)
	}

	getListByDate(data: any = {}): void {
    this.isLoading = true;

		if (data.date != null) {
			this.selectedDay = this.parseDates(data.date);
			this.displayDate = this.parseDatesForTable(data.date)
		}

		const request = {
			date: this.selectedDay,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize
		}

		this.service.getEventByDate(request).subscribe(response => {
			if (response.success) {
				this.events = response.data.items || [];
				this.pagination = response.data.pagedSummary;

				this.isLoading = false;
			}
		});
	}

	parseDates(date) {
		const day = date && date.getDate() || -1;
		const dayWithZero = day.toString().length > 1 ? day : '0' + day;
		const month = date && date.getMonth() + 1 || -1;
		const monthWithZero = month.toString().length > 1 ? month : '0' + month;
		const year = date && date.getFullYear() || -1;

		return `${year}-${monthWithZero}-${dayWithZero}`;
	}

	parseDatesForTable(date) {
		const day = date && date.getDate() || -1;
		const dayWithZero = day.toString().length > 1 ? day : '0' + day;
		const month = date && date.getMonth() + 1 || -1;
		const monthWithZero = month.toString().length > 1 ? month : '0' + month;
		const year = date && date.getFullYear() || -1;

		return `${dayWithZero}/${monthWithZero}/${year}`;
	}

	getListOfCoutedEvents(data) {
		const request = {
			fromDate: this.parseDates(data.fromDate),
			tillDate: this.parseDates(data.tillDate)
		}
		this.service.getEventCount(request).subscribe(response => {
			if (response.success) {
				this.countedPlans = response.data;

				for (let calendar of data.calendar) {
					let data = new Date(calendar.date);

					for (let values of response.data) {
						let c = new Date(values.date);
						let compararea = +data == +c;

						if (compararea) {
							calendar.count = values.count;
						}
					}
				}
			}
		})
	}

	openDeleteModal(id) {
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
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.delete(id), () => { });
	}

	delete(id) {
		this.isLoadingCalendar = false;
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
			this.isLoadingCalendar = true;
		})
	}

	navigate(id) {
		this.router.navigate(['event/', id, 'overview'], { relativeTo: this.route });
	}

	getHeaders(name: string): void {
		this.translateData();
		let eventsTable = document.getElementById('eventsTable')
		let headersHtml = eventsTable.getElementsByTagName('th');
		let headersDto = ['name', 'description', 'fromDate', 'tillDate'];
		for (let i = 0; i < headersHtml.length - 1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			fromDate: this.searchFrom || null,
			tillDate: this.searchTo || null
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
				for (let i = 0; i < this.printTranslates.length; i++) {
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
