import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryQuestionsOptionsComponent } from '../categories/category-details/category-questions-options/category-questions-options.component';
import { AddEditQuestionComponent } from './add-edit-question/add-edit-question.component';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { QuestionOverviewComponent } from './question-details/question-overview/question-overview.component';
import { QuestionListComponent } from './question-list/question-list.component';
import { AddOptionComponent } from '../categories/category-details/category-questions-options/add-option/add-option.component';

const routes: Routes = [
  { path: '', component: QuestionListComponent },
  { path: 'add-question', component: AddEditQuestionComponent },
  { path: 'edit-question/:id', component: AddEditQuestionComponent },
  {
    path: 'question-detail/:id',
    component: QuestionDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: QuestionOverviewComponent },
      { path: 'question-options', component: CategoryQuestionsOptionsComponent},
      { path: 'option/:id/add', component: AddOptionComponent },
      { path: 'option/:id/edit/:id2', component: AddOptionComponent } 
    ]
  },

];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class QuestionRoutingModule { }
