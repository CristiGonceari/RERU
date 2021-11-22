import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from '../tests/test-list/test-list.component';
import { AddTestComponent } from './add-test/add-test.component';
import { FinishPageComponent } from './finish-page/finish-page.component';
import { OnePerPagePerformingTestComponent } from './one-per-page-performing-test/one-per-page-performing-test.component';
import { StartTestPageComponent } from './start-test-page/start-test-page.component';

const routes: Routes = [
  { path: '', component: TestListComponent },
  { path: 'performing-test/:id', component: OnePerPagePerformingTestComponent },
  { path: 'start-test/:id', component: StartTestPageComponent },
  // { path: 'verify-test/:id', component: VerifyTestComponent },
  {
    path: 'add-test',
    component: AddTestComponent,
    // data: { permission: 'P03010605' },
    // canActivate: [PermissionRouteGuard],
  },
  // { path: 'remove/:id', component: DeleteTestComponent },
	// { path: 'test-result/:id', component: TestResultComponent },
	{ path: 'finish-page/:id', component: FinishPageComponent },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestsRoutingModule { }
