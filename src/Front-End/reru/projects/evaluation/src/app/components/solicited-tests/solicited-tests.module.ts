import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { SolicitedTestsComponent } from '../solicited-tests/solicited-tests.component';
import { SolicitedTestRoutingModule } from '../solicited-tests/solicited-test-routing.module';
import { SolicitedTestListComponent } from './solicited-test-list/solicited-test-list.component';
import { SolicitedTestsTableComponent } from './solicited-test-list/solicited-tests-table/solicited-tests-table.component';
import { ApproveSolicitedTestComponent } from './solicited-test-list/approve-solicited-test/approve-solicited-test.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';

@NgModule({
  declarations: [
    SolicitedTestsComponent,
    SolicitedTestListComponent,
    SolicitedTestsTableComponent,
    ApproveSolicitedTestComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbModule,
    NgxDropzoneModule,
    TranslateModule,
    SharedModule,
    RouterModule,
    HttpClientModule,
    UtilsModule,
    SolicitedTestRoutingModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule
  ]
})
export class SolicitedTestsModule { }
