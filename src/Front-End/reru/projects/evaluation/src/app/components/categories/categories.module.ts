import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { CategoryOverviewComponent } from './category-details/category-overview/category-overview.component';

@NgModule({
  declarations: [
    CategoryDetailsComponent,
    CategoryOverviewComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    TranslateModule,
    SharedModule,
    RouterModule,
    HttpClientModule,
    UtilsModule
  ],
  exports: [
  ],
  entryComponents: [
  ],
  providers: [NgbActiveModal]
})
export class CategoriesModule {
}
