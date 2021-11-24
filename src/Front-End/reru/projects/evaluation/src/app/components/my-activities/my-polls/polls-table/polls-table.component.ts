import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { MyPollStatusEnum } from '../../../../utils/enums/my-poll-status.enum';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
 
@Component({
  selector: 'app-polls-table',
  templateUrl: './polls-table.component.html',
  styleUrls: ['./polls-table.component.scss']
})
export class PollsTableComponent implements OnInit {
  polls = [];
  @Input() id: number;
  isLoading: boolean = true;
  enum = MyPollStatusEnum;
  testEnum = TestStatusEnum;
  date: any;
  pagedSummary: PaginationModel = new PaginationModel();

  constructor(private testService: TestService, private router: Router, private route: ActivatedRoute,
    ) { }

  ngOnInit(): void {
    
    this.date = new Date().toISOString();
    if(this.id) this.getPolls();
  }

  getPolls(data: any = {}){
    const params: any = {
      eventId: this.id,
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getUserPollsByEvent(params).subscribe(
      (res) => {
          this.polls = res.data.items;
          this.pagedSummary = res.data.pagedSummary;
          this.isLoading = false;
      }
    )
  }

  createPoll(check: boolean, testTypeId?) {
    this.testService.createMinePoll({testTypeId: testTypeId, eventId: this.id}).subscribe((res) => {
      if(check)
        this.router.navigate(['../../polls/start-poll-page', res.data], { relativeTo: this.route });
      else 
       this.router.navigate(['../../polls/performing-poll', res.data], { relativeTo: this.route });
    });
  }
}
