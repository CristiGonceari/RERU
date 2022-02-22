import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestTemplateSettings } from '../../../utils/models/test-templates/test-template-settings.model';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestVerificationProcessService } from '../../../utils/services/test-verification-process/test-verification-process.service';


@Component({
  selector: 'app-finish-page',
  templateUrl: './finish-page.component.html',
  styleUrls: ['./finish-page.component.scss']
})
export class FinishPageComponent implements OnInit {

  testDto = new Test();
  testTemplateeSettings = new TestTemplateSettings();
  isLoading: boolean = true;
  testId;
  correct;
  totalQuestions;
  points;
  result;
  viewResult = false;

  constructor(private route: ActivatedRoute,
    private testService: TestService,
    private testTemplateService: TestTemplateService,
    private verifyService: TestVerificationProcessService,
    private testQuestionService: TestQuestionService) { }

  ngOnInit(): void {
    this.initData();
  }

  initData() {
    this.route.params.subscribe(res => {
      this.testId = res.id;
      if (this.testId) {
        this.getSummary();
      }
    })
  }

  getSummary(): void {
    this.verifyService.getSummary(this.testId).subscribe(
      (res) => {
        this.correct = res.data.correctAnswers;
        this.totalQuestions = res.data.totalQuestions;
        this.points = res.data.points;
        this.result = res.data.result;
        this.getTestById();
      }
    )
  }

  getTestById() {
    this.testService.getTest(this.testId).subscribe(
      res => {
        this.testDto = res.data;
        this.getTestTemplateSettings(res.data.testTemplateId);
      }
    )
  }

  getTestTemplateSettings(testTemplateId) {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId: testTemplateId }).subscribe(
      res => {
        this.testTemplateeSettings = res.data;
        this.isLoading = false;
        this.testQuestionService.summary(this.testId).subscribe(
          res => {
            if (res.data.some(x => x.questionType == QuestionUnitTypeEnum.FreeText || x.questionType == QuestionUnitTypeEnum.HashedAnswer))
              this.viewResult = false
            else if (this.testTemplateeSettings.canViewResultWithoutVerification)
              this.viewResult = true;
          });
      }
    );
  }
}
