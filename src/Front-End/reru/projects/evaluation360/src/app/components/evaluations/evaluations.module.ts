import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { EvaluationsTableComponent } from './evaluations-table/evaluations-table.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { UtilsModule } from '../../utils/utils.module';
import { EvaluationsRoutingModule } from './evaluations-routing.module';
import { EvaluationsDropdownDetailsComponent } from './evaluations-dropdown-details/evaluations-dropdown-details.component';
import { ViewComponent } from './view/view.component';
import { RouterModule } from '@angular/router';
import { EvaluationsComponent } from './evaluations.component';

@NgModule({
  declarations: [
    ListComponent, 
    EvaluationsTableComponent, 
    EvaluationsDropdownDetailsComponent, 
    ViewComponent,
    EvaluationsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    EvaluationsRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    NgbModule,
    SharedModule,
    UtilsModule
  ]
})
export class EvaluationsModule { }
