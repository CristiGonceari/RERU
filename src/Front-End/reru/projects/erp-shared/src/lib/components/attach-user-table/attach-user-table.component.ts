import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PaginationClass, PaginationModel } from '../../models/pagination.model';
import { ResponseArray } from '../../models/response.model';
import { SelectItem } from '../../models/select-item.model';
import { UserModel } from '../../models/user.model';
import { UserProfileService } from '../../services/user-profile.service';
import { ObjectUtil } from '../../utils/util/object.util';

@Component({
  selector: 'erp-shared-attach-user-table',
  templateUrl: './attach-user-table.component.html',
  styleUrls: ['./attach-user-table.component.scss']
})
export class AttachUserTableComponent implements OnInit {
  @Input() departments: SelectItem[] = [];
  @Input() roles: SelectItem[] = [];
  @Input() userStatuses: SelectItem[] = [];
  @Input() functions: SelectItem[] = [];

  @Input() exceptUserIds: number[] = [];
  @Input() attachedUsers: number[] = [];

  @Input() inputType: 'checkbox' | 'radio' = 'checkbox';

  @Input() eventId: number;
  @Input() positionId: number;
  @Input() testTemplateId: number;

  @Output() changeAttachedUser: EventEmitter<{ attachedUsers: number[], checked: boolean }> = 
                            new EventEmitter<{ attachedUsers: number[], checked: boolean }>();

  paginatedAttachedIds: boolean = false;
  showFilters = true;

  users: UserModel[] = [];
  filters = {};

  pagination: PaginationModel = new PaginationClass();
  isLoading = true;
  constructor(private readonly userProfileService: UserProfileService,
              private readonly cd: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.getUsers();
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

  handleChangeAttachedUsers(data: { attachedUsers: number[], checked: boolean }): void {
    // check if the user exist in the list, then proceed
    if (this.users.some(user => data.attachedUsers.includes(user.id))) {
      this.changeAttachedUser.emit(data);
      if (data.checked) {
        this.attachedUsers =  [...data.attachedUsers];
      } else {
        data.attachedUsers.forEach(el => {
          this.attachedUsers.splice(this.attachedUsers.indexOf(el), 1);
        })
      }
    }
  }

  setFilter(field: string, value): void {
    this.pagination.currentPage = 1;
    this.filters[field] = value;
    this.getUsers();
	}

  resetFilters(): void {
    this.pagination.currentPage = 1;
    this.showFilters = false;
    this.cd.detectChanges();
    this.filters = {};
    this.getUsers();
    this.showFilters = true;
    this.cd.detectChanges();
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

  onItemChange(event): void {
    if (event.target.checked == true) {
      this.attachedUsers.push(+event.target.value);
    } else if (event.target.checked == false) {
      this.attachedUsers.splice(this.attachedUsers.indexOf(event.target.value), 1);
    }
  }

}
