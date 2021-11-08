import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { QuestionListComponent } from './question-list/question-list.component';
import { TranslateModule } from '@ngx-translate/core';
import { BulkImportQuestionsComponent } from './bulk-import-questions/bulk-import-questions.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { QuestionRoutingModule } from './questions-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { AddEditQuestionComponent } from './add-edit-question/add-edit-question.component';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { TagInputModule } from 'ngx-chips';
import { QuestionOverviewComponent } from './question-details/question-overview/question-overview.component';
import { QuestionNameComponent } from './question-list/question-name/question-name.component'
import { SearchQuestionComponent } from './question-list/search-question/search-question.component'
import { QuestionOptionsComponent } from './question-details/question-options/question-options.component'
import { AddOptionComponent } from './question-details/question-options/add-option/add-option.component';
import { DeleteOptionComponent } from './question-details/question-options/delete-option/delete-option.component';
import { QuestionListTableComponent } from './question-list/question-list-table/question-list-table.component';

@NgModule({
	declarations: [
    BulkImportQuestionsComponent,
    QuestionListComponent,
    AddEditQuestionComponent,
    QuestionDetailsComponent,
    QuestionOverviewComponent,
    QuestionNameComponent,
    SearchQuestionComponent,
    QuestionOptionsComponent,
    AddOptionComponent,
    DeleteOptionComponent,
    QuestionListTableComponent,
  ],
	imports: [
	  CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    NgxDropzoneModule,
    TranslateModule,
    SharedModule,
    RouterModule,
    QuestionRoutingModule,
    HttpClientModule,
    UtilsModule,
    TagInputModule
  ],
	exports: [QuestionListComponent],
	entryComponents: [],
})
export class QuestionsModule {}
