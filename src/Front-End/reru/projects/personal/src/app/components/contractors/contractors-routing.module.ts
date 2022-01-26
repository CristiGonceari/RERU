import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContractorsComponent } from './contractors.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { GenerateOrderComponent } from './generate-order/generate-order.component';

const routes: Routes = [
  { path: '',
    component: ContractorsComponent, children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', component: ListComponent },
      { path: 'new', component: AddComponent },
      { path: 'new/:id', component: AddComponent },
      { path: 'order/:id', component: GenerateOrderComponent },
      { path: 'edit/:id', component: EditComponent },
      { path: ':id', loadChildren: () => import('./details/details.module').then(m => m.DetailsModule) },
    ] 
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class ContractorsRoutingModule { }
