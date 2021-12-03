import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditTestTypesComponent } from './add-edit-test-types/add-edit-test-types.component';
import { AddTestTypeOptionsComponent } from './test-type-details/add-test-type-options/add-test-type-options.component';
import { TestTypeDetailsComponent } from './test-type-details/test-type-details.component';
import { AddCategoryComponent } from './test-type-details/test-types-categories/add-category/add-category.component';
import { TestTypesCategoriesComponent } from './test-type-details/test-types-categories/test-types-categories.component';
import { ViewCategoryComponent } from './test-type-details/test-types-categories/view-category/view-category.component';
import { TestTypesOverviewComponent } from './test-type-details/test-types-overview/test-types-overview.component';
import { AddTestTypeRulesComponent } from './test-type-details/test-types-rules/add-test-type-rules/add-test-type-rules.component';
import { TestTypesRulesComponent } from './test-type-details/test-types-rules/test-types-rules.component';
import { TestTypeListComponent } from './test-type-list/test-type-list.component';

const routes: Routes = [
  { path: '', component: TestTypeListComponent },
  { path: 'add-test-type', component: AddEditTestTypesComponent },
  { path: 'edit-test-type/:id', component: AddEditTestTypesComponent },
  {
    path: 'type-details/:id',
    component: TestTypeDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: TestTypesOverviewComponent },
      {  path: 'options', component: AddTestTypeOptionsComponent },
      { path: 'categories', component: TestTypesCategoriesComponent},
      { path: 'categories-view/:id', component: ViewCategoryComponent},
      { path: 'rules', component: TestTypesRulesComponent },
    ]
  },
  { path: 'category/:id/add', component: AddCategoryComponent },
  { path: 'rules/:id/add', component: AddTestTypeRulesComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class TestTypeRoutingModule { }
