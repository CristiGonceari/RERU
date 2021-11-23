import { Component, OnInit } from '@angular/core';
import { UserProfileService } from '../../../utils/services/user-profile.service';
import { PaginationSummary } from '../../../utils/models/pagination-summary.model';
import { User } from '../../../utils/models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ObjectUtil } from '../../../utils/util/object.util';
import { ConfirmModalComponent, PermissionCheckerService } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { Location } from '@angular/common';
import { UserService } from '../../../utils/services/user.service';

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
	viewDetails: boolean = false;
	filter = {
		sort: 'name',
		order: true
	}

	isLoading = true;
	module: any;
	pagedSummary = {
		totalCount: 0,
		pageSize: 0,
		currentPage: 1,
		totalPages: 1,
	};
	userState: number = 0;
	searchValue: string;

	constructor(
		private route: ActivatedRoute,
		private userProfileService: UserProfileService,
		private router: Router,
		public permissionService: PermissionCheckerService,
		private modalService: NgbModal,
		private notificationService: NotificationsService,
		private location: Location,
    	private userService: UserService,
	) {}

	ngOnInit(): void {
		this.list();
		this.prepareFilter('name', 'asc');
		this.checkPermission();
	}

	list(data: any = {}): void {
		data = {
			...data,
			page: data.page || this.pagedSummary.currentPage,
			keyword: data.keyword || this.searchValue,
			status: this.userState
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

	prepareFilter(sort, order): void {
		if (sort === this.filter.sort) {
			this.filter.order = !this.filter.order;
			return
		}
		if (sort !== this.filter.sort) {
			this.filter.sort = sort;
			this.filter.order = false;
		}
	}

	sortedList(data: any = {}): void {
		this.prepareFilter(data.sort, data.order);
		data = {
			...data,
			keyword: this.keyword,
			order: this.filter.order ? 'desc' : 'asc',
			sort: this.filter.sort,
			page: data.page || this.pagedSummary.currentPage,
			status: this.userState
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
		if (!user || !user.name || !user.lastName) {
			return '';
		}

		return `${user.lastName[0].toUpperCase()}${user.name[0].toUpperCase()}`;
	}

	navigateToDetails(id): void {
		this.router.navigate(['../../user-profile', id, 'overview'], {relativeTo: this.route});
	}

	checkPermission(): void {
		if (this.permissionService.isGranted('P00000020')) {
			this.viewDetails = true;
		}
	}

	openConfirmModal(id: number, name, lastName, type): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		if (type == 'reset-password') {
			modalRef.componentInstance.title = 'Reset';
			modalRef.componentInstance.description = `Are you sure you want to reset password for user ${name} ${lastName}?`;
			modalRef.result.then(() => this.resetPassword(id, name, lastName), () => {});
		}
		if(type == 'deactivate-user'){
			modalRef.componentInstance.title = 'Deactivate user';
			modalRef.componentInstance.description = `Are you sure you want to deactivate ${name} ${lastName}?`;
			modalRef.result.then(() => this.deactivateUser(id, name, lastName), () => {});
		}
		if(type == 'activate-user'){
			modalRef.componentInstance.title = 'Activate user';
			modalRef.componentInstance.description = `Are you sure you want to activate ${name} ${lastName}?`;
			modalRef.result.then(() => this.activateUser(id, name, lastName), () => {});
		}
	}
	
	resetPassword(id, name, lastName): void {
		this.userService.resetPassword(id).subscribe(
		  (res) => {
			this.notificationService.success('Success',
			  `Password for ${name} ${lastName} has been reset successfully!`,
			  NotificationUtil.getDefaultMidConfig()
			);
			if(res){ this.list(); }
		  },
		  (err) => {
			this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
		  },
		);
	}

	deactivateUser(id, name, lastName): void {
		this.userService.deactivateUser(id).subscribe((res) => {
				this.notificationService.success(
					'Success',
					`User ${name} ${lastName} has been deactivated successfully!`,
					NotificationUtil.getDefaultMidConfig()
				);
				if(res){ this.list(); }
			}
		);
	}

	activateUser(id, name, lastName): void {
    	this.userService.activateUser(id).subscribe((res) => {
        		this.notificationService.success(
					'Success', 
          			`User ${name} ${lastName} has been activated successfully!`, 
		  			NotificationUtil.getDefaultMidConfig()
				);
				if(res){ this.list(); }
      		},
      (err) => {
        this.notificationService.error('Errror', 'An error occured!', NotificationUtil.getDefaultMidConfig());
      }
    );
  }

}
