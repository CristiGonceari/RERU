import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { AddRankModalComponent } from 'projects/personal/src/app/utils/modals/add-rank-modal/add-rank-modal.component';
import { DeleteRankModalComponent } from 'projects/personal/src/app/utils/modals/delete-rank-modal/delete-rank-modal.component';
import { EditRankModalComponent } from 'projects/personal/src/app/utils/modals/edit-rank-modal/edit-rank-modal.component';
import { PagedSummary } from 'projects/personal/src/app/utils/models/paged-summary.model';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { RankModel } from '../../../../utils/models/rank.model';
import { RankService } from '../../../../utils/services/rank.service';

@Component({
  selector: 'app-ranks',
  templateUrl: './ranks.component.html',
  styleUrls: ['./ranks.component.scss']
})
export class RanksComponent implements OnInit {
  ranks: RankModel[];
  isLoading: boolean = true;
  pagedSummary: PagedSummary = {
    pageSize: 10,
    currentPage: 1,
    totalCount: 0,
    totalPages: 0
  };
  contractorId: number;
  constructor(private route: ActivatedRoute,
              private rankService: RankService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.parent.params.subscribe(response => {
      this.contractorId = response.id;
      this.retriveRanks({contractorId: response.id});
    });
  }

  retriveRanks(data: any = {}): void {
    const request = {
      contractorId: this.contractorId,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize 
    }

    this.rankService.list(request).subscribe((response: any) => {
      this.isLoading = false;
      this.ranks = response.data.items;
    });
  }

  openAddRankModal(): void {
    const modalRef = this.modalService.open(AddRankModalComponent);
    modalRef.componentInstance.contractorId = this.contractorId;
    modalRef.result.then((response: RankModel) => this.addRank(response), () => {})
  }

  addRank(data: RankModel): void {
    this.isLoading = true;
    this.rankService.create(data).subscribe(response => {
      this.retriveRanks();
      this.notificationService.success('Success', 'Rank added!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  openEditRankModal(rank: RankModel): void {
    const modalRef = this.modalService.open(EditRankModalComponent);
    modalRef.componentInstance.contractorId = this.contractorId;
    modalRef.componentInstance.rank = {...rank};
    modalRef.result.then((response: RankModel) => this.editRank(response), () => {})
  }

  editRank(data: RankModel): void {
    this.rankService.update(data).subscribe(response => {
      this.retriveRanks();
      this.notificationService.success('Success', 'Rank edited!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }

  openDeleteRankModal(rank: RankModel): void {
    const modalRef = this.modalService.open(DeleteRankModalComponent);
    modalRef.componentInstance.name = rank.rankRecordName;
    modalRef.result.then(() => this.deleteRank(rank.id), () => {})
  }

  deleteRank(id: number): void {
    this.rankService.delete(id).subscribe(response => {
      this.retriveRanks();
      this.notificationService.success('Success', 'Rank deleted!', NotificationUtil.getDefaultConfig());
    }, () => {
      this.isLoading = false;
    });
  }
}
