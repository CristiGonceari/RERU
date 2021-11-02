import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PontajData } from './pontaj-data.component';

const routes: Routes = [
  { path: '',
    component: PontajData
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class PontajDataTableRoutingModule { }
