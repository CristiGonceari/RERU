import { NgModule } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatStepperModule } from '@angular/material/stepper';

const materialModules = [
  MatDatepickerModule,
  MatStepperModule
];

@NgModule({
  imports: materialModules,
  exports: materialModules
})
export class MaterialModule { }
