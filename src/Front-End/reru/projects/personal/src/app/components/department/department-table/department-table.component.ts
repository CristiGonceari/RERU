import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteDepartmentModalComponent } from '../../../utils/modals/delete-department-modal/delete-department-modal.component';
import { DepartmentModel } from '../../../utils/models/department.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';

@Component({
  selector: 'app-department-table',
  templateUrl: './department-table.component.html',
  styleUrls: ['./department-table.component.scss']
})
export class DepartmentTableComponent implements OnInit {
  isLoading: boolean = true;
  departments: DepartmentModel[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  constructor(private departmentService: DepartmentService,
    private modalService: NgbModal,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = ObjectUtil.preParseObject({
      page: data.page,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize,
    });
    this.departmentService.list(request).subscribe(response => {
      if (response.success) {
        this.departments = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  openConfirmationDeleteModal(department: DepartmentModel): void {
    const modalRef: any = this.modalService.open(DeleteDepartmentModalComponent, { centered: true });
    modalRef.componentInstance.name = department.name;
    modalRef.result.then(() => this.delete(department.id), () => { });
  }

  delete(id: number): void {
    this.isLoading = true;
    this.departmentService.delete(id).subscribe(response => {
      this.list();
      this.notificationService.success('Success', 'Department deleted!', NotificationUtil.getDefaultMidConfig());
    });
  }

}
