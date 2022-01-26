import { Component, Input, OnInit } from '@angular/core';
import { Contractor } from '../../../utils/models/contractor.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { PositionModel } from '../../../utils/models/position.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';
import { FileService } from '../../../utils/services/file.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-profile-positions',
  templateUrl: './profile-positions.component.html',
  styleUrls: ['./profile-positions.component.scss']
})
export class ProfilePositionsComponent implements OnInit {
  @Input() contractor: Contractor;
  contractorId: number;
  isLoading: boolean = true;
  positions: PositionModel;
  pagedSummary: PagedSummary = {
    pageSize: 10,
    currentPage: 1,
    totalCount: 0,
    totalPages: 0
  };
  constructor(private contractorProfileService: ContractorProfileService,
              private fileService: FileService) { }

  ngOnInit(): void {
    this.list();
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = {
      ...data,
      page: data.page || this.pagedSummary.currentPage || 1
    };
    this.contractorProfileService.getPositions(request).subscribe(response => {
      this.positions = response.data.items || [];
      this.pagedSummary = response.data.pagedSummary;
      this.isLoading = false;
    });
  }

  download(item): void {
    this.fileService.get(item.orderId).subscribe(response => {
      const fileName = item.orderName;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    })
  }
}
