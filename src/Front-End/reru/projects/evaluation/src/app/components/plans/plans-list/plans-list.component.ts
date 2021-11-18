import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from 'projects/erp-shared/src/lib/modals/confirm-modal/confirm-modal.component';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-plans-list',
  templateUrl: './plans-list.component.html',
  styleUrls: ['./plans-list.component.scss']
})
export class PlansListComponent implements OnInit {

  isLoading: boolean = true;
  plans: any[] = [];
  pagination: PaginationModel = new PaginationModel();

  constructor(private planService: PlanService,
              private router: Router,
              private route: ActivatedRoute,
              private modalService: NgbModal,
		          private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data :any = {}): void {
    this.isLoading = true;
    const request = {
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize 
    }

    this.planService.list(request).subscribe(response => {
      if (response.success) {
        this.plans = response.data.items || [];
        this.pagination = response.data.pagedSummary;
        this.isLoading = false;
      }
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
     this.list();
   })
 }

  navigate(id) {
		this.router.navigate(['plan/', id, 'overview'], { relativeTo: this.route });
	}

}
