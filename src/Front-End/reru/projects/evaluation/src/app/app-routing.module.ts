import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutsComponent } from './components/layouts/layouts.component';


const routes: Routes = [{
  path: '',
  component: LayoutsComponent,
  canActivate: [],
  children: []
}]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
