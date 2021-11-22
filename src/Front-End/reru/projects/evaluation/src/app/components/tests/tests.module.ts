import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { TestsRoutingModule } from './tests-routing.module';
import { TestListTableComponent } from './test-list-table/test-list-table.component';
import { AddTestComponent } from './add-test/add-test.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { RouterModule } from '@angular/router';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { MaterialModule } from '../../material.module';
import { UtilsModule } from '../../utils/utils.module';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';
import { OnePerPagePerformingTestComponent } from './one-per-page-performing-test/one-per-page-performing-test.component';
import { SearchStatusComponent } from './test-list/search-status/search-status.component';
import { MultiplePerPagePerformingTestComponent } from './multiple-per-page-performing-test/multiple-per-page-performing-test.component';
import { FinishPageComponent } from './finish-page/finish-page.component';
import { StartTestPageComponent } from './start-test-page/start-test-page.component';
import { TestListComponent } from './test-list/test-list.component';
import { ViewTestResultComponent } from './view-test-result/view-test-result.component';
import { TestVerificationProcessComponent } from './test-verification-process/test-verification-process.component';


@NgModule({
  declarations: [
    TestListTableComponent,
    AddTestComponent,
    OnePerPagePerformingTestComponent,
    SearchStatusComponent,
    MultiplePerPagePerformingTestComponent,
    FinishPageComponent,
    StartTestPageComponent,
    TestListComponent,
    ViewTestResultComponent,
    TestVerificationProcessComponent
  ],
  imports: [
    CommonModule,
    TestsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    TranslateModule,
    SharedModule,
    UtilsModule,
    RouterModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
    CKEditorModule,
    MaterialModule,
    MatButtonModule, 
    MatCheckboxModule, 
    MatProgressBarModule
  ],
  schemas: [
		CUSTOM_ELEMENTS_SCHEMA
 	],
  providers: [DatePipe],
  exports: [StartTestPageComponent]
})
export class TestsModule {}
