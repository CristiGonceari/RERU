import { Component, OnInit } from '@angular/core';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { PaginationSummary } from '../../../utils/models/pagination-summary.model';
import { User } from '../../../utils/models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ObjectUtil } from '../../../utils/util/object.util';
import { ConfirmModalComponent, PermissionCheckerService, PrintModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { UserService } from '../../../utils/services/user.service';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import { ImportUsersModalComponent } from '../../../utils/modals/import-users-modal/import-users-modal.component';
import { saveAs } from 'file-saver';

@Component({
	selector: 'app-user-list-table',
	templateUrl: './user-list-table.component.html',
	styleUrls: ['./user-list-table.component.scss'],
})
export class UserListTableComponent implements OnInit {
	users: User[];
	pagination: PaginationSummary = new PaginationSummary();
	pager: number[] = [];
	result: boolean;
	asc: boolean;
	order: string;
	keyword: string;
	email: string;
	idnp: string;
	viewDetails: boolean = false;
	filter = {
		sort: 'name',
		order: true
	}

	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	filters: any = {}

	isLoading = true;
	module: any;
	userState: number = 0;
	searchValue: string;
	title: string;
	description: string;
	no: string;
	yes: string;

	constructor(
		private route: ActivatedRoute,
		private userProfileService: UserProfileService,
		public translate: I18nService,
		private router: Router,
		public permissionService: PermissionCheckerService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
		private userService: UserService,
	) { }

	ngOnInit(): void {
		this.list();
		this.prepareFilter('name', 'asc');
		this.checkPermission();
	}

	getFilteredUsers(data: any = {}): void {
		this.keyword = data.keyword;
		this.email = data.email;
		this.idnp = data.idnp;
		data = {
			...data,
			keyword: this.keyword,
			email: this.email,
			idnp: this.idnp,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
			status: data.userState
		};
		this.list(data);
	}

	list(data: any = {}): void {
		this.isLoading = true;
		this.userProfileService.getUserProfiles(ObjectUtil.preParseObject(data)).subscribe(response => {
			if (response && response.data.items.length) {
				this.result = true;
				this.users = response.data.items;
				this.pagination = response.data.pagedSummary;
				this.isLoading = false;
			} else {
				this.isLoading = false;
				this.result = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['-', 'lastName', 'firstName', 'fatherName', 'idnp', 'email', 'isActive'];
		for (let i=1; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			keyword: this.keyword || '',
			email: this.email || '',
			idnp: this.idnp || ''
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
		]).subscribe(
			(items) => {
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.userProfileService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].substring(2).slice(0, -2);
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	prepareFilter(sort, order): void {
		if (sort === this.filter.sort) {
			this.filter.order = !this.filter.order;
			return
		} else if (sort !== this.filter.sort) {
			this.filter.sort = sort;
			this.filter.order = false;
		}
	}

	sortedList(data: any = {}): void {
		this.prepareFilter(data.sort, data.order);
		data = {
			...data,
			keyword: this.keyword || this.searchValue,
			email: this.email || this.searchValue,
			idnp: this.idnp || this.searchValue,
			order: this.filter.order ? 'desc' : 'asc',
			sort: this.filter.sort,
			page: data.page || this.pagination.currentPage,
			status: data.userState || this.userState
		};
		this.isLoading = true;
		this.userProfileService.getUserProfiles(ObjectUtil.preParseObject(data)).subscribe(response => {
			if (response && response.data.items.length) {
				this.result = true;
				this.users = response.data.items;
				this.pagination = response.data.pagedSummary;
				for (let i = 1; i <= this.pagination.totalCount; i++) {
					this.pager.push(i);
					this.isLoading = false;
				}
			} else {
				this.isLoading = false;
				this.result = false;
			}
		});
	}

	renderText(user): string {
		if (!user || !user.firstName || !user.lastName) {
			return '';
		}

		return `${user.lastName[0].toUpperCase()}${user.firstName[0].toUpperCase()}`;
	}

	navigateToDetails(id): void {
		this.router.navigate(['../../user-profile', id, 'overview'], { relativeTo: this.route });
	}

	checkPermission(): void {
		if (this.permissionService.isGranted('P00000020')) {
			this.viewDetails = true;
		}
	}

	openImportModal(): void {
		const modalRef: any = this.modalService.open(ImportUsersModalComponent, { centered: true, backdrop: 'static', size: 'lg' });
		modalRef.result.then((data) => this.importUsers(data), () => { });
	}

	importUsers(data): void {
		this.isLoading = true;
		const form = new FormData();
		form.append('File', data.file);
		this.userService.bulkAddUsers(form).subscribe(response => {
			if(response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
			}
			this.notificationService.success('Success', 'Users Imported!', NotificationUtil.getDefaultMidConfig());
			this.list();
		}, () => { }, () => {
			this.isLoading = false;
		})
	}


	openConfirmModal(id: number, firstName, lastName, type): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		if (type == 'reset-password') {
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
			modalRef.componentInstance.title = this.title;
			modalRef.componentInstance.description = `${this.description} ${firstName} ${lastName}?`;
			modalRef.componentInstance.buttonNo = this.no;
			modalRef.componentInstance.buttonYes = this.yes;
			modalRef.result.then(() => this.resetPassword(id, firstName, lastName), () => { });
		}
		if (type == 'deactivate-user') {
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
			modalRef.componentInstance.title = this.title;
			modalRef.componentInstance.description = `${this.description} ${firstName} ${lastName}?`;
			modalRef.componentInstance.buttonNo = this.no;
			modalRef.componentInstance.buttonYes = this.yes;
			modalRef.result.then(() => this.deactivateUser(id, firstName, lastName), () => { });
		}
		if (type == 'activate-user') {
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
			modalRef.componentInstance.title = this.title;
			modalRef.componentInstance.description = `${this.description} ${firstName} ${lastName}?`;
			modalRef.componentInstance.buttonNo = this.no;
			modalRef.componentInstance.buttonYes = this.yes;
			modalRef.result.then(() => this.activateUser(id, firstName, lastName), () => { });
		}
	}

	resetPassword(id, firstName, lastName): void {
		this.userService.resetPassword(id).subscribe(
			(res) => {
				forkJoin([
					this.translate.get('modal.success'),
					this.translate.get('reset-password.success-reset'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				if (res) { this.list(); }
			},
			(err) => {
				forkJoin([
					this.translate.get('notification.title.error'),
					this.translate.get('notification.body.error'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			},
		);
	}

	deactivateUser(id, firstName, lastName): void {
		this.userService.deactivateUser(id).subscribe((res) => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('deactivate-user.success-deactivate'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			if (res) { this.list(); }
		}
		);
	}

	activateUser(id, firstName, lastName): void {
		this.userService.activateUser(id).subscribe((res) => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('activate-user.success-activate'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			if (res) { this.list(); }
		},
			(err) => {
				forkJoin([
					this.translate.get('notification.title.error'),
					this.translate.get('notification.body.error'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			}
		);
	}

	navigateToEvaluation(id): void {
		let host = window.location.host;
		console.warn('url for user overview evaluation', `http://${host}/reru-evaluation/#/user-profile/${id}/overview`);
		window.open(`http://${host}/reru-evaluation/#/user-profile/${id}/overview`, '_self')
	}

}
