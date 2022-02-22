import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditTestTemplateComponent } from './add-edit-test-templates/add-edit-test-templates.component';
import { AddTestTemplateOptionsComponent } from './test-template-details/add-test-template-options/add-test-template-options.component';
import { TestTemplateDetailsComponent } from './test-template-details/test-template-details.component';
import { AddCategoryComponent } from './test-template-details/test-template-categories/add-category/add-category.component';
import { TestTemplatesCategoriesComponent } from './test-template-details/test-template-categories/test-templates-categories.component';
import { ViewCategoryComponent } from './test-template-details/test-template-categories/view-category/view-category.component';
import { TestTemplateOverviewComponent } from './test-template-details/test-templates-overview/test-template-overview.component';
import { AddTestTemplateRulesComponent } from './test-template-details/test-templates-rules/add-test-template-rules/add-test-template-rules.component';
import { TestTemplatesRulesComponent } from './test-template-details/test-templates-rules/test-template-rules.component';
import { TestTemplateListComponent } from './test-template-list/test-template-list.component';

const routes: Routes = [
  { path: '', component: TestTemplateListComponent },
  { path: 'add-test-type', component: AddEditTestTemplateComponent },
  { path: 'edit-test-type/:id', component: AddEditTestTemplateComponent },
  {
    path: 'type-details/:id',
    component: TestTemplateDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: TestTemplateOverviewComponent },
      { path: 'options', component: AddTestTemplateOptionsComponent },
      { path: 'categories', component: TestTemplatesCategoriesComponent},
      { path: 'categories-view/:id', component: ViewCategoryComponent},
      { path: 'rules', component: TestTemplatesRulesComponent },
    ]
  },
  { path: 'category/:id/add', component: AddCategoryComponent },
  { path: 'rules/:id/add', component: AddTestTemplateRulesComponent }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class TestTemplateRoutingModule { }
