import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddFamilyModalComponent } from 'projects/personal/src/app/utils/modals/add-family-modal/add-family-modal.component';
import { DeleteFamilyModalComponent } from 'projects/personal/src/app/utils/modals/delete-family-modal/delete-family-modal.component';
import { EditFamilyModalComponent } from 'projects/personal/src/app/utils/modals/edit-family-modal/edit-family-modal.component';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { FamilyModel } from '../../../../utils/models/family.model';
import { FamilyService } from '../../../../utils/services/family.service';

@Component({
  selector: 'app-family',
  templateUrl: './family.component.html',
  styleUrls: ['./family.component.scss']
})
export class FamilyComponent implements OnInit {
  @Input() contractor: Contractor;
  families: FamilyModel[];
  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    currentPage: 1,
    pageSize: 10
  };
  isLoading: boolean = true;
  constructor(private familyService: FamilyService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.retrieveFamilies();
  }

  retrieveFamilies(data: any = {}): void {
    const request = {
      contractorId: this.contractor.id,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    };
    this.familyService.list(request).subscribe(response => {
      this.families = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  openAddFamilyModal(): void {
    const modalRef = this.modalService.open(AddFamilyModalComponent);
    modalRef.componentInstance.contractorId = this.contractor.id;
    modalRef.result.then((response) => this.addFamily(response), () => {});
  }

  addFamily(data: FamilyModel): void {
    this.isLoading = true;
    this.familyService.create(data).subscribe(() => {
      this.retrieveFamilies();
      this.notificationService.success('Success', 'Family member added!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  openDeleteFamilyModal(relation: FamilyModel): void {
    const modalRef = this.modalService.open(DeleteFamilyModalComponent);
    modalRef.componentInstance.name = relation.relationName;
    modalRef.result.then(() => this.deleteFamily(relation.id), () => {});
  }

  deleteFamily(id: number): void {
    this.isLoading = true;
    this.familyService.delete(id).subscribe(() => {
      this.retrieveFamilies();
      this.notificationService.success('Success', 'Family deleted!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  openEditFamilyModal(data: FamilyModel): void {
    const modalRef = this.modalService.open(EditFamilyModalComponent);
    modalRef.componentInstance.contractorId = this.contractor.id;
    modalRef.componentInstance.family = data;
    modalRef.result.then((response) => this.editFamily(response), () => {});
  }

  editFamily(data: FamilyModel): void {
    this.isLoading = true;
    this.familyService.update(data).subscribe(() => {
      this.retrieveFamilies();
      this.notificationService.success('Success', 'Family member updated!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }
}
