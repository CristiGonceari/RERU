import { NgModule } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { TranslatePipe, TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@erp/shared';
import { MaterialModule } from '../../material.module';
import { NgxDropzoneModule } from 'ngx-dropzone';

import { CandidateRegistrationFluxComponent } from './candidate-registration-flux.component';
import { CandidateRegistrationFluxRoutingModule } from './candidate-registration-flux-routing.module';
import { GeneralDataFormComponent } from './general-data-form/general-data-form.component';
import { BulletinComponent } from './bulletin/bulletin.component';
import { StudyComponent } from './study/study.component';
import { MaterialStatusComponent } from './material-status/material-status.component';
import { MilitaryObligationComponent } from './military-obligation/military-obligation.component';
import { AutobiographyComponent } from './autobiography/autobiography.component';
import { DeclarationComponent } from './declaration/declaration.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { DataService } from './data.service';

// import { CKEditorModule } from 'ngx-ckeditor';



@NgModule({
  declarations: [
    CandidateRegistrationFluxComponent,
    GeneralDataFormComponent,
    BulletinComponent,
    StudyComponent,
    MaterialStatusComponent,
    MilitaryObligationComponent,
    AutobiographyComponent,
    DeclarationComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    CandidateRegistrationFluxRoutingModule,
    UtilsModule,
    NgbModule,
    SharedModule,
    MaterialModule,
    NgxDropzoneModule,
    CKEditorModule,
    
  ],
  providers: [
    TranslatePipe,
    Location,
    DataService
  ]
})
export class CandidateRegistrationFluxModule { }
