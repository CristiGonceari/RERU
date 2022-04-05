import { Component, OnInit } from '@angular/core';
import { SidebarView } from '../../utils/models/sidebar.model';
import {
  ApplicationUserService,
  AvailableModulesService,
  ApplicationUserModuleModel,
} from '@erp/shared';

import { InternalService } from '../../utils/services/internal.service';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../../utils/services/i18n.service';
import { forkJoin } from 'rxjs';
import { Router } from '@angular/router';
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

  testId: number;
  showMultipleQuestionsPerPega: boolean;

  constructor(
    private moduleService: AvailableModulesService,
    private userSubject: ApplicationUserService,
    private internalService: InternalService,
    public notificationService: NotificationsService,
    public translate: I18nService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.list();
    this.subscribeForAuthChange();
    this.setIntrvl();
  }

  subscribeForAuthChange(): void {
    this.userSubject.userChange.subscribe(
      () => (this.modules = this.moduleService.get())
    );
    console.log(this.modules);
  }

  list(): void {
    this.modules = this.moduleService.get();
  }

  setIntrvl() {
    setInterval(() => this.getTestId(), 100000);
  }

  getTestId() {
    this.internalService.getTestIdForFastStart().subscribe((res) => {
      if (res && +res.data.testId != 0) {
        this.testId = +res.data.testId;
        this.showMultipleQuestionsPerPega = res.data.showManyQuestionPerPage;
        this.type = res.type;
        this.messageText = res.message;
        forkJoin([
          this.translate.get('Go to Test'),
          this.translate.get('Testul e pe cale de a incepe'),
        ]).subscribe(([type, message]) => {
          this.type = type;
          this.messageText = message;
          this.notificationService
            .info(this.type, this.messageText, {
              timeOut: 29000,
              showProgressBar: true,
            })
            .click.subscribe(() =>
              this.router.navigate(['../reru-evaluation/#/my-activities/start-test/', this.testId])
            );
          // .click.subscribe(() => this.router.navigate(['reru-evaluation/#/my-activities/start-test/', this.testId]));
          // .click.subscribe(() => this.router.navigateByUrl(`http://reru.codwer.com/reru-evaluation/#/my-activities/start-test/${this.testId}`));
        });
      }
    });
  }
}
