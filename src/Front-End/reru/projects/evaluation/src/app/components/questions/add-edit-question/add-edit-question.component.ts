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
import { QuestionUnit } from '../../../utils/models/question-units/question-unit.model';
import { CloudFileService } from '../../../utils/services/cloud-file/cloud-file.service';
import { DomSanitizer } from '@angular/platform-browser';



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
  category = 0;
  value = false;
  isLoading: boolean = true;
  items = [];
  placeHolderString = '+ Tag'
  tags = [];
  fileId;
  seIncarca: boolean = true;
  seIncarca1: boolean = true;
  seIncarca3: boolean = true;
  fileType;
  uploadFiles;
  lastId;
  video = false;
  image = false;
  audio = false;

  allFiles: File[] = [];
    imageFiles: File[] = [];
    videoFiles: File[] = [];
    audioFiles: File[] = [];

    imageUrl: any;
    audioUrl: any;
    videoUrl: any;

    isLabelHideen = false;

  constructor(
    private questionService: QuestionService,
    private activatedRoute: ActivatedRoute,
    private referenceService: ReferenceService,
    private location: Location,
    private questionByCategory: QuestionByCategoryService,
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
					this.initForm(res.data);
          this.tags = res.data.tags;
				})
			}
			else
				this.initForm();
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

  onFileChange(event) {
    this.uploadFiles = event.target.files[0];
    if (this.uploadFiles.type.includes('video')) this.video = true;
    else if (this.uploadFiles.type.includes('audio')) this.audio = true;
    else if (this.uploadFiles.type.includes('image')) this.image = true;
    else this.video = false; this.audio = false; this.image = false;
    console.log('onFileChange', this.uploadFiles, this.video);
  }

  deleteFile(id):void {
    this.fileService.delete(id).subscribe(res => {
      this.notificationService.success('Success', 'Was deleted', NotificationUtil.getDefaultConfig());
      // this.getDemoList();
      console.log('deleete', id);
    })
    
  }
  
  uploadFile(): void
  {
    this.seIncarca1 = false;
    const request = new FormData();
    request.append('File', this.uploadFiles);
    request.append('Type', '4');
    this.fileService.create(request).subscribe(res => {
      this.lastId = res.data;
      this.notificationService.success('Success', 'Fișier adăugat!', NotificationUtil.getDefaultConfig());
      this.seIncarca1 = true;
      // this.getDemoList();
      console.log('upload', this.lastId);
    }, error =>
    {
      this.notificationService.error('Error', 'Invalid file type',  NotificationUtil.getDefaultConfig());
      this.seIncarca1 = true;
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
		}
		else {
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

  addQuestion() {
    let params = {
        ...this.questionForm.value,
        tags: this.tags
    };
    this.questionService.create({data: params}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Question was successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  onTextChange(text: string) {
    if(text.length) {
      this.questionService.getTags({Keyword: text}).subscribe(res => {
        this.items = res.data.map(el => el.name);
      })
    }
  };

  editQuestion() {
    let params = {
      ...this.questionForm.value,
      tags: this.tags
    }
    this.questionService.edit({data: params}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Question was successfully updated', NotificationUtil.getDefaultMidConfig());
    });
  }

  onSave(): void {
    if (this.questionUnitId) {
      this.editQuestion();
    } else {
      this.addQuestion();
      this.uploadFile();
    }
  }

  backClicked() {
    this.location.back();
  }

  onSelect(event) {
    event.addedFiles.forEach((element) => {
        // for check image type
        const regexImage = new RegExp('(.*?).(jpg|png|jpeg|svg|gif)$');
        // for check video type
        const regexVideo = new RegExp('(.*?).(mp4|3gp|ogg|wmv|flv|avi)$');
        // for check video type
        const regexAudio = new RegExp('(.*?).(mp3|oga|wav)$');

        // clear list
        // this.onRemoved();

        if (regexImage.test(element.name)) {
            this.imageFiles.push(...event.addedFiles);
            this.readFile(this.imageFiles[0]).then(fileContents => {
                // Put this string in a request body to upload it to an API.
                this.imageUrl = fileContents;
            });
            console.warn('this.imageUrl', this.imageFiles[0]);
            
            this.isLabelHideen = true;
        } else if (regexVideo.test(element.name)) {
            this.videoFiles.push(...event.addedFiles);
            this.readFile(this.videoFiles[0]).then(fileContents => {
                // Put this string in a request body to upload it to an API.
                this.videoUrl = fileContents;
            });
            this.isLabelHideen = true;
        } else if(regexAudio.test(element.name)){
            this.audioFiles.push(...event.addedFiles);
            this.readFile(this.audioFiles[0]).then(fileContents => {
                // Put this string in a request body to upload it to an API.
                this.audioUrl = fileContents;
                this.audioUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.audioUrl);
            });
            this.isLabelHideen = true;
        } else {
          this.notificationService.error('Error', 'Invalid file type',  NotificationUtil.getDefaultConfig());
        }

    });
}   

onRemoved() {
  this.imageFiles = this.videoFiles = this.audioFiles = [];
  this.videoUrl = null;
  this.audioUrl = null;
  this.imageUrl= null;
  this.isLabelHideen = false;
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
