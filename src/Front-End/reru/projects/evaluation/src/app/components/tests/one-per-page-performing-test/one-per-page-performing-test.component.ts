import { Component, Injector, OnDestroy, OnInit } from '@angular/core';
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
import { SafeHtmlPipe } from '../../../utils/pipes/safe-html.pipe';
import { ConfirmModalComponent } from '@erp/shared';
import { forkJoin, Observable } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { FileTestAnswerService } from '../../../utils/services/FileTestAnswer/file-test-answer.service';
import { saveAs } from 'file-saver';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { StyleNodesService } from '../../../utils/services/style-nodes/style-nodes.service';

@Component({
  selector: 'app-one-per-page-performing-test',
  templateUrl: './one-per-page-performing-test.component.html',
  styleUrls: ['./one-per-page-performing-test.component.scss'],
  providers: [SafeHtmlPipe]

})
export class OnePerPagePerformingTestComponent implements OnInit, OnDestroy {

  testId: number;
  questionIndex: number = 1;
  testDto = new Test();
  pager: number[] = [];
  viewed: number[] = [];
  skipped: number[] = [];
  answered: number[] = [];
  timeLeft;
  interval;
  count: number;
  timeQuestionLeft;
  timerInterval;
  percent;

  disableBtn: boolean = false;

  title: string;
  description: string;
  inProgress: string;
  no: string;
  yes: string;

  testOptionsList = [];
  testQuestionSummary: TestQuestionSummary[] = [];
  questionUnit = new TestQuestion();
  testAnswersInput: TestAnswer[] = [];
  testQuestion: AddTestQuestion[] = [];
  testTemplateModel = new TestTemplate();
  testTemplateSettings = new TestTemplateSettings();
  hashedOptions = [];

  textAnswer: string;
  answerStatus: number;

  questionTypeEnum = QuestionUnitTypeEnum;
  answerStatusEnum = AnswerStatusEnum;
  disable: number[] = [];
  disableNext = false;
  isLoading = true;
  testTemplateId;

  imageUrl: any;
  audioUrl: any;
  videoUrl: any;
  fileId: string;

  files: any[] = [];
  answerFileid: string;
  hadFile: boolean = false;

  optionFileId = [];
  isLoadingOptionMedia: boolean = true;

  isLoadingMedia: boolean = false;

  filenames: any;
  fileName: string;
  fileStatus = { requestType: '', percent: 1 }

  stylesToApply: string = '.breadcrumb{ display: none !important; }'

