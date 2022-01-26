import { Component, OnInit } from '@angular/core';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { RankModel } from '../../../utils/models/rank.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';

@Component({
  selector: 'app-profile-ranks',
  templateUrl: './profile-ranks.component.html',
  styleUrls: ['./profile-ranks.component.scss']
})
export class ProfileRanksComponent implements OnInit {
  ranks: RankModel[];
  isLoading: boolean = true;
  pagedSummary: PagedSummary = {
    pageSize: 10,
    totalPages: 1,
    currentPage: 1,
    totalCount: 1
  };
  constructor(private contractorProfileService: ContractorProfileService) { }

  ngOnInit(): void {
    this.retrieveRanks();
  }

  retrieveRanks(): void {
    this.contractorProfileService.getRanks().subscribe(response => {
      this.ranks = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

}
