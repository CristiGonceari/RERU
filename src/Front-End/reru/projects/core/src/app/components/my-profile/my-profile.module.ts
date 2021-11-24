import { MyProfileRoutingModule } from './my-profile-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule, SvgModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { MyProfileComponent } from './my-profile.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ChangePersonalDataComponent } from './change-personal-data/change-personal-data.component';
import { OverviewProfileComponent } from './overview-profile/overview-profile.component';
import { UploadAvatarComponent } from './upload-avatar/upload-avatar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    UtilsModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    MyProfileRoutingModule,
    NgxDropzoneModule,
    SvgModule,
    NgbModule
  ],
  declarations: [
    MyProfileComponent,
      ChangePasswordComponent,
      ChangePersonalDataComponent,
      OverviewProfileComponent,
      UploadAvatarComponent
  ],
  providers: [
    TranslatePipe,
    Location
  ]
})
export class MyProfileModule { }
