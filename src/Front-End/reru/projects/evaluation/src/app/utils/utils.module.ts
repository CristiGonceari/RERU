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

@NgModule({
  declarations: [
    LoadingSpinnerComponent,
    DateComponent,
    HashOptionInputComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule, 
    ReactiveFormsModule,
  ],
  providers: [
    { provide: NgbDateParserFormatter, useClass: NgbDateFRParserFormatter },
    LoadingSpinnerComponent
  ],
  exports: [
    LoadingSpinnerComponent,
    TranslateModule,
    DateComponent
  ]
})
export class UtilsModule { }
