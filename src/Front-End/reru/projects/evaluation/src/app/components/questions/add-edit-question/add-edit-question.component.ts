import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { QuestionService } from '../../../utils/services/question/question.service'
import { SelectItem } from '../../../utils/models/select-item.model';
import { QuestionByCategoryService } from '../../../utils/services/question-by-category/question-by-category.service';
import { QuestionUnitStatusEnum } from '../../../utils/enums/question-unit-status.enum';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { QuestionUnitTypeEnum } from '../../../utils/enums/question-unit-type.enum';

@Component({
  selector: 'app-add-edit-question',
  templateUrl: './add-edit-question.component.html',
  styleUrls: ['./add-edit-question.component.scss']
})
export class AddEditQuestionComponent implements OnInit {
  questionForm: FormGroup;
  questionUnitId: number;
  disableBtn: boolean = false;
  
  hashedAnswerTag = "[answer][/answer]"

  types: SelectItem[] = [{ label: '', value: '' }];
  categories: SelectItem[] = [{ label: '', value: '' }];
  category: number;
  value = false;
  isLoading: boolean = true;
  items = [];
  placeHolderString = '+ Tag'
  tags: any;
  fileId: string;
  fileType: string = null;
  attachedFile: File;
  title: string;
  description: string;

  caretPosition: number = 0;

  constructor(
    private questionService: QuestionService,
    private activatedRoute: ActivatedRoute,
    private referenceService: ReferenceService,
    private location: Location,
    private questionByCategory: QuestionByCategoryService,
    public translate: I18nService,
    private formBuilder: FormBuilder,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.questionForm = new FormGroup({
      questionCategoryId: new FormControl(),
      question: new FormControl(),
      questionType: new FormControl(),
      questionPoints: new FormControl()
    });

    this.questionByCategory.value.subscribe(x => this.value = x);
    this.initData();
    this.getQuestionTypeValueForDropdown();
    this.getQuestionCategories();
  }

  initData(): void {
    this.activatedRoute.params.subscribe(response => {
      if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
        this.questionUnitId = response.id;
        this.questionService.get(this.questionUnitId).subscribe(res => {
          this.fileId = res.data.mediaFileId;
          this.initForm(res.data);
          if (res.data.tags[0] != null && res.data.tags[0] != 'undefined') {
            this.tags = res.data.tags[0].split(',')
          } else this.tags = [];
        })
      }
      else this.initForm();
    })
  }

  hasErrors(field): boolean {
    return this.questionForm.touched && this.questionForm.get(field).invalid;
  }

  hasError(field: string, error = 'required'): boolean {
    return (
      this.questionForm.get(field).touched && this.questionForm.get(field).hasError(error)
    );
  }

  initForm(data?: any): void {
    if (data) {
      this.questionForm = this.formBuilder.group({
        id: this.questionUnitId,
        questionCategoryId: this.formBuilder.control((data && !isNaN(data.categoryId) ? data.categoryId : null), [Validators.required]),
        question: this.formBuilder.control((data && data.question) || null, [Validators.required]),
        questionPoints: this.formBuilder.control((data && data.questionPoints) || null, [Validators.required]),
        questionType: this.formBuilder.control((data && !isNaN(data.questionType) ? data.questionType : null), [Validators.required]),
        status: QuestionUnitStatusEnum.Draft
      });
    } else {
      if (this.value) this.questionByCategory.selected.subscribe(x => this.category = x);

      this.questionForm = this.formBuilder.group({
        questionCategoryId: this.formBuilder.control((this.category), [Validators.required]),
        question: this.formBuilder.control(null, [Validators.required]),
        questionPoints: this.formBuilder.control(1, [Validators.required]),
        questionType: this.formBuilder.control(0, [Validators.required]),
        status: QuestionUnitStatusEnum.Draft
      });
    }

    this.isLoading = false;
  }

  getQuestionTypeValueForDropdown() {
    this.referenceService.getQuestionType().subscribe((res) => this.types = res.data);
  }

  getQuestionCategories() {
    this.referenceService.getQuestionCategory().subscribe((res) => this.categories = res.data);
  }

  addQuestion() {
    this.disableBtn = true;
    const request = new FormData();
    if (this.attachedFile) {
      this.fileType = '4';
      request.append('FileDto.File', this.attachedFile);
      request.append('FileDto.Type', this.fileType);
    }
    request.append('Question', this.questionForm.value.question);
    request.append('QuestionCategoryId', this.questionForm.value.questionCategoryId);
    request.append('QuestionPoints', this.questionForm.value.questionPoints);
    request.append('QuestionType', this.questionForm.value.questionType);
    request.append('QuestionStatus', this.questionForm.value.status);
    request.append('Tags', this.tags);

    this.questionService.create(request).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('questions.succes-add-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => {
      this.disableBtn = false;
    });
  }

  onTextChange(text: string) {
    if (text.length) {
      this.questionService.getTags({ Keyword: text }).subscribe(res => {
        this.items = res.data.map(el => el.name);
      })
    }
  };

  editQuestion(): void {
    this.disableBtn = true;
    const request = new FormData();

    if (this.attachedFile) {
      this.fileType = '4';
      request.append('Data.FileDto.File', this.attachedFile);
      request.append('Data.FileDto.Type', this.fileType);
    }
    request.append('Data.Id', this.questionForm.value.id);
    request.append('Data.Question', this.questionForm.value.question);
    request.append('Data.QuestionCategoryId', this.questionForm.value.questionCategoryId);
    request.append('Data.QuestionPoints', this.questionForm.value.questionPoints);
    request.append('Data.QuestionType', this.questionForm.value.questionType);
    request.append('Data.Status', this.questionForm.value.status);
    request.append('Data.MediaFileId', this.fileId);
    request.append('Data.Tags', this.tags);

    this.questionService.edit(request).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('questions.succes-update-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.disableBtn = false;
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => {
      this.disableBtn = false;
    });
  }

  onSave(): void {
    if (this.questionUnitId) {
      this.editQuestion();
    } else {
      this.addQuestion();
    }
  }

  backClicked() {
    this.location.back();
  }

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }

  getCaretPos(oField) {
    if (oField.selectionStart || oField.selectionStart == '0') {
      this.caretPosition = oField.selectionStart;
    }
  }

  insertQestionTag() {
    let questionValue = this.questionForm.get("question").value;
    let output: string;
    if (questionValue != null) {
      output = questionValue.substring(0, this.caretPosition) + this.hashedAnswerTag + questionValue.substring(this.caretPosition);
    } else {
      questionValue = "";
      output = questionValue.substring(0, this.caretPosition) + this.hashedAnswerTag + questionValue.substring(this.caretPosition);
    }
    this.questionForm.get("question").setValue(output);
  }

  setHashedAnswer(){
    if(this.questionForm.value.questionType == QuestionUnitTypeEnum.HashedAnswer){
      this.insertQestionTag();
    } else {
      this.questionForm.get("question").setValue("");
    }
  }


}
