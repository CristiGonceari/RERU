import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEditRequiredDocumentComponent } from './add-edit-required-document/add-edit-required-document.component';
import { RequiredDocumentsListComponent } from './required-documents-list/required-documents-list.component';

const routes: Routes = [
  { path: '', component: RequiredDocumentsListComponent },
  { path: 'add-document', component: AddEditRequiredDocumentComponent },
  { path: 'edit-document/:id', component: AddEditRequiredDocumentComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RequiredDocumentsRoutingModule { }
