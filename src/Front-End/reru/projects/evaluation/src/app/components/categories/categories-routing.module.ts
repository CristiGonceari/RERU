import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QuestionDetailsComponent } from '../questions/question-details/question-details.component';
import { AddEditCategoryComponent } from './add-edit-category/add-edit-category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { CategoryOverviewComponent } from './category-details/category-overview/category-overview.component';
import { CategoryQuestionsComponent } from './category-details/category-questions/category-questions.component';
import { CategoryListComponent } from './category-list/category-list.component';

const routes: Routes = [
  { path: '', component: CategoryListComponent },
  {
    path: 'add-category',
    component: AddEditCategoryComponent,
    data: { permission: 'P03010103' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'edit-category/:id', 
    component: AddEditCategoryComponent,
    data: { permission: 'P03010104' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'question-category/:id',
    component: CategoryDetailsComponent,
    data: { permission: 'P03010101' },
    // canActivate: [PermissionRouteGuard],
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: CategoryOverviewComponent },
      {
        path: 'question-details', 
        component: CategoryQuestionsComponent,
        data: { permission: 'P03010202' },
        // canActivate: [PermissionRouteGuard],
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
