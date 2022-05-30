import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditDepartmentComponent } from './add-edit-department/add-edit-department.component';
import { DepartmentDetailsComponent } from './department-details/department-details.component';
import { DepartmentsComponent } from './departments.component';
import { DepartmentOverviewComponent } from './department-details/department-overview/department-overview.component';

const routes: Routes = [
  { path: '', component: DepartmentsComponent },
  { 
    path: 'department-details/:id',
    component: DepartmentDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: DepartmentOverviewComponent }
    ]
  }
  // { path: 'add-department', component: AddEditDepartmentComponent },
  // { path: 'edit-department/:id', component: AddEditDepartmentComponent }
]

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class DepartmentsRoutingModule { }
