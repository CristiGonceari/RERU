import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TestTypesComponent } from './test-types.component';

const routes: Routes = [{ path: '', component: TestTypesComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestTypesRoutingModule { }
