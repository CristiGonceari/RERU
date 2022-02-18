import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestTypeSettings } from '../../../utils/models/test-types/test-type-settings.model';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTypeService } from '../../../utils/services/test-type/test-type.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestVerificationProcessService } from '../../../utils/services/test-verification-process/test-verification-process.service';


@Component({
  selector: 'app-finish-page',
  templateUrl: './finish-page.component.html',
  styleUrls: ['./finish-page.component.scss']
})
export class FinishPageComponent implements OnInit {

  testDto = new Test();
  testTypeSettings = new TestTypeSettings();
  isLoading: boolean = true;
  testId;
  correct;
  totalQuestions;
  points;
  result;
  viewResult = false;

  constructor(private route: ActivatedRoute,
    private testService: TestService,
    private testTypeService: TestTypeService,
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
        this.getTestTypeSettings(res.data.testTemplateId);
      }
    )
  }

  getTestTypeSettings(testTypeId) {
    this.testTypeService.getTestTypeSettings({ testTemplateId: testTypeId }).subscribe(
      res => {
        this.testTypeSettings = res.data;
        this.isLoading = false;
        this.testQuestionService.summary(this.testId).subscribe(
          res => {
            if (res.data.some(x => x.questionType == QuestionUnitTypeEnum.FreeText || x.questionType == QuestionUnitTypeEnum.HashedAnswer))
              this.viewResult = false
            else if (this.testTypeSettings.canViewResultWithoutVerification)
              this.viewResult = true;
          });
      }
    );
  }
}
