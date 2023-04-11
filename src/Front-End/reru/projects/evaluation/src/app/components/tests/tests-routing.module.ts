import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from '../tests/test-list/test-list.component';
import { TestVerificationProcessComponent } from './test-verification-process/test-verification-process.component';
import { AddTestListComponent } from './add-test-list/add-test-list.component';


const routes: Routes = [
  { path: '', component: TestListComponent },
  { path: 'signed/:id', component: TestListComponent },
  { path: 'add-test', component: AddTestListComponent },
  { path: 'verify-test/:id', component: TestVerificationProcessComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestsRoutingModule { }
