import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EvaluationComponent } from './evaluation.component';
import { EvaluationProcessComponent } from './evaluation-process/evaluation-process.component';
import { ListComponent } from './list/list.component';
import { EvaluationsSetupComponent } from './evaluations-setup/evaluations-setup.component';

const routes: Routes = [{
  path: '',
  component: EvaluationComponent,
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full'},
    { path: 'evaluate/:id', component: EvaluationProcessComponent },
    { path: 'accept/:id', component: EvaluationProcessComponent },
    { path: 'countersign/:id', component: EvaluationProcessComponent },
    { path: 'acknowledge/:id', component: EvaluationProcessComponent },
    { path: 'setup', component: EvaluationsSetupComponent },
    { path: 'list', component: ListComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EvaluationRoutingModule { }