  constructor(
    private activatedRoute: ActivatedRoute,
    private injector: Injector,
    private testQuestionService: TestQuestionService,
    private testService: TestService,
    private modalService: NgbModal,
    public translate: I18nService,
    private router: Router,
    private testTemplateService: TestTemplateService,
    private fileTestAnswerService: FileTestAnswerService,
    private styleNodesService: StyleNodesService
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.testId = params.id;
    });
  }

  ngOnInit(): void {
    this.getTestById();
    this.testQuestionService.setData(false);
    this.styleNodesService.addStyle('breadcrumb', this.stylesToApply);
  }

  getTestById() {
    this.testService.getTest(this.testId).subscribe(
      res => {
        this.testDto = res.data;
        this.testDto.idnp = '2005003116257';
        this.startTimer();
        this.summary();
      }
    )
  }

  startTimer() {
    this.interval = setInterval(() => {
      const endTime = new Date(this.testDto.endTime).getTime();
      const date = new Date().getTime();

      if (endTime > date) {
        this.timeLeft = this.milisecondsToHms(Math.abs(endTime - date));
      } else {
        this.timeLeft = this.milisecondsToHms(endTime);
        clearInterval(this.interval);
        this.finalizeTest();
      }
    }, 1000)
  }

  milisecondsToHms(miliseconds) {
    const seconds = Number(miliseconds / 1000);
    const h = Math.floor(seconds % (3600 * 24) / 3600);
    const m = Math.floor(seconds % 3600 / 60);
    const s = Math.floor(seconds % 60);

    return ` ${h < 10 ? '0' + h : h} : ${m < 10 ? '0' + m : m} : ${s < 10 ? '0' + s : s}`;
  }

  summary() {
    this.testQuestionService.summary(this.testId).subscribe((res) => {
      for (let i = 1; i <= res.data.length; i++) {
        this.pager.push(i);
      };
      this.testQuestionSummary = res.data;
      this.count = this.testQuestionSummary.length;
      this.pageColor(res.data);
      
      this.getTestTemplateSettings();
    });
  }

  pageColor(data) {
    this.viewed = data.filter(st => st.answerStatus === 1).map(id => id.index);
    this.skipped = data.filter(st => st.answerStatus === 2).map(id => id.index);
    this.answered = data.filter(st => st.answerStatus === 3).map(id => id.index);
  }

  getTestQuestions(questionIndex?: number) {
    this.isLoading = true;
    this.questionIndex = questionIndex == null ? this.questionIndex : questionIndex;

    if (this.questionUnit.timeLimit)
      clearInterval(this.timerInterval);

    if (this.testQuestionSummary.find(x => x.index === this.questionIndex).isClosed) {
      this.questionIndex = this.testQuestionSummary.find(x => x.isClosed === false).index;
    }

    this.testQuestionService.getTestQuestion({ testId: this.testId, questionIndex: this.questionIndex })
      .subscribe(
        (res) => {
          this.questionUnit = res.data;
          this.testOptionsList = res.data.options;
          this.hashedOptions = res.data.hashedOptions;
          this.fileId = res.data.mediaFileId;
          this.isLoadingMedia = false;
          this.isLoading = false;
          if (this.questionUnit.timeLimit)
            this.startQuestionTimer();
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
        },
        (error) => {
          this.isLoading = false;
          error.error.messages.some(x => {
            if (x.code === '03020604' || x.code === '03001503')
              this.finalizeTest();
          })
        }
      )
  }

  getTestTemplateSettings() {
    this.testTemplateService.getTestTemplateSettings({ testTemplateId: this.testDto.testTemplateId }).subscribe(
      res => {
        this.testTemplateSettings = res.data;
        if (res.data == null) this.testTemplateSettings = new TestTemplateSettings();
        this.getTestQuestions();
      }
    );
  }

  startQuestionTimer() {
    this.timeQuestionLeft = this.questionUnit.timeLimit;
    this.percent = this.timeQuestionLeft * 100 / this.questionUnit.timeLimit;

    this.timerInterval = setInterval(() => {
      if (this.timeQuestionLeft > 0) {
        this.timeQuestionLeft--;
        this.percent = this.timeQuestionLeft * 100 / this.questionUnit.timeLimit;
      } else {
        clearInterval(this.timerInterval);
        if (this.questionIndex < this.count)
          this.getTestQuestions(this.questionIndex + 1);
        else {
          this.getTestQuestions(1);
        }
      }
    }, 1000);
  }

  ngDoBoostrap() {
    this.subscribeForHashedAnswers();
    const el = createCustomElement(HashOptionInputComponent, { injector: this.injector });

    customElements.get('app-hash-option-input') || customElements.define('app-hash-option-input', el);
  }

  subscribeForHashedAnswers() {
    this.testQuestionService.answerSubject.subscribe(res => {
      if (res && res != undefined && this.hashedOptions.length > 0) {
        res.forEach(element => {
          const index = this.hashedOptions.findIndex(x => x.id == element.optionId);
          if (index > -1) {
            this.hashedOptions[index].answer = element.answer;
          }
        });
        this.hashedOptions.forEach(element => {
          this.testAnswersInput.push({ optionId: element.id, answerValue: element.answer })
        });
      }
    })
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
      const selectedOptions = this.testOptionsList.filter(Item => Item.isSelected == true);

      selectedOptions.forEach(el => {
        this.testAnswersInput.push(this.parseAnswer(el));
      });
    }

    if (this.questionUnit.questionType == this.questionTypeEnum.FileAnswer) {
      let request = new FormData();

      request = this.parseFiles(request);

      this.fileTestAnswerService.create(request).subscribe((res) => {
        this.reportProggress(res);
      }, (error) => {
        this.isLoadingMedia = false;
        this.disableBtn = false;
        this.isLoading = false;
      });
    } else {
      this.postAnswer(+this.answerStatusEnum.Answered);
    }
  }

  private reportProggress(httpEvent: HttpEvent<string[] | Blob>): void {
    switch (httpEvent.type) {
      case HttpEventType.Sent:
        this.isLoadingMedia = true;
        this.fileStatus.percent = 1;
        break;
      case HttpEventType.UploadProgress:
        forkJoin([
					this.translate.get('position.in-progress'),
				]).subscribe(([inProgress]) => {
					this.inProgress = inProgress;
				});
        this.updateStatus(httpEvent.loaded, httpEvent.total, this.inProgress)
        break;
      case HttpEventType.DownloadProgress:
        forkJoin([
					this.translate.get('position.in-progress'),
				]).subscribe(([inProgress]) => {
					this.inProgress = inProgress;
				});
        this.updateStatus(httpEvent.loaded, httpEvent.total, this.inProgress)
        break;
      case HttpEventType.Response:
        this.fileStatus.requestType = "Done";
        this.postAnswer(+this.answerStatusEnum.Answered);
        this.isLoadingMedia = false;
        break;
    }
  }

  updateStatus(loaded: number, total: number | undefined, requestType: string) {
    this.fileStatus.requestType = requestType;
    this.fileStatus.percent = Math.round(100 * loaded / total);
  }

  parseFiles(request: FormData) {
    const fileType = '5';
    request.append('QuestionIndex', this.questionIndex.toString());
    request.append('TestId', this.testId.toString());
    request.append('FileDto.File', this.files[0]);
    request.append('FileDto.Type', fileType);

    return request;
  }

  onRemove(event): void {
    this.files.splice(this.files.indexOf(event), 1);
  }

  getFile() {
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

  checkFileNameLength() {
    return this.fileName.length <= 20 ? this.fileName : this.fileName.slice(0, 20) + "...";
  }

  checkLength(fileName) {
    return fileName.length <= 20 ? fileName : fileName.slice(0, 20) + "...";
  }

  deleteFile() {
    this.hadFile = false;
  }

  checkIfHadFile() {
    let params = {
      questionIndex: this.questionIndex,
      testId: this.testId
    };

    this.fileTestAnswerService.getList(params).subscribe(res => {
      if (res.data.fileId !== null) {
        this.answerFileid = res.data.fileId;
        this.fileName = res.data.fileName;
        this.hadFile = true;
      } else {
        this.hadFile = false;
      }
    })
  }

  setFile(event) {
    this.files[0] = event.addedFiles[0];
  }

  postAnswer(status) {
    this.testQuestionService.postTestQuestions(this.parse(status)).subscribe(
      res => {
        this.testAnswersInput = [];

        this.testQuestionService.summary(this.testId).subscribe(
          res => {
            this.testQuestionSummary = res.data;
            this.pageColor(res.data);

            if (this.testQuestionSummary.every(x => x.isClosed === true) || this.questionIndex == this.count) {
              this.submitTest();
            } 
            else if (!this.testTemplateSettings.possibleChangeAnswer || !this.testTemplateSettings.possibleGetToSkipped) {
              this.disableBtn = false;
              const isNotClosedAnswers = this.testQuestionSummary.filter(x => x.isClosed === false);

              this.questionIndex = isNotClosedAnswers.some(x => x.index > this.questionIndex) ?
                isNotClosedAnswers.filter(x => x.index > this.questionIndex)[0]?.index :
                isNotClosedAnswers.filter(x => x.index < this.questionIndex)[0]?.index;
              if (this.questionIndex) {
                this.getTestQuestions(this.questionIndex);
              }
            }  
            else {
              this.disableBtn = false;
              if (this.questionIndex < this.count) {
                this.getTestQuestions(this.questionIndex + 1);
              } else {
                if (this.testQuestionSummary.every(x => x.answerStatus === AnswerStatusEnum.Answered)) {
                  this.submitTest();
                } else {
                  this.getTestQuestions(1);
                }
              }
            }
          });
      }, 
      err => {
        this.isLoading = false;
      }
    )
  }

  skipQuestion() {
    this.isLoading = true;
    this.postAnswer(+this.answerStatusEnum.Skipped);
  }

  submitTest() {
    if (this.testQuestionSummary.every(x => x.isClosed === true)) {
      this.testQuestionService.summary(this.testId).subscribe(() => this.disableNext = true);
    }
    forkJoin([
      this.translate.get('modal.finish-test'),
      this.translate.get('tests.finish-test-msg'),
      this.translate.get('modal.no'),
      this.translate.get('modal.yes'),
    ]).subscribe(([title, description, no, yes]) => {
      this.title = title;
      this.description = description;
      this.no = no;
      this.yes = yes;
    });
    const modalRef = this.modalService.open(ConfirmModalComponent, { centered: true });
    modalRef.componentInstance.title = this.title;
    modalRef.componentInstance.description = this.description;
    modalRef.componentInstance.buttonNo = this.no;
    modalRef.componentInstance.buttonYes = this.yes;
    modalRef.result.then(
      () => {
        this.finishTestProcess();
      }, 
      () => {
        // if (this.testQuestionSummary.every(x => x.isClosed === true)) {
        //   this.finishTestProcess();
        // }
        this.isLoading = false;
      }
    );
  }

  finalizeTest() {
    this.testService.finalizeTest(this.testId).subscribe(() => this.router.navigate(['my-activities/finish-page', this.testId]));
    this.styleNodesService.removeStyle('breadcrumb');
  }

  finishTestProcess() {
    this.isLoading = true;
    this.finalizeTest();
    clearInterval(this.interval);
    clearInterval(this.timerInterval);
  }

  ngOnDestroy() {
    this.styleNodesService.removeStyle('breadcrumb');
  }
}
