import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DetailsModalComponent } from './modals/details-modal/details-modal.component';
import { DeleteLogsModalComponent } from './modals/delete-logs-modal/delete-logs-modal.component';
import { DateFilterPipe } from './pipes/date-filter.pipe';



@NgModule({
  declarations: [DetailsModalComponent, DeleteLogsModalComponent, DateFilterPipe],
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule, 
    ReactiveFormsModule,
  ],
  exports: [
    TranslateModule,
    DateFilterPipe
  ]
})
export class UtilsModule { }
