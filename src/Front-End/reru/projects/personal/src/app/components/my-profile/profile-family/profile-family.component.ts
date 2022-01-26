import { Component, Input, OnInit } from '@angular/core';
import { Contractor } from '../../../utils/models/contractor.model';
import { FamilyModel } from '../../../utils/models/family.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';

@Component({
  selector: 'app-profile-family',
  templateUrl: './profile-family.component.html',
  styleUrls: ['./profile-family.component.scss']
})
export class ProfileFamilyComponent implements OnInit {
  @Input() contractor: Contractor;
  families: FamilyModel[];
  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    currentPage: 1,
    pageSize: 10
  };
  isLoading: boolean = true;
  constructor(private contractorProfileService: ContractorProfileService) { }

  ngOnInit(): void {
    this.retrieveFamilies();
  }

  retrieveFamilies(): void {
    this.contractorProfileService.getFamilyMember().subscribe(response => {
      this.families = response.data.items;
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    })
  }
}
