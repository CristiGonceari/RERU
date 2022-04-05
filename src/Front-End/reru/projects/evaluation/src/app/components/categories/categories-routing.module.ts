import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEditCategoryComponent } from './add-edit-category/add-edit-category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { CategoryOverviewComponent } from './category-details/category-overview/category-overview.component';
import { CategoryQuestionsComponent } from './category-details/category-questions/category-questions.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryQuestionsOptionsComponent } from './category-details/category-questions-options/category-questions-options.component';
import { AddOptionComponent } from './category-details/category-questions-options/add-option/add-option.component';
import { AddEditQuestionComponent } from '../questions/add-edit-question/add-edit-question.component';


const routes: Routes = [
  { path: '', component: CategoryListComponent },
  { path: 'add-category', component: AddEditCategoryComponent },
  { path: 'edit-category/:id', component: AddEditCategoryComponent },
  { 
    path: 'question-category/:id',
    component: CategoryDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: CategoryOverviewComponent },
      { path: 'question-details', component: CategoryQuestionsComponent },
      { path: 'edit-question/:id', component: AddEditQuestionComponent },
      { path: 'question-options/:id', component: CategoryQuestionsOptionsComponent},
      { path: 'option/:id/add', component: AddOptionComponent },
      { path: 'option/:id/edit/:id2', component: AddOptionComponent } 
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
