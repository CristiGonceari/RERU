import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TestStatusEnum } from '../../../../utils/enums/test-status.enum';
import { MyPollStatusEnum } from '../../../../utils/enums/my-poll-status.enum';
import { TestService } from 'projects/evaluation/src/app/utils/services/test/test.service';
 
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
  date;

  constructor(private testService: TestService, private router: Router) { }

  ngOnInit(): void {
    
    this.date = new Date().toISOString();
    if(this.id) this.getPolls();
  }

  getPolls(){
    const params: any = { eventId: this.id }

    this.testService.getUserPollsByEvent(params).subscribe(
      (res) => {
          this.polls = res.data;
          this.isLoading = false;
      }
    )
  }

  createPoll(check: boolean, testTypeId?) {
    this.testService.createMinePoll({testTypeId: testTypeId, eventId: this.id}).subscribe((res) => {
      if(check)
        this.router.navigate(['poll', res.data]);
      else 
       this.router.navigate(['run-poll', res.data]);
    });
  }
}
