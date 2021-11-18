import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlansRoutingModule } from './plans-routing.module';
import { PlansComponent } from './plans.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@erp/shared';
import { MaterialModule } from '../../material.module';
import { UtilsModule } from '../../utils/utils.module';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { PlanDetailsComponent } from './plan-details/plan-details.component';
import { PlansListComponent } from './plans-list/plans-list.component';
import { PlanOverviewComponent } from './plan-details/plan-overview/plan-overview.component';
import { ResponsablePersonsComponent } from './plan-details/responsable-persons/responsable-persons.component';
import { AttachPersonsComponent } from './plan-details/responsable-persons/attach-persons/attach-persons.component';
import { ResponsablePersonsTableComponent } from './plan-details/responsable-persons/responsable-persons-table/responsable-persons-table.component';
import { SearchPersonComponent } from './plan-details/responsable-persons/attach-persons/search-person/search-person.component';
import { EventsComponent } from './plan-details/events/events.component';
import { AttachComponent } from './plan-details/events/attach/attach.component';
import { EventsTableComponent } from './plan-details/events/events-table/events-table.component';
import { SearchEventComponent } from './plan-details/events/attach/search-event/search-event.component';


@NgModule({
  declarations: [
    PlansComponent, 
    AddComponent, 
    EditComponent, 
    PlanDetailsComponent, 
    PlansListComponent,
    PlanOverviewComponent,
    ResponsablePersonsComponent,
    AttachPersonsComponent,
    ResponsablePersonsTableComponent,
    SearchPersonComponent,
    EventsComponent,
    AttachComponent,
    EventsTableComponent,
    SearchEventComponent
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
    MaterialModule,
    PlansRoutingModule
  ]
})
export class PlansModule { }
