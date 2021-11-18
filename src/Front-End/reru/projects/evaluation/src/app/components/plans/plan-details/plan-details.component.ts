import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { Plan } from '../../../utils/models/plans/plan.model';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-plan-details',
  templateUrl: './plan-details.component.html',
  styleUrls: ['./plan-details.component.scss']
})
export class PlanDetailsComponent implements OnInit {
  isLoading: boolean = true;
  plan: Plan = new Plan();

  constructor(private planService: PlanService,
              private route: ActivatedRoute,
              private modalService: NgbModal,
		          private notificationService: NotificationsService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrievePlan(response.id);
      }
    })
  }

  retrievePlan(id: number): void {
    this.planService.get(id).subscribe(response => {
      this.plan = response.data;
      this.isLoading = false;
    });
  } 
  
  openDeleteModal(id){
    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true});
    modalRef.componentInstance.title = "Delete";
    modalRef.componentInstance.description= "Do you whant to delete this plan ?"
    modalRef.result.then(() => this.delete(id), () => {});
 }

 delete(id){
   this.planService.delete(id).subscribe(() => {
     this.notificationService.success('Success', 'Event was successfully deleted', NotificationUtil.getDefaultMidConfig());
     this.router.navigate(['plans']);
   })
 }
}

