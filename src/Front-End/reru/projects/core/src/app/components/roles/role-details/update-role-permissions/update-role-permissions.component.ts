import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { PermissionModel } from 'projects/core/src/app/utils/models/permission.model';
import { ModuleRolePermissionsModel } from 'projects/core/src/app/utils/models/module-role-permissions.model';
import { ModuleRolePermissionsService } from 'projects/core/src/app/utils/services/module-role-permissions.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { ModuleRolesService } from 'projects/core/src/app/utils/services/module-roles.service';
import { RoleModel } from 'projects/core/src/app/utils/models/role.model';

@Component({
	selector: 'app-update-role-permissions',
	templateUrl: './update-role-permissions.component.html',
	styleUrls: ['./update-role-permissions.component.scss']
})
export class UpdateRolePermissionsComponent implements OnInit {
	isLoading = false;
	role:RoleModel;
	roleId: number;
	permissionForm: FormGroup;
	permissions: PermissionModel[];
	moduleRolePermissions: ModuleRolePermissionsModel;
	permissionsForUpdate2 = [];

	constructor(
		private permissionServise: ModuleRolePermissionsService,
		private moduleRoleServise: ModuleRolesService,
		private activatedRoute: ActivatedRoute,
		private fb: FormBuilder,
		private notificationService: NotificationsService,
		private router: Router,
		private location: Location,
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	parseRequest(data: PermissionModel[]): PermissionModel[] {
		const permissions = []; const id = Object.values(data).filter(x => typeof x === 'number');
		const isGranted = Object.values(data).filter(x => typeof x === 'boolean');
		const length = Object.keys(data).length / 2;

		for (let i = 0; i < length; i++) {
			permissions.push({
				permissionId: id[i],
				isGranted: isGranted[i]
			})
		}
		this.permissionsForUpdate2 = Object.values(permissions);
		return {
			...permissions,
		};
	}

	initForm(data: PermissionModel[]): void {
		this.permissionForm = this.fb.group({});
		for (let i = 0; i < data.length; i++) {
			this.permissionForm.addControl('data.id' + i, this.fb.control(data[i].id, [])),
				this.permissionForm.addControl('option' + i, this.fb.control(data[i].isGranted, []));
		}
	}

	subsribeForParams() {
		this.activatedRoute.params.subscribe(params => {
			if (params.id) {
				this.roleId = params.id;
				this.getRole();
				this.getPermissionsForUpdate();
			}
		});
	}

	getPermissionsForUpdate(): void {
		this.isLoading = true;
		this.permissionServise.getForUpdate(this.roleId).subscribe(res => {
			if (res) {
				this.permissions = res.data;
				this.initForm(res.data);
				this.isLoading = false;
			}
		});
	}

	getRole(): void {
		this.isLoading = true;
		this.moduleRoleServise.getById(this.roleId).subscribe(res => {
			if (res) {
				this.role = res.data
				this.isLoading = false;
			}
		});
	}

	updateRolePermission(): void {
		const permissionsForUpdate = this.parseRequest(this.permissionForm.value);

		this.moduleRolePermissions = {
			moduleRoleId: this.roleId,
			permissions: this.permissionsForUpdate2
		}

		this.permissionServise.updateRolePermission(this.moduleRolePermissions).subscribe(
			(res) => {
				this.notificationService.success('Success', 'Permissions for Module Role has been updated successfully!', NotificationUtil.getDefaultMidConfig());
				this.back();
			},
			(err) => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	back(): void {
        this.location.back();
    }
}
