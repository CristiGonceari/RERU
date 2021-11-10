import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionRouteGuard } from '@erp/shared';
// import { UniversalDeleteModalComponent } from 'dist/erp-shared/lib/modals/universal-delete-modal/universal-delete-modal.component';
import { AddEditQuestionComponent } from './add-edit-question/add-edit-question.component';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { AddOptionComponent } from './question-details/question-options/add-option/add-option.component';
import { DeleteOptionComponent } from './question-details/question-options/delete-option/delete-option.component';
import { QuestionOverviewComponent } from './question-details/question-overview/question-overview.component';
import { QuestionListComponent } from './question-list/question-list.component';

const routes: Routes = [
  { 
    path: '', component: QuestionListComponent 
  },
  {
    path: 'add-question',
    component: AddEditQuestionComponent,
    // data: { permission: 'P03010204' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'edit-question/:id', 
    component: AddEditQuestionComponent,
    // data: { permission: 'P03010205' },
    // canActivate: [PermissionRouteGuard],
  },
  // {
  //   path: 'remove/:id', 
  //   component: UniversalDeleteModalComponent,
  //   // data: { permission: 'P03010105' },
  //   // canActivate: [PermissionRouteGuard]
  // },
  {
    path: 'question-detail/:id',
    component: QuestionDetailsComponent,
    // data: { permission: 'P03010201' },
    // canActivate: [PermissionRouteGuard],
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: QuestionOverviewComponent },
      { 
        path: 'options', 
        component: QuestionDetailsComponent,
        // data: { permission: 'P03010302' },
        // canActivate: [PermissionRouteGuard]
      }
    ]
  },
  { 
    path: 'option/:id/add', 
    component: AddOptionComponent,
    // data: { permission: 'P03010303' },
    // canActivate: [PermissionRouteGuard]
  },
  { 
    path: 'option/:id/edit/:id2', 
    component: AddOptionComponent,
    // data: { permission: 'P03010304' },
    // canActivate: [PermissionRouteGuard]
  },
  { 
    path: 'delete-option/:id', 
    component: DeleteOptionComponent,
    // data: { permission: 'P03010305' },
    // canActivate: [PermissionRouteGuard]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class QuestionRoutingModule { }
