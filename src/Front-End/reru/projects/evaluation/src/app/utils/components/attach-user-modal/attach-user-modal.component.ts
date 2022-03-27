import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { UserProfileService } from '../../services/user-profile/user-profile.service';
import { UserProfile } from '../../models/user-profiles/user-profile.model';
import { PaginationModel } from '../../models/pagination.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-attach-user-modal',
  templateUrl: './attach-user-modal.component.html',
  styleUrls: ['./attach-user-modal.component.scss']
})
export class AttachUserModalComponent implements OnInit {
  users: UserProfile;
  pagination: PaginationModel = new PaginationModel();
  isLoading = true;
  filters = {};
  @ViewChild('firstName') firstName: any;
  @ViewChild('lastName') lastName: any;
  @ViewChild('patronymic') patronymic: any;
  @ViewChild('idnp') idnp: any;
  @ViewChild('email') email: any;
  @Input() exceptUserIds: any;
  @Input() attachedItems: number[];

  constructor(private userService: UserProfileService, private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    let exceptIds = this.exceptUserIds.length ? this.exceptUserIds : 0;
    let params = {
      page: this.pagination.currentPage,
      itemsPerPage: this.pagination.pageSize,
      exceptUserIds: exceptIds,
      ...this.filters
    }
    this.userService.get(params).subscribe(res => {
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
    this.activeModal.close(this.attachedItems);
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
    this.patronymic.key = '';
    this.idnp.key = '';
    this.email.key = '';
    this.filters = {};
    this.getUsers();
	}

}
