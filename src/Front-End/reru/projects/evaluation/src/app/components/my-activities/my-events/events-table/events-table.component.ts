import { Component, Input, OnInit } from '@angular/core';
import { TestResultStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-result-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Test } from 'projects/evaluation/src/app/utils/models/tests/test.model';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';

@Component({
  selector: 'app-events-table',
  templateUrl: './events-table.component.html',
  styleUrls: ['./events-table.component.scss']
})
export class EventsTableComponent implements OnInit {
  tests = [];
  @Input() id: number;
  userId: number;
  pagedSummary: PaginationModel = new PaginationModel();
  isLoading: boolean = true;
  enum = TestStatusEnum;
  resultEnum = TestResultStatusEnum;

  constructor(private testService: TestService) { }

  ngOnInit(): void {
    if(this.id) this.getTests();
  }

  getTests(data: any = {}) {
    let params = {
      eventId: this.id,
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		}

    this.testService.getUserTestsByEvent(params).subscribe( res => {
      this.tests = res.data.items;
      this.pagedSummary = res.data.pagedSummary;
      this.isLoading = false;
    })
  }
}
