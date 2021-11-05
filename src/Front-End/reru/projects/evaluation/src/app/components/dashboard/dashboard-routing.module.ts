import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { PermissionRouteGuard } from '@erp/shared';
import { PollsTableComponent } from './polls-table/polls-table.component'
const routes: Routes = [
  {
    path: '', component: DashboardComponent,
    children: [
      { path: '', redirectTo: 'my-tests', pathMatch: 'full' },
      { 
        path: 'my-polls', 
        component: PollsTableComponent,
        // data: { permission: 'P03011218' },
				canActivate: [PermissionRouteGuard]
      },
    ]
  },
]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
