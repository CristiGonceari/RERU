import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { TestService } from '../../../utils/services/test/test.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-start-test-page',
  templateUrl: './start-test-page.component.html',
  styleUrls: ['./start-test-page.component.scss']
})
export class StartTestPageComponent implements OnInit {

  testId: number;
  testDto = new Test();
  settings;

  timeLeft;
  interval;
  accept: boolean = false;
  startTest: boolean = false;

  editorData: string = '';
  public Editor = DecoupledEditor;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private testQuestionService: TestQuestionService,
    private testService: TestService,
    private testTypeService: TestTypeService,
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
      this.getTestById(params.id);
    });
  }

  ngOnInit(): void {
    this.startTimer();
  }

  startTimer() {
    this.interval = setInterval(() => {
      var programmedTime = new Date(this.testDto.programmedTime).getTime();
      var date = new Date().getTime();

      if (programmedTime > date) {
        this.timeLeft = this.milisecondsToHms(Math.abs(programmedTime - date));
      } else {
        this.timeLeft = "00 : 00 : 00";
        if (!this.settings.startBeforeProgrammation && this.timeLeft == "00 : 00 : 00") {
          this.startTest = true;
        }
      }
    }, 1000)
  }

  milisecondsToHms(miliseconds) {
    var seconds = Number(miliseconds / 1000);
    var h = Math.floor(seconds % (3600 * 24) / 3600);
    var m = Math.floor(seconds % 3600 / 60);
    var s = Math.floor(seconds % 60);

    return ` ${h < 10 ? '0' + h : h} : ${m < 10 ? '0' + m : m} : ${s < 10 ? '0' + s : s}`;
  }

  goToTest(): void {
    if(this.settings.showManyQuestionPerPage)
      this.testQuestionService.generate(this.testId).subscribe(() => this.router.navigate(['run-test-questions', this.testId]));
    else 
      this.testQuestionService.generate(this.testId).subscribe(() => this.router.navigate(['run-test', this.testId]));
  }

  getTestById(testId: number) {
    this.testService.getTest({ id: testId }).subscribe(
      res => {
        this.testDto = res.data;
        this.getTestType();
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

  getTestType() {
    this.testTypeService.getTestTypeSettings({ testTypeId: this.testDto.testTypeId }).subscribe(
      res => {
        this.settings = res.data;
        if (this.settings.startBeforeProgrammation) this.startTest = true;
      }
    )
  }
}
