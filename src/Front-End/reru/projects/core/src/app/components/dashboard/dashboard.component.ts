import { Component, OnInit, ViewChild } from '@angular/core';
import { SidebarView } from '../../utils/models/sidebar.model';
import {
  ApplicationUserService,
  AvailableModulesService,
  ApplicationUserModuleModel,
} from '@erp/shared';

import { InternalService } from '../../utils/services/internal.service';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../utils/services/i18n.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccessModeEnum } from '../../utils/models/access-mode.enum';
import { ProfileService } from '../../utils/services/profile.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  sidebarView = SidebarView;
  type: string;
  messageText: string;
  modules: ApplicationUserModuleModel[];

  testId: string;
  showMultipleQuestionsPerPega: boolean;
  user;

  constructor(
    private moduleService: AvailableModulesService,
    private userSubject: ApplicationUserService,
    private internalService: InternalService,
    public notificationService: NotificationsService,
    public translate: I18nService,
    private router: Router,
    private route: ActivatedRoute,
    private profileService: ProfileService
  ) {}

  ngOnInit(): void {
    this.list();
    this.subscribeForAuthChange();
    this.setIntrvl();
    
  }
  
  subscribeForAuthChange(): void {
    this.userSubject.userChange.subscribe((res) => {
        this.modules = this.moduleService.get();
        if (res.isCandidateStatus)
        {
          this.profileService.GetCandidateRegistrationSteps().subscribe(res => {
            if (res.data.unfinishedSteps.length != 0){
              this.modules == null
              this.router.navigate(["./registration-flux",res.data.userProfileId,"step",res.data.unfinishedSteps[0]], { relativeTo: this.route });
            }
          })
        }
    }
    );
  }
 
  list(): void {
    this.modules = this.moduleService.get();
  }

  setIntrvl() {
    setInterval(() => this.getTestId(), 360_000);
  }

  getTestId() {
    this.internalService.getTestIdForFastStart().subscribe(() => {});
  }
}
