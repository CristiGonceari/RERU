import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentsComponent } from './departments.component';

const routes: Routes = [
  { path: '', component: DepartmentsComponent },
  // { 
  //   path: 'faq-details/:id',
  //   component: FaqDetailsComponent,
  //   children: [
  //     { path: '', redirectTo: 'overview', pathMatch: 'full' },
  //     { path: 'overview', component: FaqOverviewComponent }
  //   ]
  // },
  // { path: 'add-article', component: FaqAddEditComponent },
  // { path: 'edit-article/:id', component: FaqAddEditComponent }
]

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class DepartmentsRoutingModule { }
