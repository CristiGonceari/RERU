import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { UserProfileService } from 'projects/evaluation/src/app/utils/services/user-profile/user-profile.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-detach-persons',
  templateUrl: './detach-persons.component.html',
  styleUrls: ['./detach-persons.component.scss']
})
export class DetachPersonsComponent implements OnInit {
  userId: number;
  planId: number;
  planName;
  firstName;
  lastName;
  isLoading: boolean = true;

  constructor(
    private userService: UserProfileService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private planService: PlanService,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.planId = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.planId)
      this.getPlan();

    this.userId = +this.activatedRoute.snapshot.paramMap.get('id2');
    if (this.userId)
      this.getUser();
  }

  getPlan(): void {
    this.planService.get(this.planId ).subscribe((res) => {
      if (res && res.data) {
        this.planName = res.data.name;
        this.isLoading = false;
      }
    })
  }

  getUser(): void {
    this.userService.getUser({ userProfileId: this.userId }).subscribe((res) => {
      if (res && res.data) {
        this.firstName = res.data.firstName;
        this.lastName = res.data.lastName;
        this.isLoading = false;
      }
    })
  }

  detach() {
    this.planService.detachPerson({data: { planId: this.planId, userProfileId: this.userId }}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Location was successfully detached', NotificationUtil.getDefaultMidConfig());
    });
  }

  backClicked() {
    this.location.back();
  }
}
