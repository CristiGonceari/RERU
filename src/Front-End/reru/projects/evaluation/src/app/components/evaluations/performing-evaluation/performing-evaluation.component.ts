import { Component, Injector, OnInit } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HashOptionInputComponent } from '../../../utils/components/hash-option-input/hash-option-input.component';
import { AnswerStatusEnum } from '../../../utils/enums/answer-status.enum';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';
import { TestQuestionSummary } from '../../../utils/models/test-questions/test-question-summary.model';
import { Test } from '../../../utils/models/tests/test.model';
import { TestQuestionService } from '../../../utils/services/test-question/test-question.service';
import { TestTemplateService } from '../../../utils/services/test-template/test-template.service';
import { TestService } from '../../../utils/services/test/test.service';
import { TestQuestion } from '../../../utils/models/test-questions/test-question.model';
import { TestAnswer } from '../../../utils/models/test-questions/test-answer.model';
import { TestTemplate } from '../../../utils/models/test-templates/test-template.model';
import { TestTemplateSettings } from '../../../utils/models/test-templates/test-template-settings.model';
import { AddTestQuestion } from '../../../utils/models/test-questions/add-test-question.model';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { EvaluationResultModalComponent } from '../../../utils/modals/evaluation-result-modal/evaluation-result-modal.component';
import { FileTestAnswerService } from '../../../utils/services/FileTestAnswer/file-test-answer.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-performing-evaluation',
  templateUrl: './performing-evaluation.component.html',
  styleUrls: ['./performing-evaluation.component.scss']
})
export class PerformingEvaluationComponent implements OnInit {
  testId: number;
  questionIndex: number = 1;
  testDto = new Test();
  pager: number[] = [];
  viewed: number[] = [];
  skipped: number[] = [];
  answered: number[] = [];
  count: number;
  disableBtn: boolean = false;

  title: string;
  description: string;
  no: string;
  yes: string;

  testOptionsList = [];
  testQuestionSummary: TestQuestionSummary[] = [];
  questionUnit = new TestQuestion();
  testAnswersInput: TestAnswer[] = [];
  testQuestion: AddTestQuestion[] = [];
  testTemplateModel = new TestTemplate();
  testTemplateSettings = new TestTemplateSettings();
  hashedOptions;

  textAnswer: string;
  answerStatus: number;

  questionTypeEnum = QuestionUnitTypeEnum;
  answerStatusEnum = AnswerStatusEnum;
  disableNext = false;
  isLoading = true;
  testTemplateId;

  isLoadingMedia: boolean;
  imageUrl: any;
  audioUrl: any;
  videoUrl: any;
  fileId: string;

  files: any[] = [];
  fileName: string;
  answerFileid: string;
  hadFile: boolean = false;

  optionFileId = [];
  isLoadingOptionMedia: boolean = true;

  constructor(
    private activatedRoute: ActivatedRoute,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private testService: TestService,
    private modalService: NgbModal,
    public translate: I18nService,
    private router: Router,
    private testTemplateService: TestTemplateService,
    private fileTestAnswerService: FileTestAnswerService
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.summary();
    this.getTestById();
    this.subscribeForHashedAnswers();
    this.testQuestionService.setData(false);
  }

  subscribeForHashedAnswers() {
    this.testQuestionService.answerSubject.subscribe(res => {
      if (res && res != undefined) {
        const index = this.hashedOptions.findIndex(x => x.id == res.optionId);
        this.hashedOptions[index].answer = res.answer;
        this.hashedOptions.forEach(element => {
          this.testAnswersInput.push({ optionId: element.id, answerValue: element.answer })
        });
      }
    })
  }

  checkIfHadFile(){
    let params = {
      questionIndex: this.questionIndex,
      testId: this.testId
    };

    this.fileTestAnswerService.getList(params).subscribe(res => {
      if(res.data.fileId !== null){
        this.answerFileid = res.data.fileId;
        this.fileName = res.data.fileName;
        this.hadFile = true;
      } else {
        this.hadFile = false;
      }
    })
  }

  GetFile() {
    this.fileTestAnswerService.getFile(this.answerFileid).subscribe(response => {
      if (response) {
        const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
        // const fileNameParsed = fileName.substring(1, fileName.length - 1);
        const blob = new Blob([response.body], { type: response.body.type });
        const file = new File([blob], fileName, { type: response.body.type });
        saveAs(file);
      }
    }
    )
  }

  deleteFile(){
    this.hadFile = false;
  }

  ceckFileNameLength() {
    return this.fileName.length <= 20 ? this.fileName : this.fileName.slice(0, 20) + "...";
  }

  setFile(event){
    this.files[0] = event.addedFiles[0];
  }

  onRemove(event): void {
		this.files.splice(this.files.indexOf(event), 1);
	}

  ngDoBoostrap() {
    const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

    customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
  }

  getTestById() {
    this.testService.getTest(this.testId).subscribe(
      res => {
        this.testDto = res.data;
        console.log(res.data)
        this.getTestTemplateSettings(res.data.testTemplateId);
      }
    )
  }

