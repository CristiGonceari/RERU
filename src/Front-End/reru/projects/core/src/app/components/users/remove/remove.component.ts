import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';
import { UserForRemove } from '../../../utils/models/user-for-remove.model';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
	selector: 'app-remove',
	templateUrl: './remove.component.html',
	styleUrls: ['./remove.component.scss']
})
export class RemoveComponent implements OnInit {
	isLoading = false;
	userId: number;
	userData: UserForRemove;
	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private activatedRoute: ActivatedRoute,
		private location: Location,
		private userService: UserService,
		private ngZone: NgZone,
		public translate: I18nService,
		private router: Router,
		private route: ActivatedRoute,
		private notificationService: NotificationsService
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.isLoading = true;
		this.activatedRoute.params.subscribe(params => {
			if (params.id) {
				this.userId = params.id;
				this.getUserForRemove();
			}
		});
	}

	back(): void {
		this.location.back();
	}

	removeUser(): void {
		this.isLoading = true;
		this.userService.removeUser(this.userId).subscribe(
			res => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('user.succes-remove'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
					});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.ngZone.run(() => this.router.navigate(['../../list'], { relativeTo: this.route }));
				this.isLoading = false;
			},
			err => {
				forkJoin([
					this.translate.get('notification.title.error'),
					this.translate.get('notification.body.error'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
					});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.isLoading = false;
			}
		);
	}

	getUserForRemove(): void {
		this.userService.getUserForRemove(this.userId).subscribe(res => {
			if (res) {
				this.userData = res.data;
				this.isLoading = false;
			}
		});
	}
}
