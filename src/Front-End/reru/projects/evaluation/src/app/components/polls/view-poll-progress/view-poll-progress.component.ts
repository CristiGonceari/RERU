import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PollOption } from '../../../utils/models/tests/poll-option.model';
import { PollResult } from '../../../utils/models/tests/poll-result.model';
import { TestService } from '../../../utils/services/test/test.service';

@Component({
  selector: 'app-view-poll-progress',
  templateUrl: './view-poll-progress.component.html',
  styleUrls: ['./view-poll-progress.component.scss']
})
export class ViewPollProgressComponent implements OnInit {

  testTemplateId: number;
  pollResult: PollResult = new PollResult();
  options: PollOption[] = [];
  isLoading: boolean = true;
  pageNumber: number = 1;

  constructor(
    private testService: TestService, 
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.testTemplateId = params.id;
      this.getPollResult();
    })
  }

  getPollResult(page?){
    this.pageNumber = page == null ? this.pageNumber : page;
    this.isLoading = true;
    this.testService.pollProgress({testTemplateId: this.testTemplateId, index: this.pageNumber}).subscribe((res) => {
      this.pollResult = res.data;
      this.isLoading = false;
      this.options = this.pollResult.questions[0].options;
    });
  }

}
