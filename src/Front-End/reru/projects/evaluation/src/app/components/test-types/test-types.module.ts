import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TestTypesRoutingModule } from './test-types-routing.module';
import { TestTypesComponent } from './test-types.component';


@NgModule({
  declarations: [TestTypesComponent],
  imports: [
    CommonModule,
    TestTypesRoutingModule
  ]
})
export class TestTypesModule { }
