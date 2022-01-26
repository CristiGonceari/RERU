import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrganigramComponent } from './organigram.component';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [{
  path: '',
  component: OrganigramComponent,
  children: [
    { path: '', component: ListComponent },
    { path: 'new', component: AddComponent },
    { path: 'edit/:id', component: EditComponent },
    { path: ':id', component: DetailsComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganigramRoutingModule { }
