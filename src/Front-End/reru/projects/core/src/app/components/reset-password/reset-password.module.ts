import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { SharedModule } from '@erp/shared';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ResetPasswordRoutingModule } from './reset-password-routing.module';
import { RouterModule } from '@angular/router';
import { ResetPasswordComponent } from './reset-password.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { CommonModule, DatePipe } from '@angular/common';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@busacca/ng-pick-datetime';

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
      OwlDateTimeModule,
      OwlNativeDateTimeModule,
      ResetPasswordRoutingModule,
      NgxDropzoneModule,
    ],
    providers: [
      TranslatePipe,
      Location,
      DatePipe
    ],
    declarations: [ResetPasswordComponent ],
  })

export class ResetPasswordModule { }

