import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';
import { CategoriesRoutingModule } from './categories-routing.module';
import { AddEditCategoryComponent } from './add-edit-category/add-edit-category.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryListTableComponent } from './category-list/category-list-table/category-list-table.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchCategoryComponent } from './category-list/search-category/search-category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { CategoryOverviewComponent } from './category-details/category-overview/category-overview.component';
import { CategoryQuestionsComponent } from './category-details/category-questions/category-questions.component';
import { CategoryQuestionsOptionsComponent } from './category-details/category-questions-options/category-questions-options.component';
import { AddOptionComponent } from './category-details/category-questions-options/add-option/add-option.component';
import { NgxDropzoneModule } from 'ngx-dropzone';

@NgModule({
  declarations: [
    AddEditCategoryComponent, 
    CategoryListComponent, 
    CategoryListTableComponent, 
    SearchCategoryComponent, 
    CategoryDetailsComponent, 
    CategoryOverviewComponent, 
    CategoryQuestionsComponent, 
    CategoryQuestionsOptionsComponent, 
    AddOptionComponent
  ],
  

  imports: [
    CommonModule,
    CategoriesRoutingModule,
    SharedModule,
    UtilsModule,
    HttpClientModule,
    RouterModule,
    TranslateModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NgxDropzoneModule
  ]
})
export class CategoriesModule { }
