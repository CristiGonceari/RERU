import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { DepartmentService } from '../../../utils/services/department.service';
import { I18nService } from '../../../utils/services/i18n.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { UserRoleService } from '../../../utils/services/user-role.service';

@Component({
  selector: 'app-user-role-details',
  templateUrl: './user-role-details.component.html',
  styleUrls: ['./user-role-details.component.scss']
})
export class UserRoleDetailsComponent implements OnInit {
  roleId: number;
  roleName: string;
  isLoading: boolean = true;
  title: string;
	description: string;
	no: string;
	yes: string;

  constructor(
    private roleService: UserRoleService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
    public router: Router,
    private modalService: NgbModal,
		private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
   this.subsribeForParams();
  }

  get(){
    this.roleService.get(this.roleId).subscribe(res => {
      if (res && res.data) {
        this.roleName = res.data.name;
        this.isLoading = false;
      }
    })
  }

  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.roleId = params.id;
			if (this.roleId) {
        this.get();
    }});
	}

  delete(roleId): void{
		this.roleService.delete(roleId).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('user-roles.succes-delete-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.router.navigate(['/user-roles']);
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	openConfirmationDeleteModal(): void {
    	forkJoin([
			this.translate.get('modal.delete'),
			this.translate.get('user-roles.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
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
		modalRef.result.then(() => this.delete(this.roleId), () => { });
	}
}
