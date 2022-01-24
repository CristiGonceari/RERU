import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbDateFRParserFormatter } from './services/date-formater/date-parse-formatter.service';
import { NgbDateParserFormatter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DateComponent } from './components/date/date.component';
import { HashOptionInputComponent } from './components/hash-option-input/hash-option-input.component';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { EventCalendarComponent } from './components/event-calendar/event-calendar.component';
import { CalendarChunkPipe } from './pipes/calendar-chunk.pipe';
import { AddEditMediaFileComponent } from './components/add-edit-media-file/add-edit-media-file.component';
import { GetMediaFileComponent } from './components/get-media-file/get-media-file.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SharedModule } from '@erp/shared';
import { ShowImageModalComponent } from './components/show-image-modal/show-image-modal.component';

@NgModule({
  declarations: [
    LoadingSpinnerComponent,
    DateComponent,
    HashOptionInputComponent,
    SafeHtmlPipe,
    EventCalendarComponent,
    CalendarChunkPipe,
    AddEditMediaFileComponent,
    GetMediaFileComponent,
    ShowImageModalComponent,
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
    SharedModule
  ],
  providers: [
    { provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
    LoadingSpinnerComponent,
    SafeHtmlPipe,
    AddEditMediaFileComponent,
    GetMediaFileComponent
  ],
  exports: [
    LoadingSpinnerComponent,
    SafeHtmlPipe,
    TranslateModule,
    DateComponent,
    EventCalendarComponent,
    AddEditMediaFileComponent,
    GetMediaFileComponent
  ]
})
export class UtilsModule { }
