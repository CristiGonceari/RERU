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
import { ImportUsersModalComponent } from './modals/import-users-modal/import-users-modal.component';
import { ImportDepartmentModalComponent } from './modals/import-department-modal/import-department-modal.component';
import { ImportRoleModalComponent } from './modals/import-role-modal/import-role-modal.component';
import { AddUserProcessHistoryModalComponent } from './modals/add-user-process-history-modal/add-user-process-history-modal.component';
import { VerifyEmailCodeModalComponent } from './modals/verify-email-code-modal/verify-email-code-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DateFilterPipe } from './pipes/date-filter.pipe';
import { NgbDateFRParserFormatter } from './services/date-parse-formatter.service';
import { BulletinAddressModalComponent } from './modals/bulletin-address-modal/bulletin-address-modal.component';
import { SearchDepartmentComponent } from './components/search-department/search-department.component';
import { SearchRoleComponent } from './components/search-role/search-role.component';

const commonComponents = [
  Exception404Component,
  Exception500Component,
  DateComponent,
  SearchPipe,
  SafeHtmlPipe,
  ImportUsersModalComponent,
  ImportDepartmentModalComponent,
  ImportRoleModalComponent,
  AddUserProcessHistoryModalComponent,
  VerifyEmailCodeModalComponent,
  DateFilterPipe,
  BulletinAddressModalComponent,
  SearchDepartmentComponent,
  SearchRoleComponent
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
  providers: [
    { provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter }, 
    SearchPipe, 
    SafeHtmlPipe, 
    DatePipe
  ],
})
export class UtilsModule { }
