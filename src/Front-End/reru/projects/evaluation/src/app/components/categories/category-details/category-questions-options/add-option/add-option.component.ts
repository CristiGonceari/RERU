import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { OptionsService } from 'projects/evaluation/src/app/utils/services/options/options.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-add-option',
  templateUrl: './add-option.component.html',
  styleUrls: ['./add-option.component.scss']
})
export class AddOptionComponent implements OnInit {
  answer: string;
  isCorrect: any;
  questionId: any;
  optionId: any;
  attachedFile: File;
  disableBtn: boolean = false;

  fileId: string;
  fileType: string = null;
  isLoading: boolean = true;

  title: string;
  description: string;

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
    private location: Location,
	  public translate: I18nService,
    private notificationService: NotificationsService,
    ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.questionId = this.route.snapshot.paramMap.get('id');
    this.optionId = +this.route.snapshot.paramMap.get('id2');
    if(this.optionId) this.get();
    this.isLoading = false;
	}
  
  get() {
    this.optionService.get(this.optionId).subscribe(res => {
      if (res && res.data) {
        this.answer = res.data.answer;
        this.isCorrect = res.data.isCorrect;
        this.fileId = res.data.mediaFileId;
      }
    });
  }

  checkFile(event) {
    if (event != null) this.attachedFile = event;
    else this.fileId = null;
  }

  add() {
    this.disableBtn = true;
    const request = new FormData();

    if (this.attachedFile) {
      this.fileType = '4';
      request.append('FileDto.File', this.attachedFile);
      request.append('FileDto.Type', this.fileType);
    }
      request.append('Answer', this.answer);
      request.append('IsCorrect', this.isCorrect);
      request.append('QuestionUnitId', this.questionId);

    this.optionService.create(request).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('options.succes-add-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.back();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  edit() {
    this.disableBtn = true;
    const request = new FormData();

    if (this.attachedFile) {
      this.fileType = '4';
      request.append('Data.FileDto.File', this.attachedFile);
      request.append('Data.FileDto.Type', this.fileType);
    }
      request.append('Data.Id', this.optionId);
      request.append('Data.Answer', this.answer);
      request.append('Data.IsCorrect', this.isCorrect);
      request.append('Data.QuestionUnitId', this.questionId);
      request.append('Data.MediaFileId', this.fileId);
    this.optionService.edit(request).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('options.succes-update-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.back();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => this.disableBtn = false);
  }

  confirm() {
    if(this.optionId)
      this.edit();
    else this.add();
  }

  back() {
    this.location.back();
  }
}
