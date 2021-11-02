import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbDateParserFormatter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { CKEditorModule } from 'ngx-ckeditor';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { ViewDatePipe } from './pipes/view-date.pipe';
import { NgbDateFRParserFormatter } from './services/date-parse-formatter.service';
import { Exception404Component } from './exceptions/404/404.component';
import { Exception500Component } from './exceptions/500/500.component';
import { DateComponent } from './components/date/date.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { ViewContactDropdownComponent } from './components/view-contact-dropdown/view-contact-dropdown.component';
import { ViewContactComponent } from './components/view-contact/view-contact.component';
import { PersonalDropdownDetailsComponent } from './components/personal-dropdown-details/personal-dropdown-details.component';
import { LastOrganizationRoleLabelComponent } from './components/last-organization-role-label/last-organization-role-label.component';
import { LastDepartmentLabelComponent } from './components/last-department-label/last-department-label.component';
import { ContentComponent } from './components/content/content.component';
import { FieldGeneratorComponent } from './components/field-generator/field-generator.component';
import { RecordTextGeneratorComponent } from './components/record-text-generator/record-text-generator.component';
import { BadgeStateComponent } from './components/badge-state/badge-state.component';
import { AvatarDetailsComponent } from './components/avatar-details/avatar-details.component';
import { VacationStateComponent } from './components/vacation-state/vacation-state.component';
import { SignDirective } from './directives/sign.directive';
import { CkEditorConfigComponent } from './components/ck-editor-config/ck-editor-config.component'
import { MaterialModule } from '../material.module';



const commonComponents = [
  Exception404Component,
  Exception500Component,
  DateComponent,
  PaginationComponent,
  LoadingSpinnerComponent,
  SafeHtmlPipe,
  PersonalDropdownDetailsComponent,
  ViewContactComponent,
  ViewContactDropdownComponent,
  LastOrganizationRoleLabelComponent,
  LastDepartmentLabelComponent,
  ContentComponent,
  FieldGeneratorComponent,
  RecordTextGeneratorComponent,
  ViewDatePipe,
  BadgeStateComponent,
  AvatarDetailsComponent,
  VacationStateComponent,
  SignDirective,
  CkEditorConfigComponent
];

@NgModule({
  declarations: commonComponents,
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    CKEditorModule,
    MaterialModule
  ],
  exports: [
    TranslateModule,
    commonComponents
  ],
  providers: [{ provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter }, SafeHtmlPipe, ViewDatePipe],
  entryComponents: [
    CkEditorConfigComponent
  ],
})
export class UtilsModule { }
