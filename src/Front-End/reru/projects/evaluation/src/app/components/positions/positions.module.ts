import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule, SvgModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PositionsRoutingModule } from './positions-routing.module';
import { AddEditPositionComponent } from './add-edit-position/add-edit-position.component';
import { TagInputModule } from 'ngx-chips';
import { NgxDropzoneModule } from 'ngx-dropzone';
import {MatSelectModule} from '@angular/material/select';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PositionsDiagramComponent } from './positions-diagram/positions-diagram.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';
import { PositionDetailsComponent } from './position-details/position-details.component';
import { PositionOverviewComponent } from './position-details/position-overview/position-overview.component';
import { DiagramComponent } from './position-details/diagram/diagram.component';
import { PositionsComponent } from './positions.component';
import { SearchMedicalColumnComponent } from './search-medical-column/search-medical-column.component';
import { PositionAddTestComponent } from './position-details/position-add-test/position-add-test.component';

@NgModule({
  declarations: [
    PositionsComponent,
    AddEditPositionComponent, 
    PositionsDiagramComponent, 
    PositionDetailsComponent, 
    PositionOverviewComponent, 
    DiagramComponent, 
    SearchMedicalColumnComponent,
    PositionAddTestComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    NgxDropzoneModule,
    ReactiveFormsModule,
    TranslateModule,
    PositionsRoutingModule,
    NgbModule,
    TagInputModule,
    MatSelectModule,
    CKEditorModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
    SvgModule
    ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class PositionsModule { }
