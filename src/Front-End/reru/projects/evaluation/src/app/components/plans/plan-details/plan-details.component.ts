import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { Plan } from '../../../utils/models/plans/plan.model';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { forkJoin } from 'rxjs';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-plan-details',
  templateUrl: './plan-details.component.html',
  styleUrls: ['./plan-details.component.scss']
})
export class PlanDetailsComponent implements OnInit {
  isLoading: boolean = true;
  plan: Plan = new Plan();

  title: string;
  description: string;
  no: string;
  yes: string;

  constructor(private planService: PlanService,
              private route: ActivatedRoute,
		          public translate: I18nService,
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
    forkJoin([
			this.translate.get('plans.remove'),
			this.translate.get('plans.remove-msg'),
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
   this.planService.delete(id).subscribe(() => {
    forkJoin([
      this.translate.get('modal.success'),
      this.translate.get('plans.succes-remove-msg'),
    ]).subscribe(([title, description]) => {
      this.title = title;
      this.description = description;
      });
     this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
     this.router.navigate(['plans']);
   })
 }
}

