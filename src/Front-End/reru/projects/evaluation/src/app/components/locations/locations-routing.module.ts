import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEditLocationComponent } from './add-edit-location/add-edit-location.component';
import { LocationDetailsComponent } from './location-details/location-details.component';
import { LocationOverviewComponent } from './location-details/location-overview/location-overview.component';
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
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LocationsRoutingModule { }
