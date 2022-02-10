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

	displayMonth: string;
	displayYear: number;
	displayDate;

	countedEvents;
	fromDate;
	tillDate;

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
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize || 10
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

		if (data.date != null) {
		  this.selectedDay = this.parseDates(data.date);
		  this.displayDate = this.parseDatesForTable(data.date)
		}
	
		this.isLoading = true;
	
		const request = {
		  date:  this.selectedDay,
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
}
