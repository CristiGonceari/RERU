import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VacationComponent } from './vacation.component';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: '',
    component: VacationComponent, children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', component: ListComponent },
      { path: 'new', component: AddComponent },
      { path: 'edit/:id', component: EditComponent },
      { path: ':id', component: DetailsComponent }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VacationRoutingModule { }
