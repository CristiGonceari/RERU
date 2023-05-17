import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { UserProfileService } from '../../services/user-profile/user-profile.service';
import { PaginationModel } from '../../models/pagination.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EventService } from '../../services/event/event.service';
import { SearchStatusComponent } from './search-status/search-status.component';
import { SearchRoleComponent } from './../search-role/search-role.component';
import { SearchDepartmentComponent } from './../search-department/search-department.component';
import { SearchEmployeeFunctionComponent } from '../search-employee-function/search-employee-function.component';

@Component({
  selector: 'app-attach-user-modal',
  templateUrl: './attach-user-modal.component.html',
  styleUrls: ['./attach-user-modal.component.scss']
})
export class AttachUserModalComponent implements OnInit {
  users = [];
  paginatedAttachedIds: boolean = false;
  pagination: PaginationModel = new PaginationModel();
  isLoading = true;
  filters = {};
  showUserName: boolean = true;
  @ViewChild('firstName') firstName: any;
  @ViewChild('lastName') lastName: any;
  @ViewChild('fatherName') fatherName: any;
  @ViewChild('idnp') idnp: any;
  @ViewChild('email') email: any;
  @ViewChild('colaboratorId') colaboratorId: any;
  @ViewChild(SearchStatusComponent) userStatusEnum: SearchStatusComponent;
  @ViewChild(SearchRoleComponent) roleId: SearchRoleComponent;
  @ViewChild(SearchDepartmentComponent) departmentId: SearchDepartmentComponent;
  @ViewChild(SearchEmployeeFunctionComponent) functionId: SearchEmployeeFunctionComponent;
  @Input() exceptUserIds: any;
  @Input() attachedItems: number[];
  @Input() inputType: string;
  @Input() eventId: number;
  @Input() positionId: number;
  @Input() page: string;
  @Input() whichUser: boolean;
  @Input() testTemplateId: number;
  showEventCard: boolean = false;

  constructor(
    private userService: UserProfileService,
    private activeModal: NgbActiveModal,
    private eventUserService: EventService
  ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(data: any = {}): void {
    if (this.eventId && this.positionId == null && (this.page == 'add-test' || this.page == 'add-evaluation') && !this.whichUser) this.getAssignedUsers(data);
    else if (this.eventId && this.positionId == null && (this.page == 'add-test' || this.page == 'add-evaluation') && this.whichUser) this.getAssignedEvaluators(data);
    else this.getAllUsers(data);
  }

  getAllUsers(data: any = {}){
    let exceptIds = this.exceptUserIds.length ? this.exceptUserIds : 0;
    this.paginatedAttachedIds = false;
    let params = {
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      exceptUserIds: exceptIds,
      eventId: this.eventId && this.positionId ? this.eventId : null,
      positionId: this.eventId && this.positionId ? this.positionId : null,
      testTemplateId: this.testTemplateId || null,
      ...this.filters
    }
    if(this.testTemplateId == null){

      this.userService.get(params).subscribe(res => {
        if (res && res.data) {
          this.paginatedAttachedIds = res.data.items.map(el => el.id).some(r=> this.attachedItems.includes(r))
          this.users = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
        }
      })
    } else {
      this.userService.getByTestTemplate(params).subscribe(res => {
        if (res && res.data) {
          this.paginatedAttachedIds = res.data.items.map(el => el.id).some(r=> this.attachedItems.includes(r))
          this.users = res.data.items;
          this.pagination = res.data.pagedSummary;
          this.isLoading = false;
        }
      })
    }
  }

  getAssignedUsers(data: any = {}): void {
    let params = {
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      eventId: this.eventId,
      ...this.filters
    }
    this.eventUserService.getAssignedUsers(params).subscribe(res => {
      if (res && res.data) {
        this.users = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;
      }
    })
  }

  getAssignedEvaluators(data: any = {}): void {
    let params = {
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
      eventId: this.eventId,
      testTemplateId: this.testTemplateId || null,
      ...this.filters
    }
    this.eventUserService.getAssignedEvaluators(params).subscribe(res => {
      if (res && res.data) {
        this.users = res.data.items;
        this.pagination = res.data.pagedSummary;
        this.isLoading = false;
      }
    })
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  confirm(): void {
    let data = {
      attachedItems: this.attachedItems,
      // showUserName: this.showUserName
    }
    this.activeModal.close(data);
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
    this.onItemChange(event);
    // else this.attachedItems[0] = +event.target.value;
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
    this.firstName.value = '';
    this.lastName.value = '';
    this.fatherName.value = '';
    this.idnp.value = '';
    this.email.value = '';
    this.colaboratorId.value = '';
    this.departmentId.department = '';
    this.roleId.role = '';
    this.functionId.function = '';
    this.userStatusEnum.userStatus = '';
    this.filters = {};
    this.getUsers();
	}
  
  checkEvent(event): void {
    this.showUserName = event.target.uchecked;
  }
}
