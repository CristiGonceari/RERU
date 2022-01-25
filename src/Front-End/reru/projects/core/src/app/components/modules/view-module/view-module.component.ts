import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ModulesService } from '../../../utils/services/modules.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ConfirmModalComponent, IconService } from '@erp/shared';
import { IconModel } from 'projects/core/src/app/utils/models/icon.model';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

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
  title: string;
	description: string;
	no: string;
	yes: string;

  constructor(private route: ActivatedRoute,
    private moduleService: ModulesService,
    private router: Router,
		public translate: I18nService,
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
    forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('pages.modules.delete-msg'),
			this.translate.get('modal.no'),
			this.translate.get('modal.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
    const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(() => this.delete(), () => { });
  }

  delete(): void {
    this.isLoading = true;
    this.moduleService.delete(this.module.id).subscribe(response => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('modules.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
    });
  }

}
