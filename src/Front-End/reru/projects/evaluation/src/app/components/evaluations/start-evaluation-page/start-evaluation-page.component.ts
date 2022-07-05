import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { TestService } from '../../../utils/services/test/test.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-start-evaluation-page',
  templateUrl: './start-evaluation-page.component.html',
  styleUrls: ['./start-evaluation-page.component.scss']
})
export class StartEvaluationPageComponent implements OnInit {
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
      this.getTestById(this.testId);

    });
  }

  ngOnInit(): void {
  }

  startTestStatus() {
    this.testService.startEvaluation({ testId: this.testId }).subscribe(() => {
        this.router.navigate(['my-activities/performing-evaluation', this.testId]);
    });
  }

  getTestById(testId: number) {
    this.testService.getTest(testId).subscribe(
      res => {
        this.testDto = res.data;
        console.log(res.data)
        this.getTestTemplate();
      }
    )
    this.validateTest(testId);
  }

  validateTest(id): void {
    this.testService.getTestSettings(id).subscribe(
      res => {
        this.testDto = res.data;
      }
    )
  }

  getTestTemplate() {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId: this.testDto.testTemplateId }).subscribe(
      res => {
        this.settings = res.data;
        //if (this.settings.startBeforeProgrammation) this.startTest = true;
      }
    )
  }
}
