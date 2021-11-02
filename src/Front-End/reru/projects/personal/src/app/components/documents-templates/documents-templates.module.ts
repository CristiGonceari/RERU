import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { DocumentsTemplatesComponent } from './documents-templates.component';
import {DocumentsTemplatesRoutingModule } from './documents-templates-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CKEditorModule } from 'ngx-ckeditor';

@NgModule({
  declarations: [
    DocumentsTemplatesComponent,
  ],
  imports: [
    CommonModule,
    DocumentsTemplatesRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    NgbModule,
    SharedModule,
		CKEditorModule
  ]
})

export class DocumentsTemplatesModule {
 }
