import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { DeleteDepartmentModalComponent } from '../../../utils/modals/delete-department-modal/delete-department-modal.component';
import { DepartmentModel } from '../../../utils/models/department.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { DepartmentService } from '../../../utils/services/department.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { ImportDepartmentsModalComponent } from '../../../utils/modals/import-departments-modal/import-departments-modal.component';
import { saveAs } from 'file-saver';
import { forkJoin } from 'rxjs';
import { I18nService } from '@erp/shared';

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
  
  title: string;
  description: string

  constructor(private departmentService: DepartmentService,
    private modalService: NgbModal,
    private notificationService: NotificationsService,
    public translate: I18nService,
    ) { }

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

  openImportDepartmentsModal(){
    const modalRef: any = this.modalService.open(ImportDepartmentsModalComponent, { centered: true });
    modalRef.result.then((data) => this.import(data), () => { });
  }

  import(data): void {
		this.isLoading = true;
		const form = new FormData();
		form.append('File', data.file);
		this.departmentService.bulkImport(form).subscribe(response => {
			if(response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
			}
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('departments.bulk-import-departments-succes'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		}, () => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('departments.bulk-import-departments-error'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
     }, () => {
			this.isLoading = false;
		})
	}

  // openConfirmationDeleteModal(department: DepartmentModel): void {
  //   const modalRef: any = this.modalService.open(DeleteDepartmentModalComponent, { centered: true });
  //   modalRef.componentInstance.name = department.name;
  //   modalRef.result.then(() => this.delete(department.id), () => { });
  // }

  // delete(id: number): void {
  //   this.isLoading = true;
  //   this.departmentService.delete(id).subscribe(response => {
  //     this.list();
  //     this.notificationService.success('Success', 'Department deleted!', NotificationUtil.getDefaultMidConfig());
  //   });
  // }

}
