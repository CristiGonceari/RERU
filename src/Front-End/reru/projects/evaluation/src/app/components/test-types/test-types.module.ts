import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { TestTypeDetailsComponent } from './test-type-details/test-type-details.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { UtilsModule } from '../../utils/utils.module';
import { DragulaModule } from 'ng2-dragula';
import { TestTypeListComponent } from './test-type-list/test-type-list.component'
import { TestTypeRoutingModule } from './test-types-routing.module';
import { TestTypeListTableComponent } from './test-type-list/test-type-list-table/test-type-list-table.component';
import { TestTypesOverviewComponent } from './test-type-details/test-types-overview/test-types-overview.component'
import { TestTypeNameComponent } from './test-type-list/test-type-name/test-type-name.component'
import { TestTypeSearchComponent } from './test-type-list/test-type-search/test-type-search.component'
import { AddEditTestTypesComponent } from './add-edit-test-types/add-edit-test-types.component'
import { AddTestTypeOptionsComponent } from './test-type-details/add-test-type-options/add-test-type-options.component'
import { AddCategoryComponent } from './test-type-details/test-types-categories/add-category/add-category.component'
import { TestTypesCategoriesComponent } from './test-type-details/test-types-categories/test-types-categories.component'
import { DeleteCategoryComponent } from './test-type-details/test-types-categories/delete-category/delete-category.component'
import { TestTypesRulesComponent } from './test-type-details/test-types-rules/test-types-rules.component'
import { AddTestTypeRulesComponent } from './test-type-details/test-types-rules/add-test-type-rules/add-test-type-rules.component'
import { CategoryQuestionsTableComponent } from './test-type-details/test-types-categories/add-category/category-questions-table/category-questions-table.component'

@NgModule({
  declarations: [
    TestTypeListComponent,
    TestTypeListTableComponent,
    TestTypeDetailsComponent,
    TestTypesOverviewComponent,
    TestTypeNameComponent,
    TestTypeSearchComponent,
    AddEditTestTypesComponent,
    AddTestTypeOptionsComponent,
    AddCategoryComponent,
    TestTypesCategoriesComponent,
    DeleteCategoryComponent,
    TestTypesRulesComponent,
    AddTestTypeRulesComponent,
    CategoryQuestionsTableComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    TranslateModule,
    SharedModule,
    TestTypeRoutingModule,
    CKEditorModule,
    UtilsModule,
    DragulaModule.forRoot()
  ],
  exports: [TestTypeListComponent]
})
export class TestTypesModule { }
