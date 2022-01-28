import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';

@Component({
  selector: 'app-tests-table',
  templateUrl: './tests-table.component.html',
  styleUrls: ['./tests-table.component.scss']
})
export class TestsTableComponent implements OnInit {
  testRowList: [] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  userId: number;
  isLoading: boolean = true;
  enum = TestStatusEnum;
  resultEnum = TestResultStatusEnum;

  constructor(private testService: TestService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      if (params.id) {
        this.userId = params.id;
        this.getUserTests();
      }
    });
  }

  getUserTests(data: any = {}) {
    const params: any = {
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUsersTests(params).subscribe(
      res => {
        if (res && res.data) {
          this.testRowList = res.data.items;
          this.pagedSummary = res.data.pagedSummary;
          this.isLoading = false;
        }
      }
    )
  }
}
