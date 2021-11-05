import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { CategoriesComponent } from './categories.component';
import { AddEditCategoryComponent } from './add-edit-category/add-edit-category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryOverviewComponent } from './category-details/category-overview/category-overview.component';
import { CategoryQuestionsComponent } from './category-details/category-questions/category-questions.component';
import { SearchCategoryComponent } from './category-list/search-category/search-category.component';
import { CategoryNameComponent } from './category-list/category-name/category-name.component';
import { CategoryListTableComponent } from './category-list/category-list-table/category-list-table.component';


@NgModule({
  declarations: [CategoriesComponent, AddEditCategoryComponent, CategoryDetailsComponent, CategoryListComponent, CategoryOverviewComponent, CategoryQuestionsComponent, SearchCategoryComponent, CategoryNameComponent, CategoryListTableComponent],
  imports: [
    CommonModule,
    CategoriesRoutingModule
  ]
})
export class CategoriesModule { }
