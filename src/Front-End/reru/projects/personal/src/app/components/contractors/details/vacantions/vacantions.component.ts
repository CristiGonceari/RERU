import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddVacationModalComponent } from 'projects/personal/src/app/utils/modals/add-vacation-modal/add-vacation-modal.component';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { VacationTypeEnum } from 'projects/personal/src/app/utils/models/vacation-type.enum';
import { FileService } from 'projects/personal/src/app/utils/services/file.service';
import { VacantionService } from 'projects/personal/src/app/utils/services/vacantion.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { ObjectUtil } from 'projects/personal/src/app/utils/util/object.util';
import { VacantionModel } from 'projects/personal/src/app/utils/models/vacantion.model';
import { DetailsRoutingModule } from '../details-routing.module';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { ActivatedRoute } from '@angular/router';
import { InitializerUserProfileService } from 'projects/personal/src/app/utils/services/initializer-user-profile.service';

@Component({
  selector: 'app-vacantions',
  templateUrl: './vacantions.component.html',
  styleUrls: ['./vacantions.component.scss']
})
export class VacantionsComponent implements OnInit {
  @Input() contractor: Contractor;
  contractorId: number;
  hasContractorId: number;
  vacantions: VacantionModel[] = [];
  id: number;
  private sub: any;
 
  pagedSummary: PagedSummary = new PagedSummary();

  isLoading: boolean = true;
  DetailsRoutingModule: DetailsRoutingModule[];

  constructor(
      private vacantionService: VacantionService,
      private modalService: NgbModal,
      private notificationService: NotificationsService,
      private fileService: FileService,
      private route: ActivatedRoute,
      private initializerUserProfileService: InitializerUserProfileService
  ) { }

  ngOnInit(): void {
    this.subscribeForParams();
    this.getUserContractorId();
  }

  subscribeForParams(): void {
    this.route.parent.params.subscribe(response => {
      if (response.id) {
        this.contractorId = response.id;
        this.list({contractorId: response.id});
        return;
      }
    });
  }

  getUserContractorId(){
    this.initializerUserProfileService.get().subscribe(res => {
      if(res.data) {
        this.hasContractorId = res.data.contractorId;
      }
    });
  }

  openAddVacationModal(): void {
    const modalRef = this.modalService.open(AddVacationModalComponent, { centered: true, backdrop: 'static',  windowClass: 'my-class', scrollable: true});
    modalRef.componentInstance.id = this.contractorId;
    modalRef.result.then((response) => this.addVacation(response), () => {});
  }

  addVacation(data: VacantionModel): void {
    this.isLoading = true;
    const request: VacantionModel = ObjectUtil.preParseObject(this.parseVacation(data, this.contractorId));
    this.vacantionService.create(request).subscribe(response => {
     this.list();
      this.notificationService.success('Success', 'Vacation added!', NotificationUtil.getDefaultConfig());
    }, () => {}, () => {
      this.isLoading = false;
    });
  }

  parseVacation(data, contractorId): VacantionModel {
    const request = {
      ...data,
      vacationType: data.vacationType ? +data.vacationType : null,
      contractorId
    }

    if (+data.vacationType === VacationTypeEnum.ChildCare) {
      request.childAge = request.childAge ? +request.childAge : null
    }

    return request;
  }

  list(data: any = {}): void {
    const request = ObjectUtil.preParseObject({
      contractorId: this.contractorId,
      page: data.page || this.pagedSummary.currentPage,
      itemPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.vacantionService.get(request).subscribe((response: any) => {
      this.vacantions = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    });
  }

  download(id: number): void {
    this.fileService.download(id);
  }
}
