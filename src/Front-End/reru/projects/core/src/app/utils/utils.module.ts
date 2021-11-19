import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Exception404Component } from './exceptions/404/404.component';
import { Exception500Component } from './exceptions/500/500.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateComponent } from './components/date/date.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ConfirmModalComponent } from './modals/confirm-modal/confirm-modal.component';
import { SearchPipe } from './pipes/search.pipe';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';

const commonComponents = [
  Exception404Component,
  Exception500Component,
  DateComponent,
  ConfirmModalComponent,
  SearchPipe,
  SafeHtmlPipe,
  PaginationComponent
];

@NgModule({
  declarations: commonComponents,
  imports: [CommonModule, NgbModule, TranslateModule, HttpClientModule],
  exports: [...commonComponents],
  providers: [SearchPipe, SafeHtmlPipe],
  entryComponents: [ConfirmModalComponent],
})
export class UtilsModule { }
