import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FaqAddEditComponent } from './faq-add-edit/faq-add-edit.component';
import { FaqListComponent } from './faq-list/faq-list.component';
import { DetailsComponent } from './details/details.component';

const routes: Routes = [
  { path: '', component: FaqListComponent },
  { path: 'faq-details/:id', component: DetailsComponent },
  { path: 'add-article', component: FaqAddEditComponent },
  { path: 'edit-article/:id', component: FaqAddEditComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class FaqRoutingModule { }
