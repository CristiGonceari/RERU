import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { EventService } from '../../../utils/services/event/event.service';
import { NotificationUtil } from '../../../utils/util/notification.util';


@Component({
  selector: 'app-events-details',
  templateUrl: './events-details.component.html',
  styleUrls: ['./events-details.component.scss']
})
export class EventsDetailsComponent implements OnInit {
  id: number;
  name: string;
  isLoading: boolean = true;

  constructor(
		private service: EventService,
		private activatedRoute: ActivatedRoute,
    public router: Router,
		private modalService: NgbModal,
		private eventService: EventService,
		private notificationService: NotificationsService
  ) {  }
  
  ngOnInit(): void {
    this.subsribeForParams();
  }

  get(){
    this.service.getDetailsEvent(this.id).subscribe(res => {
      if (res) {
        this.name = res.name;
        this.isLoading = false;
      }
    })
  }
  
  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.id = params.id;
			if (this.id) {
        this.get();
    }});
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
     this.router.navigate(['events']);
   })
 }
}
