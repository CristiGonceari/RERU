import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TestResultStatusEnum } from '../../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { Test } from '../../../../utils/models/tests/test.model';
import { TestTypeService } from 'projects/evaluation/src/app/utils/services/test-type/test-type.service';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';


@Component({
  selector: 'app-tests-table',
  templateUrl: './tests-table.component.html',
  styleUrls: ['./tests-table.component.scss']
})
export class TestsTableComponent implements OnInit {testRowList: Test[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  isLoading: boolean = true;
  enum = TestStatusEnum;
  resultEnum = TestResultStatusEnum;
  testDto = new Test();
  settings: any;

  constructor(private testService: TestService,
    private testTypeService: TestTypeService,
    private router: Router) { }

  ngOnInit(): void {
    this.getUserTests();
  }

  getUserTests(data: any = {}) {
    let params = {
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUserTestsWithoutEvent(params).subscribe(
      res => {
        if (res && res.data) {
          this.testRowList = res.data.items;
          this.isLoading = false;
          this.pagedSummary = res.data.pagedSummary;
        }
      }
    )
  }

  getTestById(testId: number) {
    this.testService.getTest({ id: testId }).subscribe(res => {
      this.testId = res.data.id; 
      this.getTestType(res.data.testTypeId); 
    })
  }

  testId;

  getTestType(testTypeId) {
    this.testTypeService.getTestTypeSettings({ testTypeId: testTypeId }).subscribe(res => {
      this.settings = res.data;
      this.goToTest();
    })
  }

  goToTest(): void {
    if (this.settings.showManyQuestionPerPage)
      this.router.navigate(['run-test-questions', this.testId]);
    else
      this.router.navigate(['run-test', this.testId]);
  }

}
