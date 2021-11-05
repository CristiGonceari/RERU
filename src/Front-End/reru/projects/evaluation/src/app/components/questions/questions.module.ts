import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QuestionsRoutingModule } from './questions-routing.module';
import { QuestionsComponent } from './questions.component';
import { AddEditQustionComponent } from './add-edit-qustion/add-edit-qustion.component';
import { BulkImportQuestionsComponent } from './bulk-import-questions/bulk-import-questions.component';
import { QuestionListComponent } from './question-list/question-list.component';
import { SearchQuestionComponent } from './question-list/search-question/search-question.component';
import { QuestionNameComponent } from './question-list/question-name/question-name.component';
import { QuestionListTableComponent } from './question-list/question-list-table/question-list-table.component';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { QuestionOverviewComponent } from './question-details/question-overview/question-overview.component';
import { QuestionOptionsComponent } from './question-details/question-options/question-options.component';
import { AddOptionComponent } from './question-details/question-options/add-option/add-option.component';
import { DeleteOptionComponent } from './question-details/question-options/delete-option/delete-option.component';


@NgModule({
  declarations: [QuestionsComponent, AddEditQustionComponent, BulkImportQuestionsComponent, QuestionListComponent, SearchQuestionComponent, QuestionNameComponent, QuestionListTableComponent, QuestionDetailsComponent, QuestionOverviewComponent, QuestionOptionsComponent, AddOptionComponent, DeleteOptionComponent],
  imports: [
    CommonModule,
    QuestionsRoutingModule
  ]
})
export class QuestionsModule { }
