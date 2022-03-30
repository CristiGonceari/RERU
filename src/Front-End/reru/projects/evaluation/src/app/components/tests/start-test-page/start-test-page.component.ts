import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { TestService } from '../../../utils/services/test/test.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { TestStatusEnum } from '../../../utils/enums/test-status.enum';

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
    private testTemplateService: TestTemplateService,
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.startTimer();
    this.getTestById(this.testId);

  }

  startTimer() {
    this.interval = setInterval(() => {
      var programmedTime = new Date(this.testDto.programmedTime).getTime();
      var date = new Date().getTime();

      if (programmedTime > date) {
        this.timeLeft = this.milisecondsToHms(Math.abs(programmedTime - date));
      } else {
        this.timeLeft = "00 : 00 : 00";
        if (!this.settings.startBeforeProgrammation && this.timeLeft == "00 : 00 : 00")
          this.startTest = true;
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

  editTestStatus(){
    this.testService.changeStatus({testId: this.testId, status: TestStatusEnum.InProgress}).subscribe(() => {
      if (!this.settings.showManyQuestionPerPage)
        this.router.navigate(['my-activities/one-test-per-page', this.testId]);
      else
       this.router.navigate(['my-activities/multiple-per-page', this.testId]);
    });
  }

  getTestById(testId: number) {
    this.testService.getTest(testId).subscribe(
      res => {
        this.testDto = res.data;
        this.getTestTemplate();
      }
    )
    this.validateTest(testId);
  }

  validateTest(id): void {
    this.testService.getTestSettings(id).subscribe(
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

  getTestTemplate() {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId: this.testDto.testTemplateId }).subscribe(
      res => {
        this.settings = res.data;
        if (this.settings.startBeforeProgrammation) this.startTest = true;
      }
    )
  }
}
