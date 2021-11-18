import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestListComponent } from '../tests/test-list/test-list.component';
import { AddTestComponent } from './add-test/add-test.component';
import { FinishPageComponent } from './finish-page/finish-page.component';

const routes: Routes = [
  { path: '', component: TestListComponent },
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
