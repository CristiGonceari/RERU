import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { ModulesService } from 'projects/core/src/app/utils/services/modules.service';
import { NotificationUtil } from 'projects/core/src/app/utils/util/notification.util';
import { Location } from '@angular/common';


@Component({
	selector: 'app-remove-module',
	templateUrl: './remove-module.component.html',
	styleUrls: ['./remove-module.component.scss'],
})
export class RemoveModuleComponent implements OnInit {
	isLoading = false;
	moduleId: number;
	moduleData: any;

	constructor(
		private activatedRoute: ActivatedRoute,
		private location: Location,
		private moduleService: ModulesService,
		private notificationService: NotificationsService
	) {}

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.isLoading = true;
		this.activatedRoute.params.subscribe(params => {
			if (params.id) {
				this.moduleId = params.id;
			}
		});
		this.getModuleInfo();
	}

	back(): void {
		this.location.back();
	}

	getModuleInfo(): void {
		this.moduleService.get(this.moduleId).subscribe((res) => {
			this.moduleData = res.data;
			this.isLoading = false;
		})
	}

	removeModule(): void {
		this.moduleService.delete(this.moduleId).subscribe(
			res => {
				this.notificationService.success(
					'Success',
					`Module ${this.moduleData.name} has been removed successfully!`,
					NotificationUtil.getDefaultMidConfig()
				);
				this.location.back();
			},
			err => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
			}
		);
	}
}
