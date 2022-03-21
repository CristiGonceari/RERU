import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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

@NgModule({
  declarations: [
    DateComponent,
    HashOptionInputComponent,
    SafeHtmlPipe,
    EventCalendarComponent,
    CalendarChunkPipe,
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
    SafeHtmlPipe,

  ],
  exports: [
    SafeHtmlPipe,
    TranslateModule,
    DateComponent,
    EventCalendarComponent,
    
  ]
})
export class UtilsModule { }
