import { Component, OnDestroy, OnInit } from '@angular/core';
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
export class StartTestPageComponent implements OnInit, OnDestroy  {

  testId: number;
  testDto = new Test();
  settings;

  isLoading: boolean = false;

  timeLeft;
  interval;
  accept: boolean = false;
  startTest: boolean = false;
  validatePosition: boolean = false;

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
    this.isLoading = true;
    this.getTestById(this.testId);
  }

  startTimer() {
    this.interval = setInterval(() => {
      const programmedTime = new Date(this.testDto.programmedTime).getTime();
      const date = new Date().getTime();

      if (programmedTime > date) {
        this.timeLeft = this.milisecondsToHms(Math.abs(programmedTime - date));
      } else {
        this.timeLeft = "00 : 00 : 00";
        if (typeof this.settings !== undefined) {
          if (!this.settings.startBeforeProgrammation && this.timeLeft == "00 : 00 : 00"){
            this.startTest = true;
            this.isLoading = false;
          }
        }
        this.isLoading = false;
      }
    }, 1000)
  }

  parseCandidatePositions(candidatePositionsNames: string[]) {
    if (this.validatePosition) {
      let string = candidatePositionsNames.join();
      return string.split(',').join(', ');
    }
  }

  milisecondsToHms(miliseconds) {
    const seconds = Number(miliseconds / 1000);
    const h = Math.floor(seconds % (3600 * 24) / 3600);
    const m = Math.floor(seconds % 3600 / 60);
    const s = Math.floor(seconds % 60);

    return ` ${h < 10 ? '0' + h : h} : ${m < 10 ? '0' + m : m} : ${s < 10 ? '0' + s : s}`;
  }

  startTestStatus() {
    this.testService.startTest({ testId: this.testId }).subscribe(() => {
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
        this.getTestTemplate(this.testDto.testTemplateId, testId);
      }
    )
  }

  validateTest(id): void {
    this.testService.getTestSettings(id).subscribe(
      res => {
        this.testDto = res.data;
        this.validatePosition = true;
        this.startTimer();
      }
    )
  }

  b64DecodeUnicode(str) {
    return decodeURIComponent(atob(str).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
  }

  getTestTemplate(testTemplateId: number, testId: number) {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId }).subscribe(
      res => {
        this.settings = res.data;
        if (this.settings.startBeforeProgrammation) this.startTest = true;
        this.validateTest(testId);
      }
    )
  }

  ngOnDestroy(){
    clearInterval(this.interval);
  }
}
