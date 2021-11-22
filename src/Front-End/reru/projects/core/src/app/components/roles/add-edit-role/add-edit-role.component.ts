import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ActivatedRoute, Router } from '@angular/router';
import { ModuleRolesService } from '../../../utils/services/module-roles.service';
import { RoleModel } from '../../../utils/models/role.model';

@Component({
	selector: 'app-add-edit-role',
	templateUrl: './add-edit-role.component.html',
	styleUrls: ['./add-edit-role.component.scss'],
})
export class AddEditRoleComponent implements OnInit {
	roleForm: FormGroup;
	moduleId: number;
	roleId: number;
	roleName: string;
	isLoading = true;

	constructor(
		private fb: FormBuilder,
		private notificationService: NotificationsService,
		private router: Router,
		private route: ActivatedRoute,
		private roleServise: ModuleRolesService,
		private location: Location
	) {}

	ngOnInit(): void {
		if (this.router.url.includes('/add-role')) {
			this.subsribeForModuleParams();
		} else {
			this.subsribeForRoleParams();
		}
	}

	subsribeForModuleParams(): void {
		this.route.params.subscribe(params => {
			if (params.id) {
				this.moduleId = params.id;
				this.initForm();
				this.isLoading = false;
			}
		});
	}

	subsribeForRoleParams(): void {
		this.route.params.subscribe(params => {
			if (params.id) {
				this.roleId = params.id;
				this.roleServise.getEditRoleById(this.roleId).subscribe(res => {
					this.initForm(res.data);
					this.isLoading = false;
				});
			}
		});
	}

	initForm(data?): void {
		this.roleForm = this.fb.group({
			name: this.fb.control((data && data.name) || null, [
				Validators.required,
				Validators.pattern(
					'^(?! )[a-zA-Z][a-zA-Z0-9-_.]{0,20}$|^[a-zA-Z][a-zA-Z0-9-_. ]*[A-Za-z][a-zA-Z0-9-_.]{0,20}$'
				),
			]),
			isAssignByDefault: this.fb.control((data && data.isAssignByDefault) || false, [Validators.required])
		});
	}

	hasErrors(field): boolean {
		return this.roleForm.touched && this.roleForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.roleForm.get(field).invalid && this.roleForm.get(field).touched && this.roleForm.get(field).hasError(error)
		);
	}

	addRole(): void {
		let addRoleModel = {
			moduleId: this.moduleId,
			name: this.roleForm.value.name,
			isAssignByDefault : this.roleForm.value.isAssignByDefault,
		} as RoleModel;
		this.roleServise.addRole(addRoleModel).subscribe(
			() => {
				this.notificationService.success(
					'Success',
					'Role has been created successfully!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.router.navigate(['../../', this.moduleId, 'roles'], { relativeTo: this.route });
			},
			() => {
				this.notificationService.error('Error', 'Role has not been created!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	editRole(): void {
		let editRoleModel = {
			id: +this.roleId,
			name: this.roleForm.value.name,
			isAssignByDefault : this.roleForm.value.isAssignByDefault

		} as RoleModel;
		this.roleServise.editRole(editRoleModel).subscribe(
			() => {
				this.notificationService.success(
					'Success',
					'Role has been edited successfully!',
					NotificationUtil.getDefaultMidConfig()
				);
				this.back();
			},
			() => {
				this.notificationService.error('Error', 'Role has not been edited!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	submit(): void {
		if (this.roleId) {
			this.editRole();
		} else {
			this.addRole();
		}
	}

	back(): void {
		this.location.back();
	}
}
