import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import {ReportsComponent} from './reports.component';
import {DetailsComponent} from './details/details.component';

const routes: Routes = [{
  path: '',
  component: ReportsComponent,
  children: [
    { path: '', component: ListComponent, pathMatch: 'full'},
    { path: ':id', component: DetailsComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
