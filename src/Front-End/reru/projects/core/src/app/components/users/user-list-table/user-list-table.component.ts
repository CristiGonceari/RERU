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
import { AccessModeEnum } from '../../../utils/models/access-mode.enum';
import { UserStatusEnum } from '../../../utils/models/user-status-enum.enum';
import { ProcessService } from '../../../utils/services/process.service';
import { HttpEvent, HttpEventType } from '@angular/common/http';

@Component({
	selector: 'app-user-list-table',
	templateUrl: './user-list-table.component.html',
	styleUrls: ['./user-list-table.component.scss'],
})
export class UserListTableComponent implements OnInit {
	users: User[];
	pagination: PaginationSummary = new PaginationSummary();
	result: boolean;
	viewDetails: boolean = false;

	interval: any;
	processesData: any;
	processProgress: any;
	processId: any;
	toolBarValue: number = 0;
	isStartAddingUsers: boolean = false;
	isFileValid: boolean;

	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];
	filters: any = {};

	isLoading = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	accessMode = AccessModeEnum;
	userStatusEnum = UserStatusEnum;

	fileName: string;
	fileStatus = { requestType: '', percent: 1 }
	isLoadingMedia: boolean = false;

	constructor(
		private route: ActivatedRoute,
		private userProfileService: UserProfileService,
		public translate: I18nService,
		private router: Router,
		public permissionService: PermissionCheckerService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
		private userService: UserService,
		private processService: ProcessService
	) { }

	ngOnInit(): void {
		this.list();
		this.checkPermission();
	}

	setFilter(field: string, value, startSearch: boolean = false): void {
		this.filters[field] = value;
		this.pagination.currentPage = 1;
		this.list();
	}

	resetFilter(): void {
		this.filters = {};
		this.list();
	}

	list(data: any = {}): void {
		data = ObjectUtil.preParseObject({
			page: data.page || this.pagination.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
			...this.filters,
		});
		this.isLoading = true;
		this.userProfileService.getUserProfiles(ObjectUtil.preParseObject(data)).subscribe(response => {
			if (response && response.data.items) {
				this.users = response.data.items;
				this.pagination = response.data.pagedSummary;
				this.isLoading = false;
			} else {
				this.isLoading = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['-', 'userName', 'lastName', 'firstName', 'fatherName', 'idnp', 'email', 'departmentName', 'departmentColaboratorId', 'roleName', 'roleColaboratorId','functionName' ,'functionColaboratorId', 'birthday', 'phoneNumber', 'userStatusEnum', 'accessModeEnum', 'isActive'];
         
		for (let i = 1; i <= headersHtml.length - 2; i++) {
			if (i == 1) {
				this.headersToPrint.push({ value: "userName", label: this.printTranslates[6], isChecked: true })
			} else if (i == 2) {
				this.headersToPrint.push({ value: "lastName", label: this.printTranslates[7], isChecked: true })
			} else if (i == 3) {
				this.headersToPrint.push({ value: "firstName", label: this.printTranslates[8], isChecked: true })
			} else if (i == 4) {
				this.headersToPrint.push({ value: "fatherName", label: this.printTranslates[9], isChecked: true })
			} else if (i == 8) {
				this.headersToPrint.push({ value: "departmentColaboratorId", label: this.printTranslates[10], isChecked: true })
			} else if (i == 10) {
				this.headersToPrint.push({ value: "roleColaboratorId", label: this.printTranslates[11], isChecked: true })
			} else if (i == 12) {
				this.headersToPrint.push({ value: "functionColaboratorId", label: this.printTranslates[12], isChecked: true })
			} else if (i == 13) {
				this.headersToPrint.push({ value: "birthday", label: this.printTranslates[13], isChecked: true })
			} else if (i == 14) {
				this.headersToPrint.push({ value: "phoneNumber", label: this.printTranslates[14], isChecked: true })
			} else {
				this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML, isChecked: true })
			}
		}

		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			...this.filters
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel', 'select-file-format', 'max-print-rows', 'full-username', 'last-name', 'first-name', 
								'father-name', 'department-colaborator-id', 'role-colaborator-id', 'function-colaborator-id', 'birth-day', 'phone-number' ]
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel'),
      		this.translate.get('print.select-file-format'),
		  	this.translate.get('print.max-print-rows'),
			this.translate.get('print.full-username'),
			this.translate.get('print.last-name'),
			this.translate.get('print.first-name'),
			this.translate.get('print.father-name'),
			this.translate.get('print.department-colaborator-id'),
			this.translate.get('print.role-colaborator-id'),
			this.translate.get('print.function-colaborator-id'),
			this.translate.get('print.birth-day'),
			this.translate.get('print.phone-number'),
			]).subscribe(
			(items) => {
				for (let i = 0; i < this.printTranslates.length; i++) {
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
				const file = new File([blob], data.tableName.trim(), { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
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
		const modalRef: any = this.modalService.open(ImportUsersModalComponent, { centered: true, size: 'xl' });
		modalRef.result.then((data) => this.importUsers(data), () => { });
	}

	importUsers(data): void {
		this.isLoading = true;
		this.isStartAddingUsers = true;

		this.userService.startAddProcess({ totalProcesses: null, processType: 1 }).subscribe(res => {
			this.processId = res.data

			const interval = this.setIntervalGetProcess();

			const form = new FormData();
			form.append('Data.File', data.file);
			form.append('ProcessId', this.processId);
			this.userService.bulkAddUsers(form).subscribe(response => {
				if (response) {
					const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
					const blob = new Blob([response.body], { type: response.body.type });
					const file = new File([blob], fileName, { type: response.body.type });
					saveAs(file);

					if (fileName.includes("Invalid")) {
						this.isFileValid = false;
					} else {
						this.isFileValid = true;
					}
				}
				if (this.isFileValid) {
					forkJoin([
						this.translate.get('notification.title.success'),
						this.translate.get('bulk-import-users.succes-msg'),
					]).subscribe(([title, description]) => {
						this.title = title;
						this.description = description;
					});
					this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				} else {
					forkJoin([
						this.translate.get('notification.title.error'),
						this.translate.get('bulk-import-users.error-msg'),
					]).subscribe(([title, description]) => {
						this.title = title;
						this.description = description;
					});
					this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				}
				this.isStartAddingUsers = false;
				clearInterval(interval);
				this.list();
			}, () => { }, () => {
				this.isLoading = false;
			})
		})
	}

	setIntervalGetProcess() {
		return setInterval(() => {
			this.userService.getImportProcess(this.processId).subscribe(res => {
				this.processProgress = res.data;
				this.toolBarValue = Math.round(this.processProgress.done * 100 / this.processProgress.total);
			})
		}, 10 * 1000);
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
			modalRef.result.then(() => this.resetPassword(id), () => { });
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
			modalRef.result.then(() => this.deactivateUser(id), () => { });
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
			modalRef.result.then(() => this.activateUser(id), () => { });
		}
	}

	resetPassword(id): void {
		this.userService.resetPassword(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('reset-password.success-reset'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		}, (err) => {
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

	deactivateUser(id): void {
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

	activateUser(id): void {
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
		window.open(`http://${host}/reru-evaluation/#/user-profile/${id}/overview`, '_self')
	}

	getUserDataSheet(id: number) {
		this.isLoadingMedia = true;

		const params = {
			userProfileId: id
		}

		this.userProfileService.exportUserProfileSheet(params).subscribe((event) => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('downloand-message.succes-dosier'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.reportProggress(event);
		},
			(error) => {
				forkJoin([
					this.translate.get('modal.error'),
					this.translate.get('downloand-message.error-dosier'),
				]).subscribe(([title, description]) => {
					this.title = title;
					this.description = description;
				});
				this.notificationService.error(this.title, this.description, NotificationUtil.getDefaultMidConfig());
				this.isLoadingMedia = false;
			})
	}

	private reportProggress(httpEvent: HttpEvent<Blob>): void {
		switch (httpEvent.type) {
			case HttpEventType.Sent:
				this.fileStatus.percent = 1;
				break;
			case HttpEventType.UploadProgress:
				this.updateStatus(httpEvent.loaded, httpEvent.total, 'In Progress...')
				break;
			case HttpEventType.DownloadProgress:
				this.updateStatus(httpEvent.loaded, httpEvent.total, 'In Progress...')
				break;
			case HttpEventType.Response:
				this.fileStatus.requestType = "Done";
				this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());

				const fileName = httpEvent.headers.get('Content-Disposition').split("filename=")[1].split(';')[0].slice(1, -1);
				const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
				const file = new File([blob], fileName, { type: httpEvent.body.type });
				saveAs(file);
				this.isLoadingMedia = false;
				break;
		}
	}

	updateStatus(loaded: number, total: number | undefined, requestType: string) {
		this.fileStatus.requestType = requestType;
		this.fileStatus.percent = Math.round(100 * loaded / total);
	}

}
