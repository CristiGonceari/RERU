import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { Exception404Component } from './exceptions/404/404.component';
import { Exception500Component } from './exceptions/500/500.component';
import { NgbDateParserFormatter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateComponent } from './components/date/date.component';
import { SearchPipe } from './pipes/search.pipe';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpLoaderFactory } from './../utils/services/i18n.service';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { WatchInfoVideoModalComponent } from './modals/watch-info-video-modal/watch-info-video-modal.component';
import { ImportUsersModalComponent } from './modals/import-users-modal/import-users-modal.component';
import { ImportDepartmentModalComponent } from './modals/import-department-modal/import-department-modal.component';
import { ImportRoleModalComponent } from './modals/import-role-modal/import-role-modal.component';
import { AddUserProcessHistoryModalComponent } from './modals/add-user-process-history-modal/add-user-process-history-modal.component';
import { VerifyEmailCodeModalComponent } from './modals/verify-email-code-modal/verify-email-code-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { DateFilterPipe } from './pipes/date-filter.pipe';
import { NgbDateFRParserFormatter } from './services/date-parse-formatter.service';

const commonComponents = [
  Exception404Component,
  Exception500Component,
  DateComponent,
  SearchPipe,
  SafeHtmlPipe,
  WatchInfoVideoModalComponent,
  ImportUsersModalComponent,
  ImportDepartmentModalComponent,
  ImportRoleModalComponent,
  AddUserProcessHistoryModalComponent,
  VerifyEmailCodeModalComponent,
  DateFilterPipe
];

@NgModule({
  declarations: commonComponents,
  imports: [
    CommonModule, 
    NgbModule, 
    TranslateModule, 
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [...commonComponents],
  providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter }, SearchPipe, SafeHtmlPipe, DatePipe],
})
export class UtilsModule { }
