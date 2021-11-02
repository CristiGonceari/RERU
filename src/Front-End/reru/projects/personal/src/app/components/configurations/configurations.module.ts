import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurationsComponent } from './configurations.component';
import { ConfigureVacationComponent } from './configure-vacation/configure-vacation.component';
import { ConfigureHolidayComponent } from './configure-holiday/configure-holiday.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ConfigurationsRoutingModule } from './configurations-routing.module';
import { UtilsModule } from '../../utils/utils.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@erp/shared';
import { HolidayDropdownDetailsComponent } from './holiday-dropdown-details/holiday-dropdown-details.component';

@NgModule({
    declarations: [
      ConfigurationsComponent, 
      ConfigureVacationComponent, 
      ConfigureHolidayComponent, HolidayDropdownDetailsComponent
    ],
    imports: [
      CommonModule,
      RouterModule,
      HttpClientModule,
      FormsModule,
      TranslateModule,
      ReactiveFormsModule,
      ConfigurationsRoutingModule,
      UtilsModule,
      NgbModule,
      SharedModule
    ]
  })
  export class ConfigurationsModule { }
  