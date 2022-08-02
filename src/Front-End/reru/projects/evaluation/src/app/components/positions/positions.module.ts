import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@erp/shared';
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

@NgModule({
  declarations: [AddEditPositionComponent, PositionsDiagramComponent],
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
    ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class PositionsModule { }
