import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModulesService } from '../../../utils/services/modules.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '../../../utils/modals/confirm-modal/confirm-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { IconService } from '@erp/shared';
import { IconModel } from 'projects/core/src/app/utils/models/icon.model';

@Component({
  selector: 'app-view-module',
  templateUrl: './view-module.component.html',
  styleUrls: ['./view-module.component.scss']
})
export class ViewModuleComponent implements OnInit {
  icons: IconModel[];
  isLoading = false;
  module: any;
  moduleId: number;
  constructor(private route: ActivatedRoute,
    private moduleService: ModulesService,
    private router: Router,
    private ngZone: NgZone,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    private iconService: IconService) {
    this.icons = this.iconService.list();
  }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      this.moduleId = response.id
      if (response.id) {
        this.isLoading = true;
        this.moduleService.get(response.id).subscribe(response => {
          if (!response) {
            this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
          }
          this.module = response.data;
          const item = this.icons.find(el => el.name === this.module.icon);
          this.module.icon = item && item.icon || null;
          this.isLoading = false;
        });
      } else {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      }
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
    this.moduleService.delete(this.module.id).subscribe(response => {
      this.notificationService.success('Success', 'Module has been successfully deleted!', NotificationUtil.getDefaultMidConfig());
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
    });
  }

}
