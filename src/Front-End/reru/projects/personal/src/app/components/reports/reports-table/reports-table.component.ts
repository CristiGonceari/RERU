import { Component, OnInit } from '@angular/core';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import {ReportsService} from '../../../utils/services/reports.service';
import { ObjectUtil } from '../../../utils/util/object.util';
import { ReportFilterModel } from '../../../utils/models/report.model';
import { InitializerUserProfileService } from '../../../utils/services/initializer-user-profile.service';
import { FileService } from '../../../utils/services/file.service';
import { saveAs } from 'file-saver';


@Component({
  selector: 'app-reports-table',
  templateUrl: './reports-table.component.html',
  styleUrls: ['./reports-table.component.scss']
})
export class ReportsTableComponent implements OnInit {
  isLoading: boolean = true;
  reports: any[] = [];
  pagedSummary: PagedSummary = new PagedSummary();
  filter: ReportFilterModel = {};
  hasContractorId: any;

  constructor(private reportsService: ReportsService,
              private initializerUserProfileService: InitializerUserProfileService,
              private fileService: FileService) { }

  ngOnInit(): void {
    this.list();
    this.getUserContractorId();
  }

  downloadFile(item): void {
    this.fileService.get(item.id).subscribe(response => {
      const fileName = item.name;
      const blob = new Blob([response.body], { type: response.body.type });
			const file = new File([blob], fileName, { type: response.body.type });
      saveAs(file);
    });
  }

  list(data: any = {}): void {
    this.isLoading = true;
    const request = ObjectUtil.preParseObject({
      page: data.page || this.pagedSummary.currentPage,
      type: this.filter.type == 0 ? null : this.filter.type,
      contractorLastName: data.contractorLastName || this.filter.contractorLastName,
      contractorName: data.contractorName || this.filter.contractorName,
      departmentId: data.departmentId || this.filter.departmentId,
      name: data.name || this.filter.name,
      fromDate: data.fromDate || this.filter.fromDate,
      toDate: data.toDate || this.filter.toDate,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.reportsService.list(request).subscribe(response => {
      if (response.success) {
        this.reports = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  resetList(): void {
    this.filter = {};
    this.list();
  }

  getUserContractorId(){
    this.initializerUserProfileService.get().subscribe(res => {
      if(res.data) {
        this.hasContractorId = res.data.contractorId;
      }
    });
  }

}
