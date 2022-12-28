import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { UserProfileService } from '../../services/user-profile.service';
import { PaginationModel } from '@utils';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SearchStatusComponent } from './search-status/search-status.component';
import { SearchRoleComponent } from './search-role/search-role.component';
import { SearchDepartmentComponent } from './search-department/search-department.component';
import { ObjectUtil } from '../../util/object.util';
import { UserModel } from '@utils';
import { PaginationClass } from '../../models/pagination.model';

export interface AttachUserModel {
  selectedUsers: number[];
}

@Component({
  selector: 'app-attach-user-modal',
  templateUrl: './attach-user-modal.component.html',
  styleUrls: ['./attach-user-modal.component.scss']
})
export class AttachUserModalComponent implements OnInit {
  users: UserModel[] = [];
  paginatedAttachedIds: boolean = false;
  pagination: PaginationModel = new PaginationClass();
  isLoading = true;
  filters = {};
  @ViewChild('firstName') firstName: any;
  @ViewChild('lastName') lastName: any;
  @ViewChild('fatherName') fatherName: any;
  @ViewChild('idnp') idnp: any;
  @ViewChild('email') email: any;
  @ViewChild(SearchStatusComponent) userStatusEnum: SearchStatusComponent;
  @ViewChild(SearchRoleComponent) roleId: SearchRoleComponent;
  @ViewChild(SearchDepartmentComponent) departmentId: SearchDepartmentComponent;
  @Input() exceptUserIds: any;
  @Input() attachedItems: number[] = [];
  @Input() inputType: string;
  @Input() eventId: number;
  @Input() positionId: number;
  @Input() page: string;
  @Input() whichUser: boolean;
  @Input() testTemplateId: number;
  showEventCard: boolean = false;

  constructor(
    private readonly userProfileService: UserProfileService,
    private readonly activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(data: any = {}): void {
    this.paginatedAttachedIds = false;
    const params = ObjectUtil.preParseObject({
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      exceptUserIds: this.exceptUserIds.length ? this.exceptUserIds : 0,
      eventId: this.eventId && this.positionId ? this.eventId : null,
      positionId: this.eventId && this.positionId ? this.positionId : null,
      testTemplateId: this.testTemplateId || null,
      ...this.filters
    });
    this.userProfileService.get(params).subscribe(res => {
      if (res && res.data) {
        this.users = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;
      }
    }, () => {
      this.isLoading = false;
    })
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  confirm(): void {
    this.activeModal.close({
      selectedUsers: this.attachedItems,
    });
  }

  checkAll(event): void {
    if (event.target.checked == false) {
      let itemsToRemove = this.users.map(el => +el.id);
      this.attachedItems = this.attachedItems.filter( x => !itemsToRemove.includes(x) );
    }
    if (event.target.checked == true) {
      this.attachedItems = this.attachedItems.filter( x => !this.users.map(el => +el.id).includes(x) );
      let itemsToAdd = this.users.map(el => +el.id)
      for (let i=0; i<itemsToAdd.length; i++) {
        this.attachedItems.push(itemsToAdd[i]);
      }
    }
    this.getUsers();
  }

  checkInput(event): void {
    if (this.inputType == 'checkbox') this.onItemChange(event);
    else this.attachedItems[0] = +event.target.value;
  }

  onItemChange(event): void {
    if (event.target.checked == true) {
      this.attachedItems.push(+event.target.value);
    } else if (event.target.checked == false) {
      let indexToDelete = this.attachedItems.findIndex(x => x == event.target.value);
      this.attachedItems.splice(indexToDelete, 1);
    }
  }

  setFilter(field: string, value): void {
    this.pagination.currentPage = 1;
    this.filters[field] = value;
    this.getUsers();
	}

	resetFilters(): void {
    this.pagination.currentPage = 1;
    this.firstName.key = '';
    this.lastName.key = '';
    this.fatherName.key = '';
    this.idnp.key = '';
    this.email.key = '';
    this.departmentId.department = '';
    this.roleId.role = '';
    this.userStatusEnum.userStatus = '';
    this.filters = {};
    this.getUsers();
	}
}
