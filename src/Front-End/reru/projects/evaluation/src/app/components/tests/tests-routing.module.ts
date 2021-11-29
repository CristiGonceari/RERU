import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from '../tests/test-list/test-list.component';
import { AddTestComponent } from './add-test/add-test.component';
import { TestVerificationProcessComponent } from './test-verification-process/test-verification-process.component';

const routes: Routes = [
  { path: '', component: TestListComponent },
  { path: 'add-test', component: AddTestComponent },
  { path: 'verify-test/:id', component: TestVerificationProcessComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestsRoutingModule { }
