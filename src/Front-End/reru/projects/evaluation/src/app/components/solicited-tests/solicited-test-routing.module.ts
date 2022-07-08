import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApproveSolicitedTestComponent } from './solicited-test-list/approve-solicited-test/approve-solicited-test.component';
import { SolicitedTestListComponent } from './solicited-test-list/solicited-test-list.component';

const routes: Routes = [
  { path: '', component: SolicitedTestListComponent },
  { path: 'review/:id/:positionId', component: ApproveSolicitedTestComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class SolicitedTestRoutingModule { }
