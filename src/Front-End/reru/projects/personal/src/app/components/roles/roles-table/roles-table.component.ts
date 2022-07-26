import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '../../../utils/modals/confirm-modal/confirm-modal.component';
import { ImportRoleModalComponent } from '../../../utils/modals/import-role-modal/import-role-modal.component';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { RoleModel } from '../../../utils/models/role.model';
import { RoleService } from '../../../utils/services/role.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ObjectUtil } from '../../../utils/util/object.util';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-roles-table',
  templateUrl: './roles-table.component.html',
  styleUrls: ['./roles-table.component.scss']
})
export class RolesTableComponent implements OnInit {
  isLoading: boolean = true;
  roles: RoleModel[] = [];
  pagedSummary: PagedSummary = new PagedSummary();
  searchWord: string;
  constructor(private roleService: RoleService,
    private modalService: NgbModal,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    this.isLoading = true;
    if (data.hasOwnProperty('searchWord')) this.searchWord = data.searchWord;
    if (data.hasOwnProperty('searchBy') && data.searchBy !== 'null') {
      data[data.searchBy] = this.searchWord;
    }
    data = ObjectUtil.preParseObject({
      ...data,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage:data.itemsPerPage || this.pagedSummary.pageSize,
      searchWord: (!data.searchBy || data.searchBy === 'null') && (data.searchWord || this.searchWord),
      searchBy: null
    });
    this.roleService.list(data).subscribe(response => {
      if (response.success) {
        this.roles = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  // openConfirmationDeleteModal(id: number): void {
  //   const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
  //   modalRef.componentInstance.title = 'Delete';
  //   modalRef.componentInstance.description = 'Are you sure you want to delete it?';
  //   modalRef.result.then(() => this.delete(id), () => { });
  // }

  // delete(id: number): void {
  //   this.isLoading = true;
  //   this.roleService.delete(id).subscribe(response => {
  //     this.list();
  //     this.notificationService.success('Success', 'Role deleted!', NotificationUtil.getDefaultMidConfig());
  //   });
  // }

  openImportModal(): void {
    const modalRef: any = this.modalService.open(ImportRoleModalComponent, { centered: true, backdrop: 'static', size: 'lg' });
    modalRef.result.then((data) => this.importRoles(data), () => {});
  }

  importRoles(data): void {
    this.isLoading = true;
    const form = new FormData();
    form.append('file', data.file);
    this.roleService.bulkImport(form).subscribe(response => {
			if(response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
			}
      this.notificationService.success('Success', 'Roles imported!', NotificationUtil.getDefaultMidConfig());
      this.list();
    }, () => {}, () => {
      this.isLoading = false;
    })
  }
}
