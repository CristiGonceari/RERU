import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { RouterModule } from '@angular/router';
import { ViewDatePipe } from './pipes/view-date.pipe';
import { CKEditorModule } from 'ngx-ckeditor';
import { MaterialModule } from '../material.module';
import { SharedModule } from '@erp/shared';

import { EvaluationDropdownDetailsComponent } from './components/evaluation-dropdown-details/evaluation-dropdown-details.component';
import { ConfirmDeleteSurveyModalComponent } from './modals/confirm-delete-survey-modal/confirm-delete-survey-modal.component';
import { EvaluationComponent } from './components/evaluation/evaluation.component';
import { PublicEvaluationComponent } from './components/public-evaluation/public-evaluation.component';
import { LoadingWrapperComponent } from './components/loading-wrapper/loading-wrapper.component';
import { AttachUserModalComponent } from './modals/attach-user-modal/attach-user-modal.component';
import { SearchStatusComponent } from './modals/attach-user-modal/search-status/search-status.component';
import { SearchRoleComponent } from './modals/attach-user-modal/search-role/search-role.component';
import { SearchDepartmentComponent } from './modals/attach-user-modal/search-department/search-department.component';

const commonComponents = [
  EvaluationDropdownDetailsComponent,
  ConfirmDeleteSurveyModalComponent,
  EvaluationComponent,
  PublicEvaluationComponent,
  LoadingWrapperComponent,
  AttachUserModalComponent
];

@NgModule({
  declarations: [
    ...commonComponents, 
    SearchStatusComponent, 
    SearchRoleComponent, 
    SearchDepartmentComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    CKEditorModule,
    MaterialModule,
    SharedModule
  ],
  exports: [
    TranslateModule,
    commonComponents
  ],
  providers: [
    SafeHtmlPipe, 
    ViewDatePipe
  ],
  entryComponents: [],
})
export class UtilsModule { }
