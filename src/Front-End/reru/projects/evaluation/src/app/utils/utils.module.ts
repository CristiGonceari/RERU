import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { NgbDateFRParserFormatter } from './services/date-formater/date-parse-formatter.service';
import { NgbDateParserFormatter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DateComponent } from './components/date/date.component';
import { HashOptionInputComponent } from './components/hash-option-input/hash-option-input.component';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { EventCalendarComponent } from './components/event-calendar/event-calendar.component';
import { CalendarChunkPipe } from './pipes/calendar-chunk.pipe';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SharedModule } from '@erp/shared';
import { AttachUserModalComponent } from './components/attach-user-modal/attach-user-modal.component';
import { DateFilterPipe } from './pipes/date-filter.pipe';
import { ConvertPdfDocumentModalComponent } from './modals/convert-pdf-document-modal/convert-pdf-document-modal.component';
import { CKEditorModule } from 'ngx-ckeditor';
import { GenerateDocumentModalComponent } from './modals/generate-document-modal/generate-document-modal.component';
import { CkEditorConfigComponent } from './components/ck-editor-config/ck-editor-config.component';

@NgModule({
  declarations: [
    DateComponent,
    HashOptionInputComponent,
    SafeHtmlPipe,
    EventCalendarComponent,
    CalendarChunkPipe,
    AttachUserModalComponent,
    DateFilterPipe,
    ConvertPdfDocumentModalComponent,
    GenerateDocumentModalComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule, 
    ReactiveFormsModule,
    NgxDropzoneModule,
    SharedModule,
    CKEditorModule
  ],
  providers: [
    { provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
    SafeHtmlPipe,
    DatePipe,
  ],
  exports: [
    SafeHtmlPipe,
    TranslateModule,
    DateComponent,
    EventCalendarComponent,
    DateFilterPipe,
  ]
})
export class UtilsModule { }
