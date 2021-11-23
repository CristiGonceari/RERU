import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';
import { UserForRemove } from '../../../utils/models/user-for-remove.model';

@Component({
	selector: 'app-remove',
	templateUrl: './remove.component.html',
	styleUrls: ['./remove.component.scss']
})
export class RemoveComponent implements OnInit {
	isLoading = false;
	userId: number;
	userData: UserForRemove;

	constructor(
		private activatedRoute: ActivatedRoute,
		private location: Location,
		private userService: UserService,
		private ngZone: NgZone,
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
		this.userService.removeUser(this.userId).subscribe(
			res => {
				this.notificationService.success(
					'Success',
					`User ${this.userData.name} ${this.userData.lastName} has been removed successfully!`,
					NotificationUtil.getDefaultMidConfig()
				);
				this.ngZone.run(() => this.router.navigate(['../../list'], { relativeTo: this.route }));
			},
			err => {
				this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
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
