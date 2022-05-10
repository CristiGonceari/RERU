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
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
	selector: 'app-user-profile',
	templateUrl: './user-profile.component.html',
	styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {
	userId: any;
	acronym: string;
	isLoading = false;
	oneColumn: boolean;
	avatarString: string;
	avatar: any;
	title: string;
	description: string;
	no: string;
	yes: string;
	fileId: string;
	avatarLoading: boolean = true;

	@Input() user: any;
	@Input() isCustomHeader: boolean;

	constructor(
		private activatedRoute: ActivatedRoute,
		private userService: UserService,
		private userProfileService: UserProfileService,
		public translate: I18nService,
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
			const matches = user && (user.data.firstName + ' ' + user.data.lastName).match(/\b(\w)/g);
			this.acronym = matches ? matches.join('') : null;
		}
	}

	getUserById(id: number) {
		this.userProfileService.getUserProfile(id).subscribe(response => {
			this.subscribeForUserChanges(response);
			this.userId = response.data.id;
			this.fileId = response.data.mediaFileId;
      		this.avatarLoading = false;
			this.isLoading = false;
		});
	}

	openResetPasswordModal(id: string): void {
		forkJoin([
			this.translate.get('reset-password.title'),
			this.translate.get('reset-password.reset-pass-msg'),
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
		modalRef.result.then(() => this.reset(this.userId), () => { });
	}

	reset(id: string): void {
		this.userService.resetPassword(this.userId).subscribe(response => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('reset-password.success-reset'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title,this.description, NotificationUtil.getDefaultMidConfig());
			window.location.reload();
		});
	}

	openActivateUserModal(id: string): void {
		forkJoin([
			this.translate.get('activate-user.title'),
			this.translate.get('activate-user.activate-msg'),
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
		modalRef.result.then(
			() => this.activateUser(this.userId),
			() => { }
		);
	}

	openDeactivateUserModal(id: string): void {
		forkJoin([
			this.translate.get('deactivate-user.title'),
			this.translate.get('deactivate-user.deactivate-msg'),
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
		modalRef.result.then(
			() => this.deactivateUser(this.userId),
			() => { }
		);
	}

	activateUser(id: string): void {
		this.userService.activateUser(this.userId).subscribe(response => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('activate-user.success-activate'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			window.location.reload();
		});
	}

	deactivateUser(id: string): void {
		this.userService.deactivateUser(this.userId).subscribe(response => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('deactivate-user.success-deactivate'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			window.location.reload();
		});
	}
	
	navigateToEvaluation(id): void {
		let host = window.location.host;
		window.open(`http://${host}/reru-evaluation/#/user-profile/${id}/overview`, '_self')
	}
}
