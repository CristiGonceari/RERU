import { Component, ViewChild } from '@angular/core';
import { FilterUserStateComponent } from './filter-user-state/filter-user-state.component';
import { SearchStatusComponent } from './search-status/search-status.component';
import { ReferenceService } from '../../../utils/services/reference.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddUserProcessHistoryModalComponent } from '../../../utils/modals/add-user-process-history-modal/add-user-process-history-modal.component'
import { SearchDepartmentComponent } from '../../../utils/components/search-department/search-department.component';
import { SearchRoleComponent } from '../../../utils/components/search-role/search-role.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
  @ViewChild('keyword') searchKeyword: any;
  @ViewChild('email') searchEmail: any;
  @ViewChild('idnp') searchIdnp: any;
  @ViewChild(FilterUserStateComponent) userState: FilterUserStateComponent;
  @ViewChild(SearchStatusComponent) userStatusEnum: SearchStatusComponent;
  @ViewChild(SearchDepartmentComponent) departmentId: SearchDepartmentComponent;
  @ViewChild(SearchRoleComponent) roleId: SearchRoleComponent;

  title: string;
  constructor(private referenceService: ReferenceService,
    private modalService: NgbModal) { }

  interval: any;
  processesData: any;

  ngOnInit(): void {
  }

  clearFilters(): void {
    this.searchKeyword.value = '';
    this.searchEmail.value = '';
    this.searchIdnp.value = '';
    this.userState.status = '0';
    this.userStatusEnum.userStatus = '';
    this.departmentId.department = '';
    this.roleId.role = '';
  }

  getTitle(): string {
    this.title = document.getElementById('title').innerHTML;
    return this.title
  }

  openHistoryModal() {
    const modalRef: any = this.modalService.open(AddUserProcessHistoryModalComponent, { centered: true, size: 'lg', windowClass: 'my-class', scrollable: true });
    modalRef.result.then((response) => (response), () => { });
  }

}
