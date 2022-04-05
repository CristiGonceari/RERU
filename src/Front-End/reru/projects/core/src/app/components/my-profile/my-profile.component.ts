import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProfileService } from '../../utils/services/profile.service';
import { UploadAvatarComponent } from './upload-avatar/upload-avatar.component';
import { DomSanitizer } from '@angular/platform-browser';
import { MyProfile } from '../../utils/models/user-profile.model';
import { I18nService } from '../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';
import { AuthenticationService, ConfirmModalComponent } from '@erp/shared';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit {
  userProfile: MyProfile;
	userProfileItems: any[];
	acronym: string;
	isLoading = false;
	avatarString: string;
	deleteTitle: string;
	deleteMsg: string;
	logoutTitle: string;
	logoutMsg: string;
	no: string;
    yes: string;
	buttonNo: string;
    buttonYes: string;
	fileId: string;

	@Input() user: any;
	@Input() isCustomHeader: boolean;
	@Output() logOut = new EventEmitter();

	constructor(
		private modalService: NgbModal,
		private authService: AuthenticationService,
		private profileService: ProfileService,
		private sanitizer: DomSanitizer,
		public translate: I18nService
	) {}

	ngOnInit(): void {
		this.getAvatar();
	}

	getAvatar(): void {
		this.isLoading = true;
		this.profileService.getUserProfile().subscribe(res => {
			this.userProfile = res.data;
			if (res.data.mediaFileId !== null) {
				this.avatarString = res.data.mediaFileId;
				this.fileId = res.data.mediaFileId;
			}
			const matches = res && (res.data.name + ' ' + res.data.lastName).match(/\b(\w)/g);
			this.acronym = matches ? matches.join('') : null;
			this.isLoading = false;
		});
	}

	openRemoveAvatarModal(): void {
		forkJoin([
			this.translate.get('upload-avatar.remove'), 
			this.translate.get('upload-avatar.sure-remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes')
		])
			.subscribe(([deleteTitle, deleteMsg, buttonNo, buttonYes]) => {
				this.deleteTitle = deleteTitle;
				this.deleteMsg = deleteMsg;
				this.buttonNo = buttonNo;
				this.buttonYes = buttonYes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.deleteTitle;
		modalRef.componentInstance.description = this.deleteMsg;
		modalRef.componentInstance.buttonNo = this.buttonNo;
		modalRef.componentInstance.buttonYes = this.buttonYes;
		modalRef.result.then(
			() => {
				this.removeAvatar();
				window.location.reload();
			},
			() => {}
		);
	}

	removeAvatar(): void {
		this.profileService.removeAvatar().subscribe();
	}

	openLogoutModal(): void {
		forkJoin([
			this.translate.get('user-profile.logout'), 
			this.translate.get('user-profile.logout-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes')
		])
			.subscribe(([logoutTitle, logoutMsg, no, yes]) => {
				this.logoutTitle = logoutTitle;
				this.logoutMsg = logoutMsg;
				this.no = no;
				this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.logoutTitle;
		modalRef.componentInstance.description = this.logoutMsg;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(
			() => this.logout(),
			() => {}
		);
	}

	logout(): void {
		this.authService.signout();
	}

	openUploadAvatarModal(): void {
		const modalRef: any = this.modalService.open(UploadAvatarComponent, { centered: true });
	}
}
