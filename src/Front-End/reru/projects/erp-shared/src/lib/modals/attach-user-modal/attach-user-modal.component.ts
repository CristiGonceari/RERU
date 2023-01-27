import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationClass, PaginationModel } from '../../models/pagination.model';
import { ResponseArray } from '../../models/response.model';
import { SelectItem } from '../../models/select-item.model';
import { UserModel } from '../../models/user.model';
import { UserProfileService } from '../../services/user-profile.service';
import { ObjectUtil } from '../../utils/util/object.util';

enum ModalViewEnum {
  isTable = 0,
  isOrganigram = 1
}

@Component({
  selector: 'erp-shared-attach-user-modal',
  templateUrl: './attach-user-modal.component.html',
  styleUrls: ['./attach-user-modal.component.scss']
})
export class AttachUserModalComponent implements OnInit {
  users: UserModel[] = [];
  @Input() departments: SelectItem[] = [];
  @Input() roles: SelectItem[] = [];
  @Input() userStatuses: SelectItem[] = [];
  @Input() functions: SelectItem[] = [];
  paginatedAttachedIds: boolean = false;
  pagination: PaginationModel = new PaginationClass();
  isLoading = true;
  filters = {};
  view: ModalViewEnum = ModalViewEnum.isTable;
  modalViewEnum = ModalViewEnum;
  // @ViewChild('firstName') firstName: any;
  // @ViewChild('lastName') lastName: any;
  // @ViewChild('fatherName') fatherName: any;
  // @ViewChild('idnp') idnp: any;
  // @ViewChild('email') email: any;
  // @ViewChild(SearchStatusComponent) userStatusEnum: SearchStatusComponent;
  // @ViewChild(SearchRoleComponent) roleId: SearchRoleComponent;
  // @ViewChild(SearchDepartmentComponent) departmentId: SearchDepartmentComponent;
  @Input() exceptUserIds: number[] = [];
  @Input() attachedUsers: number[] = [];

  @Input() inputType: 'checkbox' | 'radio';

  @Input() eventId: number;
  @Input() positionId: number;
  @Input() testTemplateId: number;

  constructor(private readonly userProfileService: UserProfileService,
              private readonly activeModal: NgbActiveModal,
              private readonly cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getUsers();
  }

  handleChangeAttachedUsers(data: { attachedUsers: number[], checked: boolean }): void {
    console.log('came in table', data);
    // check if the user exist in the list, then proceed
    if (this.users.some(user => data.attachedUsers.includes(user.id))) {
      if (data.checked) {
        this.attachedUsers = [...data.attachedUsers];
      } else {
        data.attachedUsers.forEach(el => {
          this.attachedUsers.splice(this.attachedUsers.indexOf(el), 1);
        })
      }
    }
  }

  public getUsers(data: any = {}): void {
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

    this.userProfileService.get(params).subscribe((response: ResponseArray<UserModel>) => {
      if (response && response.data) {
        this.users = response.data.items;
        this.pagination = response.data.pagedSummary;
        this.isLoading = false;
      }
    }, () => {
      this.isLoading = false;
    })
  }

  public dismiss(): void {
    this.activeModal.dismiss();
  }

  public confirm(): void {
    this.activeModal.close({
      selectedUsers: this.attachedUsers,
    });
  }

  public checkAll(event): void {
    if (event.target.checked == false) {
      const itemsToRemove = this.users.map(el => +el.id);
      this.attachedUsers = this.attachedUsers.filter( x => !itemsToRemove.includes(x) );
    }
    if (event.target.checked == true) {
      this.attachedUsers = this.attachedUsers.filter( x => !this.users.map(el => +el.id).includes(x) );
      const itemsToAdd = this.users.map(el => +el.id)
      for (let i = 0; i < itemsToAdd.length; i++) {
        this.attachedUsers.push(itemsToAdd[i]);
      }
    }
    this.getUsers();
  }

  /**
   * Triggers check (true/false) into attachedUsers,
   * reassigns the attachedUsers array to trigger OnChanges on TreeComponent
   * @param {Event} event Checkbox Event
   */
  checkInput(event): void {
    if (this.inputType == 'checkbox') {
      this.onItemChange(event);
    } else { 
      this.attachedUsers[0] = +event.target.value;
    }
    this.attachedUsers = [...this.attachedUsers];
  }

  onItemChange(event): void {
    if (event.target.checked == true) {
      this.attachedUsers.push(+event.target.value);
    } else if (event.target.checked == false) {
      this.attachedUsers.splice(this.attachedUsers.indexOf(event.target.value), 1);
    }
  }

  setFilter(field: string, value): void {
    this.pagination.currentPage = 1;
    this.filters[field] = value;
    this.getUsers();
	}

	resetFilters(): void {
    this.pagination.currentPage = 1;
    // this.firstName.key = '';
    // this.lastName.key = '';
    // this.fatherName.key = '';
    // this.idnp.key = '';
    // this.email.key = '';
    // this.departmentId.department = '';
    // this.roleId.role = '';
    // this.userStatusEnum.userStatus = '';
    this.filters = {};
    this.getUsers();
	}
}
