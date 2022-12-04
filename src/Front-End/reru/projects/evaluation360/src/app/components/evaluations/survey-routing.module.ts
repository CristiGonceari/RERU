import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './survey.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { EvaluationsSetupComponent } from './evaluations-setup/evaluations-setup.component';

const routes: Routes = [{
  path: '',
  component: SurveyComponent,
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full'},
    { path: 'evaluate/:id', component: CreateComponent },
    { path: 'accept/:id', component: CreateComponent },
    { path: 'countersign/:id', component: CreateComponent },
    { path: 'setup', component: EvaluationsSetupComponent },
    { path: 'list', component: ListComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveyRoutingModule { }
