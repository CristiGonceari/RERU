import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PermissionRouteGuard } from '@erp/shared';
import { AddEditLocationComponent } from './add-edit-location/add-edit-location.component';
import { AttachPersonComponent } from './attach-person/attach-person.component';
import { LocationDetailsComponent } from './location-details/location-details.component';
import { LocationOverviewComponent } from './location-details/location-overview/location-overview.component';
import { DetachComputerComponent } from './location-details/registered-computers/detach-computer/detach-computer.component';
import { RegisteredComputersComponent } from './location-details/registered-computers/registered-computers.component';
import { DetachPersonComponent } from './location-details/responsible-person/detach-person/detach-person.component';
import { ResponsiblePersonComponent } from './location-details/responsible-person/responsible-person.component';
import { LocationListComponent } from './location-list/location-list.component';


const routes: Routes = [
  { path: '', component: LocationListComponent },
  {
    path: 'add-location', 
    component: AddEditLocationComponent,
    data: { permission: 'P03011103' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'edit-location/:id', 
    component: AddEditLocationComponent,
    data: { permission: 'P03011104' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'location/:id',
    component: LocationDetailsComponent,
    data: { permission: 'P03011102' },
    // canActivate: [PermissionRouteGuard],
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: LocationOverviewComponent },
      { 
        path: 'persons', 
        component: ResponsiblePersonComponent,
        data: { permission: 'P03011107' },
        // canActivate: [PermissionRouteGuard]
      },
      { 
        path: 'clients', 
        component: RegisteredComputersComponent,
        data: { permission: 'P03011108' },
        // canActivate: [PermissionRouteGuard]
      }
    ]
  },
  {
    path: 'location/:id/remove-person/:id2', 
    component: DetachPersonComponent,
    data: { permission: 'P03011111' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'attach-person/:id', 
    component: AttachPersonComponent,
    data: { permission: 'P03011106' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'location/:id/detach-computer/:id2', 
    component: DetachComputerComponent,
    data: { permission: 'P03011110' },
    // canActivate: [PermissionRouteGuard],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LocationsRoutingModule { }
