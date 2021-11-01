import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { CKEditorModule } from 'ngx-ckeditor';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';

const commonComponents = [
  LoadingSpinnerComponent,
];

@NgModule({
  declarations: commonComponents,
  imports: [
    HttpClientModule,
    CommonModule,
    NgbModule,
    RouterModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    CKEditorModule,
  ],
  exports: [
    TranslateModule,
    commonComponents
  ],
  providers: [],
})
export class UtilsModule { }
