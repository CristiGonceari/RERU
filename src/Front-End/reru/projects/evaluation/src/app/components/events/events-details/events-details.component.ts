import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { EventService } from '../../../utils/services/event/event.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
  selector: 'app-events-details',
  templateUrl: './events-details.component.html',
  styleUrls: ['./events-details.component.scss']
})
export class EventsDetailsComponent implements OnInit {
  eventId: number;
  eventName: string;
  isLoading: boolean = true;
  title: string;
	description: string;
	no: string;
	yes: string;

  constructor(
		private service: EventService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
    public router: Router,
		private modalService: NgbModal,
		private eventService: EventService,
		private notificationService: NotificationsService
  ) {  }
  
  ngOnInit(): void {
    this.subsribeForParams();
  }

  getEvent(){
    this.service.getEvent(this.eventId).subscribe(res => {
      if (res && res.data) {
        this.eventName = res.data.name;
        this.isLoading = false;
      }
    })
  }
  
  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.eventId = params.id;
			if (this.eventId) {
        this.getEvent();
    }});
	}

  openDeleteModal(eventId){
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
    modalRef.result.then(() => this.delete(eventId), () => {});
 }

 delete(eventId){
   this.eventService.deleteEvent(this.eventId).subscribe(() => {
    forkJoin([
      this.translate.get('modal.success'),
      this.translate.get('events.succes-remove-event-msg'),
      ]).subscribe(([title, description]) => {
      this.title = title;
      this.description = description;
      });
     this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
     this.router.navigate(['events']);
   })
 }
}
