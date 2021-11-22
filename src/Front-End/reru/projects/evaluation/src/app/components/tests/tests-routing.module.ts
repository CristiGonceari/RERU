import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from '../tests/test-list/test-list.component';
import { AddTestComponent } from './add-test/add-test.component';
import { TestVerificationProcessComponent } from './test-verification-process/test-verification-process.component';
import { FinishPageComponent } from './finish-page/finish-page.component';
import { OnePerPagePerformingTestComponent } from './one-per-page-performing-test/one-per-page-performing-test.component';
import { StartTestPageComponent } from './start-test-page/start-test-page.component';

const routes: Routes = [
  { path: '', component: TestListComponent },
  { path: 'performing-test/:id', component: OnePerPagePerformingTestComponent },
  { path: 'start-test/:id', component: StartTestPageComponent },
  { path: 'add-test', component: AddTestComponent },
	{ path: 'finish-page/:id', component: FinishPageComponent },
  {path: 'start-test/:id', component: StartTestPageComponent},
  { path: 'verify-test/:id', component: TestVerificationProcessComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestsRoutingModule { }
