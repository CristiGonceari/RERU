import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteDepartmentModalComponent } from '../../../utils/modals/delete-department-modal/delete-department-modal.component';
import { DepartmentContentModalComponent } from '../../../utils/modals/department-content-modal/department-content-modal.component';
import { DepartmentModel } from '../../../utils/models/department.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DepartmentContentService } from '../../../utils/services/department-content.service';
import { forkJoin } from 'rxjs';
import { DeleteDepartmentContentModalComponent } from '../../../utils/modals/delete-department-content-modal/delete-department-content-modal.component';
import { DepartmentContentTypeEnum } from '../../../utils/models/department-content.model';
import { el } from 'date-fns/locale';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  department: DepartmentModel;
  template: any;
  calculated: any;
  summary: any = {};
  contentType = DepartmentContentTypeEnum;
  constructor(private departmentService: DepartmentService,
              private route: ActivatedRoute,
              private router: Router,
              private ngZone: NgZone,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private departmentContentService: DepartmentContentService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id && !isNaN(response.id)) {
        this.retrieveDepartment(response.id);
      } else {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      }
    });
  }

  retrieveDepartment(id: number): void {
    this.departmentService.get(id).subscribe(response => {
      if (!response) {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        return;
      }
      this.department = response.data;
      this.retrieveDepartmentContent(this.department.id);
    });
  }

  retrieveDepartmentContent(id: number): void {
    this.isLoading = true;

    this.departmentContentService.getDasboard(id).subscribe(response => {
      this.template = response.data.template;
      this.calculated = response.data.actual;
      this.summary = response.data.summary;
      
      this.isLoading = false;
    })
  }

  openConfirmationDeleteModal(): void {
    const modalRef: any = this.modalService.open(DeleteDepartmentModalComponent, { centered: true });
    modalRef.componentInstance.name = this.department.name;
    modalRef.result.then(() => this.delete(), () => { });
  }

  delete(): void {
    // this.isLoading = true;
    // this.departmentService.delete(this.department.id).subscribe(response => {
    //   this.notificationService.success('Success', 'Department deleted!', NotificationUtil.getDefaultMidConfig());
    //   this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
    // });
  }

  addDepartmentContentModal(): void {
    const modalRef: any = this.modalService.open(DepartmentContentModalComponent, { centered: true });
    modalRef.result.then(response => this.addDepartmentContent(response), () => { });
  }

  addDepartmentContent(data): void {
    this.departmentContentService.add({departmentId: this.department.id, RoleId: +data.organizationRoleId, RoleCount: + data.organizationRoleCount}).subscribe(response => {
      this.retrieveDepartmentContent(this.department.id);
      this.notificationService.success('Success', 'Role attached!', NotificationUtil.getDefaultConfig());
    }, (error) => {
      this.notificationService.error('Error', 'There was an error attaching', NotificationUtil.getDefaultConfig());
    });
  }

  openDeleteDepartmentContentModal(item): void {
    const modalRef: any = this.modalService.open(DeleteDepartmentContentModalComponent, { centered: true });
    modalRef.componentInstance.name = item.organizationRoleName;
    modalRef.result.then(() => this.deleteDepartmentContent(item.departmentRoleContentId), () => { });
  }

  deleteDepartmentContent(id: number): void {
    this.departmentContentService.delete(id).subscribe(response => {
      this.retrieveDepartmentContent(this.department.id);
      this.notificationService.success('Success', 'Role deleted!', NotificationUtil.getDefaultConfig());
    });
  }

  // calculateSummary(): void {
  //   this.summary = Object.assign({}, this.calculated);
  //   let additionalItems = this.template.roles.filter(el => this.calculated.roles.map(item => item.organizationRoleName).indexOf(el.organizationRoleName) !== 0);
  //   let concatedArr = additionalItems.concat(this.calculated.roles);

  //   this.summary.roles = concatedArr.map(calculated => {
  //     const p = this.template.roles.find(el => el.organizationRoleId === calculated.organizationRoleId);

  //     return {
  //       ...calculated,
  //       currentCount: p && p.organizationRoleCount - calculated.organizationRoleCount || 0
  //     }
  //   });
  // }
}
