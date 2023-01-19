import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailsComponent } from './details/details.component';
import { EmployeeFunctionComponent } from './employee-function.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [{
  path: '',
  component: EmployeeFunctionComponent, children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', component: ListComponent },
    { path: ':id', component: DetailsComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeFunctionRoutingModule { }
