import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEditLocationComponent } from './add-edit-location/add-edit-location.component';
import { AttachPersonComponent } from './location-details/responsible-person/attach-person/attach-person.component';
import { LocationDetailsComponent } from './location-details/location-details.component';
import { LocationOverviewComponent } from './location-details/location-overview/location-overview.component';
import { RegisteredComputersComponent } from './location-details/registered-computers/registered-computers.component';
import { ResponsiblePersonComponent } from './location-details/responsible-person/responsible-person.component';
import { LocationListComponent } from './location-list/location-list.component';


const routes: Routes = [
  { path: '', component: LocationListComponent },
  { path: 'add-location', component: AddEditLocationComponent },
  { path: 'edit-location/:id', component: AddEditLocationComponent },
  { 
    path: 'location/:id', 
    component: LocationDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: LocationOverviewComponent },
      { path: 'persons', component: ResponsiblePersonComponent },
      { path: 'clients', component: RegisteredComputersComponent }
    ]
  },
  { path: 'attach-person/:id', component: AttachPersonComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LocationsRoutingModule { }
