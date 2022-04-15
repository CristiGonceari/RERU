import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Exception404Component } from './exceptions/404/404.component';
import { Exception500Component } from './exceptions/500/500.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateComponent } from './components/date/date.component';
import { SearchPipe } from './pipes/search.pipe';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { WatchInfoVideoModalComponent } from './modals/watch-info-video-modal/watch-info-video-modal.component';
import { ImportUsersModalComponent } from './modals/import-users-modal/import-users-modal.component';

const commonComponents = [
  Exception404Component,
  Exception500Component,
  DateComponent,
  SearchPipe,
  SafeHtmlPipe,
  WatchInfoVideoModalComponent,
  ImportUsersModalComponent
];

@NgModule({
  declarations: commonComponents,
  imports: [CommonModule, NgbModule, TranslateModule, HttpClientModule],
  exports: [...commonComponents],
  providers: [SearchPipe, SafeHtmlPipe],
})
export class UtilsModule { }
