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
import { SearchLocationComponent } from './location-list/search-location/search-location.component';
import { LocationDetailsComponent } from './location-details/location-details.component';
import { LocationOverviewComponent } from './location-details/location-overview/location-overview.component';
import { ResponsiblePersonComponent } from './location-details/responsible-person/responsible-person.component';
import { PersonTableListComponent } from './location-details/responsible-person/person-table-list/person-table-list.component';
import { MaterialModule } from './../../material.module';

@NgModule({
  declarations: [
    AddEditLocationComponent,
    LocationListComponent,
    LocationListTableComponent,
    SearchLocationComponent,
    LocationDetailsComponent,
    LocationOverviewComponent,
    ResponsiblePersonComponent,
    PersonTableListComponent,
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
    MaterialModule
  ]
})
export class LocationsModule { }
