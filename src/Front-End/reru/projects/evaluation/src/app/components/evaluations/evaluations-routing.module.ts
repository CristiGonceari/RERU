import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEvaluationListComponent } from './add-evaluation-list/add-evaluation-list.component';
import { EvaluationsComponent } from './evaluations.component';

const routes: Routes = [
  {path: '', component: EvaluationsComponent},
  {path: 'add', component: AddEvaluationListComponent},
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class EvaluationsRoutingModule { }
