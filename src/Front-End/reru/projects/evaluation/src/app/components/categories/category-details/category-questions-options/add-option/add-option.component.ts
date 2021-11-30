import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { OptionModel } from 'projects/evaluation/src/app/utils/models/options/option.model';
import { OptionsService } from 'projects/evaluation/src/app/utils/services/options/options.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-option',
  templateUrl: './add-option.component.html',
  styleUrls: ['./add-option.component.scss']
})
export class AddOptionComponent implements OnInit {

  answer: string;
  isCorrect: boolean;
  questionId: number;
  optionId: number;
  isLoading: boolean = true;

  constructor(private optionService: OptionsService, 
    private route: ActivatedRoute, 
    private location: Location,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.questionId = +this.route.snapshot.paramMap.get('id');
    this.optionId = +this.route.snapshot.paramMap.get('id2');
    if(this.optionId) this.get();
    this.isLoading = false;
	}

  get(){
    this.optionService.get(this.optionId).subscribe(res => {
      if (res && res.data) {
        this.answer = res.data.answer;
        this.isCorrect = res.data.isCorrect;
      }
    });
  }

  parse() {
    this.optionId = null ? null : this.optionId;
    this.isCorrect == true ? this.isCorrect : this.isCorrect = false;
    return {
      data: new OptionModel({
        id: this.optionId,
        answer: this.answer,
        isCorrect: this.isCorrect,
        questionUnitId: this.questionId
      })
    }
  }

  add(){
    this.optionService.create(this.parse()).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Option was successfully added', NotificationUtil.getDefaultMidConfig());
    });
  }

  edit(){
    this.optionService.edit(this.parse()).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Option was successfully updated', NotificationUtil.getDefaultMidConfig());
    });
  }

  confirm(){
    if(this.optionId)
      this.edit();
    else this.add();
  }

  back(){
    this.location.back();
  }
}
