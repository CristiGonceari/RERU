import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestResultStatusEnum } from '../../../utils/enums/test-result-status.enum';
import { TestStatusEnum } from '../../../utils/enums/test-status.enum';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { TestService } from '../../../utils/services/test/test.service';

@Component({
  selector: 'app-user-evaluated-tests',
  templateUrl: './user-evaluated-tests.component.html',
  styleUrls: ['./user-evaluated-tests.component.scss']
})
export class UserEvaluatedTestsComponent implements OnInit {
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

    this.testService.getUserEvaluatedTests(params).subscribe(
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
