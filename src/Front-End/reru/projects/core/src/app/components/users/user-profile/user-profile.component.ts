import { Component, Input, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { filter } from 'rxjs/operators';
import { User } from '../../../utils/models/user.model';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { UserService } from '../../../utils/services/user.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { DomSanitizer } from '@angular/platform-browser';
import { ConfirmModalComponent } from '@erp/shared';

@Component({
	selector: 'app-user-profile',
	templateUrl: './user-profile.component.html',
	styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {
	acronym: string;
	isLoading = false;
	oneColumn: boolean;
	avatarString: string;
	avatar: any;

	@Input() user: any;
	@Input() isCustomHeader: boolean;

	constructor(
		private activatedRoute: ActivatedRoute,
		private userService: UserService,
		private userProfileService: UserProfileService,
		private router: Router,
		private route: ActivatedRoute,
		private notificationService: NotificationsService,
		private modalService: NgbModal,
		private sanitizer: DomSanitizer,
		private ngZone: NgZone
	) { }

	ngOnInit(): void {
		this.subsribeForParams();
		this.routeChanges();
		this.setRouteView();
	}

	setRouteView(): void {
		this.oneColumn = this.router.url.includes('give') ||
			this.router.url.includes('update') ||
			this.router.url.includes('remove') ? true : false;
	}

	routeChanges(): void {
		this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(() => {
			this.setRouteView();
		})

	}

	subsribeForParams() {
		this.isLoading = true;
		this.activatedRoute.params.subscribe(params => {
			if (params.id) {
				this.getUserById(params.id);
			}
		});
	}

	parseRequest(data: User): User {
		return {
			...data,
		};
	}

	subscribeForUserChanges(user): void {
		if (!this.isCustomHeader) {
			this.user = user;
			if (user.data.avatar !== null) {
				this.avatarString = user.data.avatar;
				this.avatar = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/*;base64, ${this.avatarString}`);
			}
			const matches = user && (user.data.name + ' ' + user.data.lastName).match(/\b(\w)/g);
			this.acronym = matches ? matches.join('') : null;
		}
	}

	getUserById(id: number) {
		this.userProfileService.getUserProfile(id).subscribe(response => {
			this.subscribeForUserChanges(response);
			this.isLoading = false;
		});
	}

	openResetPasswordModal(id: string): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Reset';
		modalRef.componentInstance.description = 'Are you sure you want to reset the password?';
		modalRef.result.then(
			() => this.reset(id),
			() => { }
		);
	}

	reset(id: string): void {
		this.userService.resetPassword(id).subscribe(response => {
			this.notificationService.success(
				'Success',
				'Password has been send to your email',
				NotificationUtil.getDefaultMidConfig()
			);
			this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
		});
	}

	openActivateUserModal(id: string): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Activate';
		modalRef.componentInstance.description = 'Are you sure you want to activate this user?';
		modalRef.result.then(
			() => this.activateUser(id),
			() => { }
		);
	}

	openDeactivateUserModal(id: string): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Deactivate';
		modalRef.componentInstance.description = 'Are you sure you want to deactivate this user?';
		modalRef.result.then(
			() => this.deactivateUser(id),
			() => { }
		);
	}

	activateUser(id: string): void {
		this.userService.blockUnblockUser(id).subscribe(response => {
			this.notificationService.success('Success', 'User has been activated', NotificationUtil.getDefaultMidConfig());
			window.location.reload();
		});
	}

	deactivateUser(id: string): void {
		this.userService.blockUnblockUser(id).subscribe(response => {
			this.notificationService.success('Success', 'User has been deactivated', NotificationUtil.getDefaultMidConfig());
			window.location.reload();
		});
	}
}
