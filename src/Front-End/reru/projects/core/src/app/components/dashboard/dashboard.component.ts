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
  isLoading: boolean = true;
  user;

  constructor(
    private moduleService: AvailableModulesService,
    private userSubject: ApplicationUserService,
    private internalService: InternalService,
    public notificationService: NotificationsService,
    public translate: I18nService,
  ) {}

  ngOnInit(): void {
    this.subscribeForAuthChange();
    this.list();
    this.setIntrvl();
    
  }
  
  subscribeForAuthChange(): void {
    this.isLoading = true;
    this.userSubject.userChange.subscribe((res) => {
        this.modules = this.moduleService.get();
        this.isLoading = false;
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