  summary() {
    this.testQuestionService.summary(this.testId).subscribe((res) => {
      for (var i = 1; i <= res.data.length; i++) {
        this.pager.push(i);
      };
      this.testQuestionSummary = res.data;
      this.count = this.testQuestionSummary.length;
      this.pageColor(res.data);
      this.getTestQuestions();
    });
  }

  getViewed(page) {
    return !!(this.viewed.includes(page));
  }

  getSkipped(page) {
    return !!(this.skipped.includes(page));
  }

  getAnswered(page) {
    return !!(this.answered.includes(page));
  }

  parseAnswer(item) {
    return new TestAnswer({
      optionId: item.id,
      answerValue: item.answer
    });
  }

  onItemChange(event) {
    this.testOptionsList.forEach(el => {
      if (el.id == event.target.value) {
        el.isSelected = event.target.checked;
      } else {
        if (this.questionUnit.questionType == this.questionTypeEnum.OneAnswer) {
          el.isSelected = false;
        }
      }
    });

  }

  parseTextAnswer() {
    return new TestAnswer({
      optionId: 0,
      answerValue: this.questionUnit.answerText
    });
  }

  parse(status) {
    return {
      data: new AddTestQuestion({
        testId: this.testId,
        questionIndex: this.questionIndex,
        questionUnitId: null,
        status: status,
        answers: this.testAnswersInput
      })
    }
  }

  saveAnswers() {
    this.disableBtn = true;
    this.isLoading = true;
    this.testAnswersInput = [];

    if (this.questionUnit.questionType == this.questionTypeEnum.FreeText)
      this.testAnswersInput.push(this.parseTextAnswer());
    if (this.questionUnit.questionType == this.questionTypeEnum.HashedAnswer)
      this.subscribeForHashedAnswers();
    else {
      var selectedOptions = this.testOptionsList.filter(Item => Item.isSelected == true);

      selectedOptions.forEach(el => {
        this.testAnswersInput.push(this.parseAnswer(el));
      });
    }
    this.postAnswer(+this.answerStatusEnum.Answered);

    if(this.questionUnit.questionType == this.questionTypeEnum.FileAnswer){
      let request = new FormData();

      request = this.parseFiles(request);

     this.fileTestAnswerService.create(request).subscribe();
    }
  }

  parseFiles(request: FormData) {
    const fileType = '5';
    request.append('QuestionIndex', this.questionIndex.toString());
    request.append('TestId', this.testId.toString());
    request.append('FileDto.File', this.files[0]);
    request.append('FileDto.Type', fileType);

    return request;
  }

  getTestTemplateSettings(testTemplateId) {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId: testTemplateId }).subscribe(
      res => {
        this.testTemplateSettings = res.data;
        this.isLoading = false;
        if (res.data == null) this.testTemplateSettings = new TestTemplateSettings();
      }
    );
  }

  pageColor(data) {
    this.viewed = data.filter(st => st.answerStatus === 1).map(id => id.index);
    this.skipped = data.filter(st => st.answerStatus === 2).map(id => id.index);
    this.answered = data.filter(st => st.answerStatus === 3).map(id => id.index);
  }

  postAnswer(status) {
    this.testQuestionService.postTestQuestions(this.parse(status)).subscribe(
      res => {
        this.testAnswersInput = [];

        this.testQuestionService.summary(this.testId).subscribe(
          res => {
            this.testQuestionSummary = res.data;
            this.pageColor(res.data);

            this.disableBtn = false;
            if (this.questionIndex < this.count)
              this.getTestQuestions(this.questionIndex + 1);
            else {
              this.getTestQuestions(1);
            }
          });
      }
    )
  }

  skipQuestion() {
    this.isLoading = true;
    this.postAnswer(+this.answerStatusEnum.Skipped);
  }

  getTestQuestions(questionIndex?: number) {
    this.isLoading = true;
    this.questionIndex = questionIndex == null ? this.questionIndex : questionIndex;


    this.testQuestionService.getTestQuestion({ testId: this.testId, questionIndex: this.questionIndex })
      .subscribe(
        (res) => {
          this.questionUnit = res.data;
          this.testOptionsList = res.data.options;
          this.hashedOptions = res.data.hashedOptions;
          this.fileId = res.data.mediaFileId;
          this.isLoadingMedia = false;
          this.isLoading = false;
          if (this.questionUnit.answerStatus == AnswerStatusEnum.None)
            this.testQuestionService.postTestQuestions(this.parse(AnswerStatusEnum.Viewed)).subscribe(
              (res) => {
                this.testQuestionService.summary(this.testId).subscribe(
                  res => {
                    this.viewed = res.data.filter(st => st.answerStatus === 1).map(id => id.index);
                  });
              });
          this.ngDoBoostrap();
          this.checkIfHadFile();
          this.files = [];
        }
      )
  }

  submitTest() {
    const modalRef = this.modalService.open(EvaluationResultModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.testId = this.testId;
  }
}
