import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '../../../utils/modals/confirm-modal/confirm-modal.component';
import { RoleModel } from '../../../utils/models/role.model';
import { RoleService } from '../../../utils/services/role.service';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  role: RoleModel;
  constructor(private roleService: RoleService,
    private route: ActivatedRoute,
    private router: Router,
    private ngZone: NgZone,
    private modalService: NgbModal,
    private notificationService: NotificationsService) { }

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
    this.roleService.get(id).subscribe(response => {
      if (!response) {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        return;
      }
      this.role = response.data;
      this.isLoading = false;
    });
  }

  openConfirmationDeleteModal(): void {
    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = 'Delete';
    modalRef.componentInstance.description = 'Are you sure you want to delete it?';
    modalRef.result.then(() => this.delete(), () => { });
  }

  delete(): void {
    this.isLoading = true;
    this.roleService.delete(this.role.id).subscribe(response => {
      this.notificationService.success('Success', 'Module has been successfully deleted!', NotificationUtil.getDefaultMidConfig());
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
    });
  }
}
