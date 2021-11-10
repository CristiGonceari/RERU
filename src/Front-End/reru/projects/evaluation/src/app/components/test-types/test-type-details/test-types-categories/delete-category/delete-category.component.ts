import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { TestTypeQuestionCategoryService } from 'projects/evaluation/src/app/utils/services/test-type-question-category/test-type-question-category.service';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';

@Component({
  selector: 'app-delete-category',
  templateUrl: './delete-category.component.html',
  styleUrls: ['./delete-category.component.scss']
})
export class DeleteCategoryComponent implements OnInit {
  id;
  answer;
  constructor(private location: Location, 
    private service: TestTypeQuestionCategoryService, 
    private route: ActivatedRoute,
    private notificationService: NotificationsService
    ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.id = this.route.snapshot.paramMap.get('id');
	}

  get(){
    this.service.getQuestionCategoryByTestTypeId(this.id).subscribe(res => {
        this.answer = res.data.answer;
      }
    )
  }

  yes(){
    this.service.deleteTestTypeQuestionCategory({id: this.id}).subscribe(() => {
      this.back();
			this.notificationService.success('Success', 'Category was successfully deleted', NotificationUtil.getDefaultMidConfig());
    });
  }

  back(){
    this.location.back();
  }
}
