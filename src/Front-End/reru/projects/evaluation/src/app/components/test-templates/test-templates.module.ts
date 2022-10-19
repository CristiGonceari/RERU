import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { TestTemplateDetailsComponent } from './test-template-details/test-template-details.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { UtilsModule } from '../../utils/utils.module';
import { DragulaModule } from 'ng2-dragula';
import { TestTemplateListComponent } from './test-template-list/test-template-list.component'
import { TestTemplateRoutingModule } from './test-templates-routing.module';
import { TestTemplateListTableComponent } from './test-template-list/test-template-list-table/test-template-list-table.component';
import { TestTemplateOverviewComponent } from './test-template-details/test-templates-overview/test-template-overview.component'
import { TestTemplateSearchComponent } from './test-template-list/test-template-search/test-template-search.component'
import { AddEditTestTemplateComponent } from './add-edit-test-templates/add-edit-test-templates.component'
import { AddTestTemplateOptionsComponent } from './test-template-details/add-test-template-options/add-test-template-options.component'
import { AddCategoryComponent } from './test-template-details/test-template-categories/add-category/add-category.component'
import { TestTemplatesCategoriesComponent } from './test-template-details/test-template-categories/test-templates-categories.component'
import { TestTemplatesRulesComponent } from './test-template-details/test-templates-rules/test-template-rules.component'
import { AddTestTemplateRulesComponent } from './test-template-details/test-templates-rules/add-test-template-rules/add-test-template-rules.component'
import { CategoryQuestionsTableComponent } from './test-template-details/test-template-categories/add-category/category-questions-table/category-questions-table.component';
import { ViewCategoryComponent } from './test-template-details/test-template-categories/view-category/view-category.component';
import { SearchQualifyingTypeComponent } from './test-template-list/search-qualifying-type/search-qualifying-type.component'
import { SearchTestModeComponent } from './test-template-list/search-test-mode/search-test-mode.component'
import { TagInputModule } from 'ngx-chips';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    TestTemplateListComponent,
    TestTemplateListTableComponent,
    TestTemplateDetailsComponent,
    TestTemplateOverviewComponent,
    TestTemplateSearchComponent,
    AddEditTestTemplateComponent,
    AddTestTemplateOptionsComponent,
    AddCategoryComponent,
    TestTemplatesCategoriesComponent,
    TestTemplatesRulesComponent,
    AddTestTemplateRulesComponent,
    CategoryQuestionsTableComponent,
    ViewCategoryComponent,
    SearchTestModeComponent,
    SearchQualifyingTypeComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    TranslateModule,
    SharedModule,
    TestTemplateRoutingModule,
    CKEditorModule,
    UtilsModule,
    TagInputModule,
    MatSelectModule,
    DragulaModule.forRoot()
  ],
  exports: [TestTemplateListComponent]
})
export class TestTemplatesModule { }
