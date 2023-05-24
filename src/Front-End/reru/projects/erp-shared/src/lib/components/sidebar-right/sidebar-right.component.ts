import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SidebarService } from '../../services/sidebar.service';
import { ApplicationUserService } from '../../services/application-user.service';
import { SidebarItemType, SidebarView } from '../../models/sidebar.model';
import { ApplicationUserModel } from '../../models/application-user.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmLogoutModalComponent } from '../../modals/confirm-logout-modal/confirm-logout-modal.component';
import { DomSanitizer } from '@angular/platform-browser';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../services/i18n.service';
import { PermissionCheckerService } from '../../services/permission-checker.service';

@Component({
	selector: 'app-sidebar-right',
	templateUrl: './sidebar-right.component.html',
	styleUrls: ['./sidebar-right.component.scss'],
})
export class SidebarRightComponent implements OnInit {
	isOpenModules: boolean;
	isOpenUser: boolean;
	isOpen: boolean;
	sidebarView = SidebarView;
	authUserModel: ApplicationUserModel;
	acronym: string;
	avatarString: string;
	avatar: any;
	header: string;
	myProfile: string;
	changePass: string;
	exit: string;
	logoutTitle: string;
	logoutMsg: string;
	no: string;
	yes: string;
	isEnclosed: boolean;
	sidebarItemType = SidebarItemType;
	sidebarHeight: string;
	isActiveTab: boolean[] = [true];

	@Input() user: any;
	@Input() isCustomHeader: boolean;
	@Input() modules: any[] = [];
	@Input() title: string;
	@Input() menuItems: any[] = [];
	@Output() navigate: EventEmitter<number> = new EventEmitter<number>();
	@Output() logOut = new EventEmitter();

	constructor(
		private sidebarService: SidebarService,
		private applicationUserService: ApplicationUserService,
		private modalService: NgbModal,
		private sanitizer: DomSanitizer,
		public translate: I18nService,
		private permissionCheckerService: PermissionCheckerService
	) {}

	ngOnInit(): void {
		this.subscribeForSidebarChanges();
		this.subscribeForUserChanges();
		this.setAuthUserModel();
		if (!this.menuItems || !this.menuItems.length) {
			this.sidebarService.toggle(SidebarView.MODULES, null);
		}
		let height = window.innerWidth-360
		this.sidebarHeight = `${height}px`;
	}


	navigateTo(index: number): void {
		if (!isNaN(index)) {
			this.isActiveTab = [];
			this.isActiveTab[index] = true;
			this.navigate.emit(index);
		}
		this.close();
	}

	setAuthUserModel(): void {
		if (!this.authUserModel) {
			this.authUserModel = <ApplicationUserModel>{
				isAuthenticated: true,
				tenant: { name: 'Codwer' },
				user: this.user,
				availableModules: [],
			};
		}
	}

	subscribeForUserChanges(): void {
		if (!this.isCustomHeader) {
			this.applicationUserService.userChange.subscribe(response => {
				if (response && response.user && response.user.avatar !== null) {
					this.avatarString = response.user.avatar;
					this.avatar = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/*;base64, ${this.avatarString}`);
				}
				this.authUserModel = response;
				const matches = this.authUserModel && this.authUserModel.user && this.authUserModel.user.firstName.match(/\b(\w)/g);
				this.acronym = matches ? matches.join('') : null;
			});
		} else {
			this.acronym = this.user && `${this.user.firstName[0]} ${this.user.lastName[0]}`;
		}
	}

	openLogoutModal(): void {
		forkJoin([
			this.translate.get('user-profile.logout'),
			this.translate.get('user-profile.logout-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes')
		]).subscribe(([logoutTitle, logoutMsg, no, yes]) => {
				this.logoutTitle = logoutTitle;
				this.logoutMsg = logoutMsg;
				this.no = no;
				this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmLogoutModalComponent, { centered: true });
		modalRef.componentInstance.title = this.logoutTitle;
		modalRef.componentInstance.description = this.logoutMsg;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(
			() => this.logout(),
			() => {}
		);
	}

	subscribeForSidebarChanges(): void {
		this.sidebarService.sidebar$.subscribe(response => (this.isOpen = response));
		this.sidebarService.enclosed$.subscribe((response: boolean) => (this.isEnclosed = response));
		this.sidebarService.modules$.subscribe((response: boolean) => (this.isOpenModules = response));
		this.sidebarService.user$.subscribe((response: boolean) => {
			this.isOpenUser = response;
			forkJoin([
				this.translate.get('right-sidebar.header'),
				this.translate.get('right-sidebar.my-profile'),
				this.translate.get('right-sidebar.change-password'),
				this.translate.get('right-sidebar.logout')])
				.subscribe(([header, myProfile, changePass, logout]) => {
					this.header = header;
					this.myProfile = myProfile;
					this.changePass = changePass;
					this.exit = logout;
				});
		});
	}

	close(): void {
		if (this.isOpenModules) {
			this.sidebarService.toggle(SidebarView.MODULES);
		}

		if (this.isOpenUser) {
			this.sidebarService.toggle(SidebarView.USER);
		}
	}

	toggleEnclosed(): void {
		this.sidebarService.toggleIsEnclosed();
	}

	toggle(view: SidebarView): void {
		this.sidebarService.toggle(view);
	}

	logout(): void {
		this.logOut.emit();
		window.location.reload();
	}

	navigateToChangePassword(): void {
		const location = window.location;
		window.open(`${location.protocol}//${location.host}/#/personal-profile/change-password`, '_self');
		this.close();
	}

	navigateToCoreMyProfile(): void {
		const location = window.location;
		window.open(`${location.protocol}//${location.host}/#/personal-profile/overview`, '_self');
		this.close();
	}

	ngOnDestroy(): void {
		this.close();
	}

	hasPermission(menuItem: any): boolean {
		if (!menuItem.permission) {
			return true;
		}
		return this.permissionCheckerService.isGranted(menuItem.permission);
	}

	hasPermissions(permissions: any): boolean {
		if (!permissions) {
			return true;
		}
		return this.permissionCheckerService.areGranted(permissions);
	}
}
