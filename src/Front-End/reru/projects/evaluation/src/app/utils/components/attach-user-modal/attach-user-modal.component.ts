import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { UserProfileService } from '../../services/user-profile/user-profile.service';
import { PaginationModel } from '../../models/pagination.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EventService } from '../../services/event/event.service';
import { Test } from '../../models/tests/test.model';

@Component({
  selector: 'app-attach-user-modal',
  templateUrl: './attach-user-modal.component.html',
  styleUrls: ['./attach-user-modal.component.scss']
})
export class AttachUserModalComponent implements OnInit {
  users = [];
  paginatedAttachedIds: boolean;
  pagination: PaginationModel = new PaginationModel();
  isLoading = true;
  filters = {};
  showUserName: boolean = false;
  @ViewChild('firstName') firstName: any;
  @ViewChild('lastName') lastName: any;
  @ViewChild('fatherName') fatherName: any;
  @ViewChild('idnp') idnp: any;
  @ViewChild('email') email: any;
  @Input() exceptUserIds: any;
  @Input() attachedItems: number[];
  @Input() inputType: string;
  @Input() eventId: number;
  @Input() page: string;

  constructor(
    private userService: UserProfileService,
    private activeModal: NgbActiveModal,
    private eventUserService: EventService
  ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(data: any = {}): void {
    if (this.inputType == 'checkbox' && this.page == 'add-test') this.getAssignedUsers(data);
    else {
      let exceptIds = this.exceptUserIds.length ? this.exceptUserIds : 0;
      let params = {
        page: data.page || this.pagination.currentPage,
        itemsPerPage: data.itemsPerPage || this.pagination.pageSize,
        exceptUserIds: exceptIds,
        ...this.filters
      }
      this.userService.get(params).subscribe(res => {
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

  dismiss(): void {
    this.activeModal.dismiss();
  }

  confirm(): void {
    let data = {
      attachedItems: this.attachedItems,
      showUserName: this.showUserName
    }
    this.activeModal.close(data);
  }

  checkAll(event): void {
    if (event.target.checked == false) this.attachedItems = [];
    if (event.target.checked == true) {
      let itemsToAdd = this.users.map(el => +el.id)
      for (let i=0; i<itemsToAdd.length; i++) {
        this.attachedItems.push(itemsToAdd[i]);
      }
    }
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
    this.filters[field] = value;
    this.getUsers();
	}

	resetFilters(): void {
    this.firstName.key = '';
    this.lastName.key = '';
    this.fatherName.key = '';
    this.idnp.key = '';
    this.email.key = '';
    this.filters = {};
    this.getUsers();
	}
  
  checkEvent(event): void {
    this.showUserName = event.target.checked;
  }

}
