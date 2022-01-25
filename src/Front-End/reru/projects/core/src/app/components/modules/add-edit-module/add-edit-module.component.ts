import { Component, OnInit, NgZone } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NotificationsService } from 'angular2-notifications';
import { ModulesService } from '../../../utils/services/modules.service';
import { IconModel } from 'projects/core/src/app/utils/models/icon.model';
import { SafeHtmlPipe } from 'projects/core/src/app/utils/pipes/safe-html.pipe';
import { IconService } from '@erp/shared';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
	selector: 'app-add-edit-module',
	templateUrl: './add-edit-module.component.html',
	styleUrls: ['./add-edit-module.component.scss'],
	providers: [SafeHtmlPipe],
})
export class AddEditModuleComponent implements OnInit {
	icons: IconModel[];
	moduleForm: FormGroup;
	isLoading = true;
	id: number;
	title: string;
	description: string;

	constructor(
		private fb: FormBuilder,
		private moduleService: ModulesService,
		private router: Router,
		public translate: I18nService,
		private ngZone: NgZone,
		private route: ActivatedRoute,
		private notificationService: NotificationsService,
		private iconService: IconService,
		private location: Location
	) {
		this.icons = this.iconService.list();
	}

	ngOnInit(): void {
		this.initData();
	}

	initData(): void {
		this.route.params.subscribe(response => {
			if (response.id && response.id !== 'undefined') {
				this.id = response.id;
				this.moduleService.getForEdit(response.id).subscribe(getModuleResponse => {
					this.initForm(getModuleResponse.data);
					this.isLoading = false;
				});
			} else {
				this.initForm();
				this.isLoading = false;
			}
		});
	}

	initForm(data?: any): void {
		this.isLoading = true;
		this.moduleForm = this.fb.group({
			id: this.fb.control((data && data.id) || null, []),
			priority: this.fb.control((data && data.priority) || 0),
			code: this.fb.control((data && data.code) || null, []),
			name: this.fb.control((data && data.name) || null, [Validators.required]),
			publicUrl: this.fb.control((data && data.publicUrl) || null, [Validators.required]),
			icon: this.fb.control((data && data.icon) || null, []),
			internalGatewayAPIPath: this.fb.control((data && data.internalGatewayAPIPath) || null, []),
			color: this.fb.control((data && data.color) || null, [Validators.pattern('^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$')]),
		});
	}

	hasErrors(field): boolean {
		return this.moduleForm.touched && this.moduleForm.get(field).invalid;
	}

	hasError(field: string, error = 'required'): boolean {
		return (
			this.moduleForm.get(field).invalid && this.moduleForm.get(field).touched && this.moduleForm.get(field).hasError(error)
		);
	}

	submit(): void {
		this.isLoading = true;
		if (this.id) {
			this.moduleService.edit(this.parseRequest(this.moduleForm.value)).subscribe(
				() => {
					forkJoin([
						this.translate.get('modal.success'),
						this.translate.get('pages.modules.success-edit'),
					]).subscribe(([title, description]) => {
						this.title = title;
						this.description = description;
						});
					this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultConfig());
					this.isLoading = false;
					this.back();
				},
				() => {
					this.isLoading = false;
					forkJoin([
						this.translate.get('notification.title.error'),
						this.translate.get('notification.body.error'),
					]).subscribe(([title, description]) => {
						this.title = title;
						this.description = description;
						});
					this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultConfig());
				}
			);
		} else {
			this.moduleService.add(this.parseRequest(this.moduleForm.value)).subscribe(
				() => {
					forkJoin([
						this.translate.get('modal.success'),
						this.translate.get('pages.modules.succes-create'),
					]).subscribe(([title, description]) => {
						this.title = title;
						this.description = description;
						});
					this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultConfig());
					this.isLoading = false;
					this.back();
				},
				() => {
					this.isLoading = false;
					forkJoin([
						this.translate.get('notification.title.error'),
						this.translate.get('notification.body.error'),
					]).subscribe(([title, description]) => {
						this.title = title;
						this.description = description;
						});
					this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultConfig());
				}
			);
		}
	}

	parseRequest(data): any {
		if (!data.id) {
			return {
				name: data.name,
				priority: data.priority,
				code: data.code,
				publicUrl: data.publicUrl,
				icon: data.icon,
				internalGatewayAPIPath: data.internalGatewayAPIPath,
				color: data.color,
			};
		}
		return data;
	}

	back(): void {
		this.location.back();
	}
}
