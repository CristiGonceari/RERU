import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DocumentsTemplatesComponent } from './documents-templates.component';

const routes: Routes = [{
  path: '',
  component: DocumentsTemplatesComponent,
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class DocumentsTemplatesRoutingModule { }
