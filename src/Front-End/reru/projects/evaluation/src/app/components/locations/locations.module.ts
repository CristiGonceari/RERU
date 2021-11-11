import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LocationsRoutingModule } from './locations-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@erp/shared';
import { TranslateModule } from '@ngx-translate/core';
import { UtilsModule } from '../../utils/utils.module';
import { AddEditLocationComponent } from '../locations/add-edit-location/add-edit-location.component';
import { LocationListComponent } from './location-list/location-list.component';
import { LocationListTableComponent } from './location-list/location-list-table/location-list-table.component';
import { LocationNameComponent } from './location-list/location-name/location-name.component';
import { SearchLocationComponent } from './location-list/search-location/search-location.component';
import { LocationDetailsComponent } from './location-details/location-details.component';
import { LocationOverviewComponent } from './location-details/location-overview/location-overview.component';
import { RegisteredComputersComponent } from './location-details/registered-computers/registered-computers.component';
import { ResponsiblePersonComponent } from './location-details/responsible-person/responsible-person.component';
import { ComputersTableComponent } from './location-details/registered-computers/computers-table/computers-table.component';
import { DetachComputerComponent } from './location-details/registered-computers/detach-computer/detach-computer.component';
import { DetachPersonComponent } from './location-details/responsible-person/detach-person/detach-person.component';
import { PersonTableListComponent } from './location-details/responsible-person/person-table-list/person-table-list.component';
import { AttachPersonComponent } from './attach-person/attach-person.component';
import { SearchPersonComponent } from './attach-person/search-person/search-person.component';


@NgModule({
  declarations: [
    AddEditLocationComponent,
    LocationListComponent,
    LocationListTableComponent,
    LocationNameComponent,
    SearchLocationComponent,
    LocationDetailsComponent,
    LocationOverviewComponent,
    RegisteredComputersComponent,
    ResponsiblePersonComponent,
    ComputersTableComponent,
    DetachComputerComponent,
    DetachPersonComponent,
    PersonTableListComponent,
    AttachPersonComponent,
    SearchPersonComponent, 
    ],

  imports: [
    CommonModule,
    LocationsRoutingModule,
    SharedModule,
    UtilsModule,
    HttpClientModule,
    RouterModule,
    TranslateModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ]
})
export class LocationsModule { }
