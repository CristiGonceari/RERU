import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { OptionsService } from 'projects/evaluation/src/app/utils/services/options/options.service';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

@Component({
  selector: 'app-delete-option',
  templateUrl: './delete-option.component.html',
  styleUrls: ['./delete-option.component.scss']
})
export class DeleteOptionComponent implements OnInit {
  optionId: number;
  answer: string;
  isLoading: boolean = true;

  constructor(private location: Location, 
    private optionService: OptionsService, 
    private route: ActivatedRoute,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.optionId = +this.route.snapshot.paramMap.get('id');
    if(this.optionId) this.get();
	}

  get() {
    this.optionService.get(this.optionId).subscribe(res => {
      if (res && res.data) {
        this.answer = res.data.answer;
        this.isLoading = false;
      }
    });
  }

  yes() {
    this.optionService.delete(this.optionId).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Option was successfully deleted', NotificationUtil.getDefaultMidConfig());
    });
  }

  back() {
    this.location.back();
  }
}
