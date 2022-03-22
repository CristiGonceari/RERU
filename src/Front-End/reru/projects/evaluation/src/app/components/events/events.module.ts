import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsListComponent } from './events-list/events-list.component';
import { EventsListTableComponent } from './events-list/events-list-table/events-list-table.component';
import { EventsRoutingModule } from './events-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UtilsModule } from '../../utils/utils.module';
import { AddEditEventsComponent } from './add-edit-events/add-edit-events.component';
import { EventsDetailsComponent } from './events-details/events-details.component';
import { EventOverviewComponent } from './events-details/event-overview/event-overview.component';
import { OwlDateTimeModule, OwlMomentDateTimeModule } from '@busacca/ng-pick-datetime';
import { LocationsListComponent } from './events-details/locations-list/locations-list.component';
import { TestTemplatesListComponent } from './events-details/test-templates-list/test-templates-list.component';
import { AttachedUsersListComponent } from './events-details/attached-users-list/attached-users-list.component';
import { ResponsiblePersonsComponent } from './events-details/responsible-persons/responsible-persons.component';
import { MaterialModule } from '../../material.module';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { TableComponent } from './events-details/events-details-options/table/table.component'
import { AttachComponent } from './events-details/events-details-options/attach/attach.component'
import { SearchComponent } from './events-details/events-details-options/attach/search/search.component'
import { EvaluatorsComponent } from './events-details/evaluators/evaluators.component'

@NgModule({
  declarations: [
    EventsListComponent,
    EventsListTableComponent,
    AddEditEventsComponent,
    EventsDetailsComponent,
    EventOverviewComponent,
    LocationsListComponent,
    TestTemplatesListComponent,
    AttachedUsersListComponent,
    ResponsiblePersonsComponent,
    TableComponent,
    AttachComponent,
    SearchComponent,
    EvaluatorsComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule, 
    NgbModule,
    TranslateModule,
    SharedModule,
    RouterModule,
    HttpClientModule,
    UtilsModule,
    EventsRoutingModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
    MaterialModule,
    NgxDropzoneModule
  ]
})
export class EventsModule { }
