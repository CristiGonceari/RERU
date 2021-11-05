import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrganigramComponent } from './organigram.component';

const routes: Routes = [{
  path: '',
  component: OrganigramComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganigramRoutingModule { }
