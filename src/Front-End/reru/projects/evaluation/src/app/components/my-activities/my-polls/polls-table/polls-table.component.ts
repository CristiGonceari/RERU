import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { MyPollStatusEnum } from '../../../../utils/enums/my-poll-status.enum';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { Events } from 'projects/evaluation/src/app/utils/models/calendar/events';
 
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
  countedTests: Events[] = [];
  pagedSummary: PaginationModel = new PaginationModel();
  selectedDay;
  displayDate;

  constructor(private testService: TestService, private router: Router, private route: ActivatedRoute,
    ) { }

  ngOnInit(): void {
  }

  getListByDate(data: any = {}): void {
    this.isLoading = true;

    if (data.date != null) {
      this.selectedDay = this.parseDates(data.date);
      this.displayDate = this.parseDatesForTable(data.date);
    }

    const request = {
      date: this.selectedDay,
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }

    this.testService.getMyPolls(request).subscribe(response => {
      if (response.success) {
        this.polls = response.data.items || [];
        this.pagedSummary = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  getListOfCoutedTests(data) {
    const request = {
      startTime: this.parseDates(data.fromDate),
      endTime: this.parseDates(data.tillDate)
    }

    this.testService.getMyPollsCount(request).subscribe(
      response => {
        if (response.success) {
          this.countedTests = response.data;

          for (let calendar of data.calendar) {
            let data = new Date(calendar.date);

            for (let values of response.data) {
              let c = new Date(values.date);
              let compararea = +data == +c;

              if (compararea) {
                calendar.count = values.count;
              }
            }
          }
        }
      }
    )
  }

  parseDates(date) {
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${year}-${monthWithZero}-${dayWithZero}`;
  }

  parseDatesForTable(date) {
    const day = date && date.getDate() || -1;
    const dayWithZero = day.toString().length > 1 ? day : '0' + day;
    const month = date && date.getMonth() + 1 || -1;
    const monthWithZero = month.toString().length > 1 ? month : '0' + month;
    const year = date && date.getFullYear() || -1;

    return `${dayWithZero}/${monthWithZero}/${year}`;
  }

  createPoll(check: boolean, testTemplateId?, eventId?) {
    this.testService.createMinePoll({testTemplateId: testTemplateId, eventId: eventId}).subscribe((res) => {
      if(check)
        this.router.navigate(['../../polls/start-poll-page', res.data], { relativeTo: this.route });
      else 
       this.router.navigate(['../../polls/performing-poll', res.data], { relativeTo: this.route });
    });
  }
}
