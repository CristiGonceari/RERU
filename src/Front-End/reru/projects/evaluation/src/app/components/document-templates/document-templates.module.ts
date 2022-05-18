import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentTemplatesRoutingModule } from './document-templates-routing.module';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { CKEditorModule } from 'ngx-ckeditor';
import { HttpClientModule } from '@angular/common/http';
import { DocumentTemplatesComponent } from './document-templates.component';
import { AddComponent } from './add/add.component';
import { ListComponent } from './list/list.component';
import { DocumentTemplatesTableComponent } from './document-templates-table/document-templates-table.component';

@NgModule({
  declarations: [
    DocumentTemplatesComponent,
    AddComponent,
    ListComponent,
    DocumentTemplatesTableComponent,
  ],
  imports: [
    CommonModule,
    DocumentTemplatesRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    NgbModule,
    SharedModule,
		CKEditorModule,
  ]
})
export class DocumentTemplatesModule { }
