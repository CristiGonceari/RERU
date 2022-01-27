import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { DocumentsTemplatesTableComponent } from './documents-templates-table/documents-templates-table.component';
import { DocumentsTemplatesComponent } from './documents-templates.component';
import {DocumentsTemplatesRoutingModule } from './documents-templates-routing.module';
import { ListComponent } from './list/list.component';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { DocumentsTemplatesDropdownComponent } from './documents-templates-dropdown/documents-templates-dropdown.component';
import { AddComponent } from './add/add.component';
import { CKEditorModule } from 'ngx-ckeditor';

@NgModule({
  declarations: [
    DocumentsTemplatesTableComponent,
    DocumentsTemplatesComponent,
    ListComponent,
    DocumentsTemplatesDropdownComponent,
    AddComponent
  ],
  imports: [
    CommonModule,
    DocumentsTemplatesRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    NgbModule,
    SharedModule,
		CKEditorModule
  ]
})

export class DocumentsTemplatesModule {
 }
