import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionRouteGuard } from 'dist/erp-shared/public-api';
import { EventsListComponent } from '../events/events-list/events-list.component';
// import { PermissionRouteGuard } from '@erp/shared';
import { AddComponent } from './add/add.component';
import { DeleteComponent } from './delete/delete.component';
import { EditComponent } from './edit/edit.component';
import { AttachComponent } from './plan-details/events/attach/attach.component';
import { DetachComponent } from './plan-details/events/detach/detach.component';
import { PlanDetailsComponent } from './plan-details/plan-details.component';
import { PlanOverviewComponent } from './plan-details/plan-overview/plan-overview.component';
import { AttachPersonsComponent } from './plan-details/responsable-persons/attach-persons/attach-persons.component';
import { DetachPersonsComponent } from './plan-details/responsable-persons/detach-persons/detach-persons.component';
import { ResponsablePersonsComponent } from './plan-details/responsable-persons/responsable-persons.component';
import { EventsComponent } from './plan-details/events/events.component';

import { PlansComponent } from './plans.component';

const routes: Routes = [
  {path: '', component: PlansComponent},
  {
    path: 'add', 
    component: AddComponent,
    // data: { permission: 'P03011503' },
    // canActivate: [PermissionRouteGuard]
  },
  {
    path: 'edit/:id', 
    component: EditComponent,
    // data: { permission: 'P03011504' },
    // canActivate: [PermissionRouteGuard]
  },
  {
    path: 'plan/:id',
    component: PlanDetailsComponent,
    // data: { permission: 'P03011502' },
    // canActivate: [PermissionRouteGuard],
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: PlanOverviewComponent },
      {
        path: 'events', 
        component: EventsComponent,
        // data: { permission: 'P03011509' },
        // canActivate: [PermissionRouteGuard]
      },
      {
        path: 'persons', 
        component: ResponsablePersonsComponent,
        // data: { permission: 'P03011506' },
        // canActivate: [PermissionRouteGuard]
      }
    ]
  },
  {
    path: 'remove/:id', 
    component: DeleteComponent,
    // data: { permission: 'P03011505' },
    // canActivate: [PermissionRouteGuard]
  },
  {
    path: 'attach-event/:id', 
    component: AttachComponent,
    // data: { permission: 'P03011510' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'plan/:id/detach-event/:id2', 
    component: DetachComponent,
    // data: { permission: 'P03011511' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'attach-person/:id', 
    component: AttachPersonsComponent,
    // data: { permission: 'P03011507' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'plan/:id/detach-person/:id2', 
    component: DetachPersonsComponent,
    // data: { permission: 'P03011508' },
    // canActivate: [PermissionRouteGuard],
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlansRoutingModule { }
