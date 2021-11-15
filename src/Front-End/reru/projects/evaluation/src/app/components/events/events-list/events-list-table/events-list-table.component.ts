import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '../../../../../../../erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { EventService } from 'projects/evaluation/src/app/utils/services/event/event.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

@Component({
  selector: 'app-events-list-table',
  templateUrl: './events-list-table.component.html',
  styleUrls: ['./events-list-table.component.scss']
})
export class EventsListTableComponent implements OnInit {
  events: Event;
	pagination: PaginationModel = new PaginationModel();
	isLoading: boolean = true;

	constructor(
		private service: EventService, 
		private router: Router, 
		private route: ActivatedRoute,
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
			itemsPerPage: Number(this.pagination?.pageSize || 10)
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
		 const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true});
		 modalRef.componentInstance.title = "Delete";
		 modalRef.componentInstance.description= "Do you whant to delete this event ?"
		 modalRef.result.then(() => this.delete(id), () => {});
	}

	delete(id){
		this.eventService.deleteEvent(id).subscribe(() => {
			this.notificationService.success('Success', 'Event was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.list();
		})
	}
	
	navigate(id) {
		this.router.navigate(['event/', id, 'overview'], { relativeTo: this.route });
	}
}
