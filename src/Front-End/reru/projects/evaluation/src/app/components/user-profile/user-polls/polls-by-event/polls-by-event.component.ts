import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyPollStatusEnum } from 'projects/evaluation/src/app/utils/enums/my-poll-status.enum';
import { TestStatusEnum } from 'projects/evaluation/src/app/utils/enums/test-status.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';

@Component({
  selector: 'app-polls-by-event',
  templateUrl: './polls-by-event.component.html',
  styleUrls: ['./polls-by-event.component.scss']
})
export class PollsByEventComponent implements OnInit {
  polls = [];
  @Input() id: number;
  isLoading: boolean = true;
  pagedSummary: PaginationModel = new PaginationModel();
  enum = MyPollStatusEnum;
  testEnum = TestStatusEnum;
  date;
  userId;

  constructor(private testService: TestService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.date = new Date().toISOString();
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.parent.params.subscribe(params => {
      if (params.id && this.id) {
        this.userId = params.id;
        this.getPolls();
      }
    });
  }

  getPolls(data: any = {}){
    const params: any = {
      eventId: this.id,
      userId: this.userId,
      page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUsersPollsByEvent(params).subscribe(
      (res) => {
          this.polls = res.data.items;
          this.pagedSummary = res.data.pagedSummary;
          this.isLoading = false;
      }
    )
  }
}
