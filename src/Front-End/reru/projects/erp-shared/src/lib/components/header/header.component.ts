import { Component, OnInit, Renderer2, Output, EventEmitter, Input } from '@angular/core';
import { SidebarService } from '../../services/sidebar.service';
import { SidebarView } from '../../models/sidebar.model';
import { ApplicationUserService } from '../../services/application-user.service';
import { ApplicationUserModel } from '../../models/application-user.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
	isOpenUserIcon: boolean;
	isOpenModules: boolean;
	isOpenUser: boolean;
	authUserModel: ApplicationUserModel;
	sidebarView = SidebarView;
	acronym: string;
	avatarString: string;
	avatar: any;

	@Input() logo: string;
	@Input() appLogo: string;
	@Input() languages: any = [];
	@Input() currentLanguage: string;
	@Input() isCustomHeader: boolean;
	@Input() disableSidenav: boolean;
	@Input() isEnclosed: boolean;
	@Output() changeLanguage = new EventEmitter<string>();

	constructor(
		private sidebarService: SidebarService,
		private renderer: Renderer2,
		private applicationUserService: ApplicationUserService,
		private sanitizer: DomSanitizer
	) {}

	ngOnInit(): void {
		this.isEnclosed = this.sidebarService.isEnclosedOn();
		this.subscribeForSidebarChanges();
		this.subscribeForUserChanges();
	}

	getLang(): string {
		const language = this.languages.find(el => el.code === this.currentLanguage);
		return (language && language.text) || 'Language';
	}

	subscribeForUserChanges(): void {
		if (!this.isCustomHeader) {
			this.applicationUserService.userChange.subscribe(response => {
				this.authUserModel = response;
				if (response && response.user && response.user.avatar !== null) {
					this.avatarString = response.user.avatar;
					this.avatar = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/*;base64, ${this.avatarString}`);
				}
				let matches = this.authUserModel && this.authUserModel.user && this.authUserModel.user.firstName.match(/\b(\w)/g);
				this.acronym = matches ? matches.join('') : null;
			});
		} else {
			this.acronym = this.authUserModel.user && 
			`${this.authUserModel.user.firstName[0]} ${this.authUserModel.user.firstName[0]}`;
		}
	}

	subscribeForSidebarChanges(): void {
		this.sidebarService.modules$.subscribe((response: boolean) => (this.isOpenModules = response));
		this.sidebarService.user$.subscribe((response: boolean) => (this.isOpenUser = response));
	}

	toggleUserIcon(): void {
		this.isOpenUserIcon = !this.isOpenUserIcon;
		if (this.isOpenUserIcon) {
			this.renderer.addClass(document.body, 'topbar-mobile-on');
		} else {
			this.renderer.removeClass(document.body, 'topbar-mobile-on');
		}
	}

	toggleEnclosed(): void {
		this.sidebarService.toggleIsEnclosed();
	}

	toggle(view: SidebarView) {
		this.sidebarService.toggle(view);
	}
}
