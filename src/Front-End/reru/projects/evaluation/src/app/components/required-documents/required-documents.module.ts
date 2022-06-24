import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { RequiredDocumentsRoutingModule } from './required-documents-routing.module';
import { RequiredDocumentsListComponent } from './required-documents-list/required-documents-list.component';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RequiredDocumentsTableComponent } from './required-documents-table/required-documents-table.component';
import { SearchByMandatoryComponent } from './search-by-mandatory/search-by-mandatory.component';
import { AddEditRequiredDocumentComponent } from './add-edit-required-document/add-edit-required-document.component';


@NgModule({
  declarations: [RequiredDocumentsListComponent, RequiredDocumentsTableComponent, SearchByMandatoryComponent, AddEditRequiredDocumentComponent],
  imports: [
    CommonModule,
    RequiredDocumentsRoutingModule,
    RouterModule,
    TranslateModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  schemas: [
		CUSTOM_ELEMENTS_SCHEMA
 	],
  providers: [DatePipe,
    TranslatePipe,]
})
export class RequiredDocumentsModule { }
