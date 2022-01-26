import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { VacationProfileModel } from '../../../utils/models/vacation-profile.model';
import { VacationProfileService } from '../../../utils/services/vacation-profile.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { AddVacationModalComponent } from '../../../utils/modals/add-vacation-modal/add-vacation-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { FileService } from '../../../utils/services/file.service';
import { VacationTypeEnum } from '../../../utils/models/vacation-type.enum';

@Component({
  selector: 'app-my-vacations',
  templateUrl: './my-vacations.component.html',
  styleUrls: ['./my-vacations.component.scss']
})
export class MyVacationsComponent implements OnInit {
  vacations: VacationProfileModel[] = [];
  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    pageSize: 10,
    currentPage: 1
  }
  isLoading: boolean = true;
  constructor(private vacationProfileService: VacationProfileService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private fileService: FileService) { }

  ngOnInit(): void {
    this.list();
  }

  openAddVacationModal(): void {
    const modalRef = this.modalService.open(AddVacationModalComponent,  { centered: true, backdrop: 'static',  windowClass: 'my-class', scrollable: true});
    modalRef.result.then((response) => this.addVacation(response), () => {});
  }

  addVacation(data: VacationProfileModel): void {
    this.isLoading = true;
    const request: VacationProfileModel = ObjectUtil.preParseObject(this.parseVacation(data));
    this.vacationProfileService.create(request).subscribe(response => {
      this.list();
      this.notificationService.success('Success', 'Vacation added!', NotificationUtil.getDefaultConfig());
    }, () => {}, () => {
      this.isLoading = false;
    });
  }

  parseVacation(data): VacationProfileModel {
    const request = {
      ...data,
      vacationType: data.vacationType ? +data.vacationType : null,
    }

    if (+data.vacationType === VacationTypeEnum.ChildCare) {
      request.childAge = request.childAge ? +request.childAge : null
    }

    return request;
  }

  list(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      pageSize: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.vacationProfileService.get(request).subscribe((response: any) => {
      this.vacations = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    });
  }

  download(id: number): void {
    this.fileService.download(id);
  }

}
