import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CKEditorModule } from 'ngx-ckeditor';
import { MaterialModule } from '../material.module';
import { SharedModule } from '@erp/shared';

import { EvaluationDropdownDetailsComponent } from './components/evaluation-dropdown-details/evaluation-dropdown-details.component';
import { ConfirmDeleteEvaluationModalComponent } from './modals/confirm-delete-evaluation-modal/confirm-delete-evaluation-modal.component';
import { LoadingWrapperComponent } from './components/loading-wrapper/loading-wrapper.component';
import { AttachUserModalComponent } from './modals/attach-user-modal/attach-user-modal.component';
import { SearchStatusComponent } from './modals/attach-user-modal/search-status/search-status.component';
import { SearchRoleComponent } from './modals/attach-user-modal/search-role/search-role.component';
import { SearchDepartmentComponent } from './modals/attach-user-modal/search-department/search-department.component';
import { FormComponent } from './components/form/form.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';
import { QualificationSelectComponent } from './components/qualification-select/qualification-select.component';

const commonComponents = [
  EvaluationDropdownDetailsComponent,
  ConfirmDeleteEvaluationModalComponent,
  LoadingWrapperComponent,
  AttachUserModalComponent,
  FormComponent
];

@NgModule({
  declarations: [
    ...commonComponents, 
    SearchStatusComponent,
    SearchRoleComponent,
    SearchDepartmentComponent,
    QualificationSelectComponent,
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
    SharedModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule
  ],
  exports: [
    TranslateModule,
    ...commonComponents
  ]
})
export class UtilsModule { }
