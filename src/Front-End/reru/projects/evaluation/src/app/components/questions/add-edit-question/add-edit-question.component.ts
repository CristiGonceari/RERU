import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { QuestionService } from  '../../../utils/services/question/question.service'
import { SelectItem } from '../../../utils/models/select-item.model';
import { QuestionByCategoryService } from '../../../utils/services/question-by-category/question-by-category.service';
import { QuestionUnitStatusEnum } from '../../../utils/enums/question-unit-status.enum';
import { ReferenceService } from '../../../utils/services/reference/reference.service';
import { CloudFileService } from '../../../utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';



@Component({
  selector: 'app-add-edit-question',
  templateUrl: './add-edit-question.component.html',
  styleUrls: ['./add-edit-question.component.scss']
})
export class AddEditQuestionComponent implements OnInit {
  questionForm: FormGroup;
  questionUnitId: number;

  types: SelectItem[] = [{ label: '', value: '' }];
  categories: SelectItem[] = [{ label: '', value: '' }];
  category: number;
  value = false;
  isLoading: boolean = true;
  isLoadingMedia: boolean;
  items = [];
  placeHolderString = '+ Tag'
  tags: any;
  fileId: string;
  fileType: string = null;
  attachedFile: File;
  imageFiles: File[] = [];
  videoFiles: File[] = [];
  audioFiles: File[] = [];
  imageUrl: any;
  audioUrl: any;
  videoUrl: any;
  filenames: any;
  fileName: string; 
  
  title: string;
  description: string;

  constructor(
    private questionService: QuestionService,
    private activatedRoute: ActivatedRoute,
    private referenceService: ReferenceService,
    private location: Location,
    private questionByCategory: QuestionByCategoryService,
	  public translate: I18nService,
    private formBuilder: FormBuilder,
		private notificationService: NotificationsService,
    private fileService : CloudFileService,
    private sanitizer: DomSanitizer
  ) {  }

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
          if (res.data.mediaFileId) this.getMediaFile(this.fileId);
          res.data.tags[0] != ('undefined' || 'null') ? this.tags = res.data.tags : this.tags = null;
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

  deleteFile(id):void {
    this.fileService.delete(id).subscribe(res => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('media.file-was-deleted'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultConfig());
    })
  }

	initForm(data?: any): void {
		if (data){
			this.questionForm = this.formBuilder.group({
				id: this.questionUnitId,
				questionCategoryId: this.formBuilder.control((data && !isNaN(data.categoryId) ? data.categoryId : null), [Validators.required]),
				question: this.formBuilder.control((data && data.question) || null, [Validators.required]),
        questionPoints: this.formBuilder.control((data && data.questionPoints) || null, [Validators.required]),
				questionType: this.formBuilder.control((data && !isNaN(data.questionType) ? data.questionType : null), [Validators.required]),
				status: QuestionUnitStatusEnum.Draft
			});
		} else {
      if(this.value) this.questionByCategory.selected.subscribe(x => this.category = x);
      
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
    this.referenceService.getQuestionTypeStatuses().subscribe((res) => this.types = res.data);
  }

  getQuestionCategories() {
    this.referenceService.getQuestionCategory().subscribe((res) => this.categories = res.data);
  }

  getMediaFile(fileId) {
    this.isLoadingMedia = true;
    this.fileService.get(fileId).subscribe( res => {
      this.resportProggress(res);
    })
  }

  private resportProggress(httpEvent: HttpEvent<string[] | Blob>): void
  {
    switch(httpEvent.type)
    {
      case HttpEventType.Response:
        if (httpEvent.body instanceof Array) {
          for (const filename of httpEvent.body) {
            this.filenames.unshift(filename);
          }
        } else {
          this.fileName = httpEvent.headers.get('Content-Disposition').split('filename=')[1].split(';')[0];
          const blob = new Blob([httpEvent.body], { type: httpEvent.body.type });
          const file = new File([blob], this.fileName, { type: httpEvent.body.type });
          this.readFile(file).then(fileContents => {
            if (blob.type.includes('image')) this.imageUrl = fileContents;
            else if (blob.type.includes('video')) this.videoUrl = fileContents;
            else if (blob.type.includes('audio')) {
              this.audioUrl = fileContents;
              this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
            }
          });
        this.isLoadingMedia = false;
      }
      break;
    }
  }

  addQuestion() {
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
    });
  }

  onTextChange(text: string) {
    if(text.length) {
      this.questionService.getTags({Keyword: text}).subscribe(res => {
        this.items = res.data.map(el => el.name);
      })
    }
  };

  editQuestion(): void {
    const request = new FormData();

    if (this.attachedFile)
    {
      this.fileType = '4';
      request.append('Data.FileDto.File', this.attachedFile);
      request.append('Data.FileDto.Type', this.fileType);
    }
    request.append('Data.Id', this.questionForm.value.id);
    request.append('Data.Question', this.questionForm.value.question);
    request.append('Data.QuestionCategoryId', this.questionForm.value.questionCategoryId);
    request.append('Data.QuestionPoints', this.questionForm.value.questionPoints);
    request.append('Data.QuestionType', this.questionForm.value.questionType);
    request.append('Data.QuestionStatus', this.questionForm.value.status);
    request.append('Data.Tags', this.tags);
    request.append('Data.MediaFileId', this.fileId);

    this.questionService.edit(request).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('questions.succes-update-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
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

  onSelect(event) {
    event.addedFiles.forEach((element) => {
        const regexImage = new RegExp('(.*?).(jpg|png|jpeg|svg|gif)$');
        const regexVideo = new RegExp('(.*?).(mp4|3gp|ogg|wmv|flv|avi)$');
        const regexAudio = new RegExp('(.*?).(mp3|oga|wav)$');

        if (regexImage.test(element.name)) {
          this.imageFiles.push(...event.addedFiles);
          this.readFile(this.imageFiles[0]).then(fileContents => {
            this.imageUrl = fileContents;
          });
        } else if (regexVideo.test(element.name)) {
          this.videoFiles.push(...event.addedFiles);
          this.readFile(this.videoFiles[0]).then(fileContents => {
            this.videoUrl = fileContents;
          });
        } else if (regexAudio.test(element.name)){
          this.audioFiles.push(...event.addedFiles);
          this.readFile(this.audioFiles[0]).then(fileContents => {
            this.audioUrl = fileContents;
            this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
          });
        } else {
          forkJoin([
            this.translate.get('modal.error'),
            this.translate.get('media.invalid-type'),
          ]).subscribe(([title, description]) => {
            this.title = title;
            this.description = description;
            });
          this.notificationService.error(this.title, this.description,  NotificationUtil.getDefaultConfig());
        }
        this.attachedFile = event.addedFiles[0];
    });
}   

  onRemoved() {
    this.imageFiles = this.videoFiles = this.audioFiles = [];
    this.videoUrl = this.audioUrl = this.imageUrl = this.fileName = null;
    // this.fileService.delete(this.fileId).subscribe( res => {
    //   if(res) this.fileId = null;
    // })
  }

  public async readFile(file: File): Promise<string | ArrayBuffer> {
    return new Promise<string | ArrayBuffer>((resolve, reject) => {
      const reader = new FileReader();
  
      reader.onload = e => {
        return resolve((e.target as FileReader).result);
      };

      reader.onerror = e => {
        console.error(`FileReader failed on file ${file.name}.`);
        return reject(null);
      };

      if (!file) {
        console.error('No file to read.');
        return reject(null);
      }

      reader.readAsDataURL(file);
    });
  }

}
