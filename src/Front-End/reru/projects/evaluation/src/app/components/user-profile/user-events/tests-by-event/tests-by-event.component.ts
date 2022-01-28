import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';

@Component({
  selector: 'app-tests-by-event',
  templateUrl: './tests-by-event.component.html',
  styleUrls: ['./tests-by-event.component.scss']
})
export class TestsByEventComponent implements OnInit {
  tests = [];
  @Input() id: number;
  userId: number;
  pagedSummary: PaginationModel = new PaginationModel();
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
      if (params.id && this.id) {
        this.userId = params.id;
        this.getTests();
      }
    });
  }

  getTests(data: any = {}){
    const params: any = {
      eventId: this.id,
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUsersTestsByEvent(params).subscribe(
      res => {
          this.tests = res.data.items;
          this.pagedSummary = res.data.pagedSummary;
          this.isLoading = false;
      }
    )
  }
}
