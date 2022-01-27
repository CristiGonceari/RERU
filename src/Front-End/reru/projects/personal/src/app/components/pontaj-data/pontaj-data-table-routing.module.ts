import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { PontajData } from './pontaj-data.component';

const routes: Routes = [
  { path: '',
    component: PontajData, children: [
      { path: '', component: ListComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class PontajDataTableRoutingModule { }
