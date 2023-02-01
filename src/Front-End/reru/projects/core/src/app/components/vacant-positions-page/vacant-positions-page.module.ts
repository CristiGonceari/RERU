import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { SharedModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { VacantPositionPageRoutingModule } from './vacant-position-page-routing.module';
import { RouterModule } from '@angular/router';
import { VacantPositionsPageComponent } from './vacant-positions-page.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CommonModule, DatePipe } from '@angular/common';
// import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@busacca/ng-pick-datetime';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    NgbModule,
    // OwlDateTimeModule,
    // OwlNativeDateTimeModule,
    VacantPositionPageRoutingModule,
    NgxDropzoneModule,
    CKEditorModule
  ],
  providers: [
    TranslatePipe,
    Location,
    DatePipe
  ],
  declarations: [VacantPositionsPageComponent],
})
export class VacantPositionsPageModule { }
