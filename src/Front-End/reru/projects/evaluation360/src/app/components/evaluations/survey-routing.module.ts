import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './survey.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { EvaluationsSetupComponent } from './evaluations-setup/evaluations-setup.component';
import { EvaluationProcessComponent } from './evaluations-process/evaluations-process.component';
import { EvalationsAcceptComponent } from './evaluations-accept/evaluations-accept.component';
import { EvaluationCountersignComponent } from './evaluation-countersign/evaluation-countersign.component';

const routes: Routes = [{
  path: '',
  component: SurveyComponent,
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full'},
    { path: 'new', component: CreateComponent },
    { path: 'evaluate/:id', component: EvaluationProcessComponent },
    { path: 'accept/:id', component: EvalationsAcceptComponent },
    { path: 'countersign/:id', component: EvaluationCountersignComponent },
    { path: 'setup', component: EvaluationsSetupComponent },
    { path: 'list', component: ListComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveyRoutingModule { }
