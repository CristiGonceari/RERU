import { Component, OnInit } from '@angular/core';
import { Test } from '../../../utils/models/tests/test.model';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestService } from '../../../utils/services/test/test.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-start-poll-page',
  templateUrl: './start-poll-page.component.html',
  styleUrls: ['./start-poll-page.component.scss']
})
export class StartPollPageComponent implements OnInit {

  accept: boolean = false;
  pollId;
  testDto: Test = new Test();
  editorData: string = '';
  public Editor = DecoupledEditor;

  constructor(
    private testQuestionService: TestQuestionService, 
    private testService: TestService,
    private activatedRoute: ActivatedRoute, 
    private router: Router
  ) { }

  ngOnInit(): void {
    this.pollId = this.activatedRoute.snapshot.paramMap.get('id');
    this.getTestById(this.pollId);
  }

  getTestById(testId: number) {
    this.testService.getTest(testId).subscribe(
      res => {
        this.testDto = res.data;
        if (this.testDto.rules == null) {
          this.testDto.rules == '';
        } else {
          this.testDto.rules = this.b64DecodeUnicode(res.data.rules);
        }
      }
    )
  }

  b64DecodeUnicode(str) {
    return decodeURIComponent(atob(str).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
  }

  generatePoll(){
    this.testQuestionService.generate(this.pollId).subscribe(() => {this.router.navigate(['../../performing-poll', this.pollId], { relativeTo: this.activatedRoute })});
  }
}
