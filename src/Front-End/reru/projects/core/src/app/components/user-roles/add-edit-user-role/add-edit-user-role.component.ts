import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../../utils/services/i18n.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { Location } from '@angular/common';
import { UserRoleService } from '../../../utils/services/user-role.service';
import { UserRoleModel } from '../../../utils/models/user-role.model';

@Component({
  selector: 'app-add-edit-user-role',
  templateUrl: './add-edit-user-role.component.html',
  styleUrls: ['./add-edit-user-role.component.scss']
})
export class AddEditUserRoleComponent implements OnInit {
	roleForm: FormGroup;
	roleId: number;
	isLoading: boolean = true;  
	title: string;
	description: string;

	constructor(
		private formBuilder: FormBuilder,
		private roleService: UserRoleService,
		public translate: I18nService,
		private location: Location,
		private activatedRoute: ActivatedRoute,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.roleForm = new FormGroup({ name: new FormControl(), colaboratorId: new FormControl() });
		this.initData();
	}

	initData(): void {
		this.activatedRoute.params.subscribe(response => {
			if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
				this.roleId = response.id;
				this.roleService.get(this.roleId).subscribe(res => {
					this.initForm(res.data);
					console.log("role", res.data)
				})
			}
			else
				this.initForm();
		})
	}

	initForm(role?: any): void {
		if (role) {
			this.roleForm = this.formBuilder.group({
				id: this.formBuilder.control(role.id, [Validators.required]),
				name: this.formBuilder.control((role && role.name) || null, Validators.required),
				colaboratorId: this.formBuilder.control((role && role.colaboratorId), Validators.required)
			});
			this.isLoading = false;
		}
		else {
			this.roleForm = this.formBuilder.group({
				name: this.formBuilder.control(null, [Validators.required]),
				colaboratorId: this.formBuilder.control(0, [Validators.required])
			});
			this.isLoading = false;
		}
	}

	onSave(): void {
		if (this.roleId) {
			this.edit();
		} else {
			this.add();
		}
	}

	backClicked() {
		this.location.back();
	}

	add(): void {
		const data = {
			name: this.roleForm.value.name,
			colaboratorId: this.roleForm.value.colaboratorId
		} as UserRoleModel;

		this.roleService.create(data).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('user-roles.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	edit(): void {
		const data = {
			id: this.roleId,
			name: this.roleForm.value.name,
			colaboratorId: this.roleForm.value.colaboratorId
		} as UserRoleModel;

		this.roleService.edit(data).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('user-roles.succes-edit-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}
}
