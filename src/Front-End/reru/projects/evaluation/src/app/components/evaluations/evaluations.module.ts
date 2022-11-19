import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EvaluationsRoutingModule } from './evaluations-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';
import { MaterialModule } from '../../material.module';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { EvaluationsListTableComponent } from './evaluations-list-table/evaluations-list-table.component';
import { AddEvaluationComponent } from './add-evaluation-list/add-evaluation/add-evaluation.component';
import { AddEvaluationListComponent } from './add-evaluation-list/add-evaluation-list.component';
import { StartEvaluationPageComponent } from './start-evaluation-page/start-evaluation-page.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PerformingEvaluationComponent } from './performing-evaluation/performing-evaluation.component';
import { EvaluationsTableComponent } from './evaluations-list-table/evaluations-table/evaluations-table.component';
import { SearchResultComponent } from './evaluations-list-table/search-result/search-result.component';
import { ViewEvaluationResultComponent } from './view-evaluation-result/view-evaluation-result.component';

@NgModule({
  declarations: [
    EvaluationsListTableComponent,
    AddEvaluationComponent,
    AddEvaluationListComponent,
    StartEvaluationPageComponent,
    PerformingEvaluationComponent,
    EvaluationsTableComponent,
    SearchResultComponent,
    ViewEvaluationResultComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule, 
    NgbModule,
    TranslateModule,
    SharedModule,
    RouterModule,
    HttpClientModule,
    UtilsModule,
    EvaluationsRoutingModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
    MaterialModule,
    NgxDropzoneModule,
    CKEditorModule,
    // MatButtonModule, 
    // MatCheckboxModule, 
    // MatProgressBarModule
  ]
})
export class EvaluationsModule { }
