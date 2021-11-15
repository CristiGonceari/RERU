import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionRouteGuard } from '@erp/shared';
import { AddEditEventsComponent } from './add-edit-events/add-edit-events.component';
import { AttachedUsersListComponent } from './events-details/attached-users-list/attached-users-list.component';
import { EvaluatorsComponent } from './events-details/evaluators/evaluators.component';
import { EventOverviewComponent } from './events-details/event-overview/event-overview.component';
import { AttachComponent } from './events-details/events-details-options/attach/attach.component';
import { EventsDetailsComponent } from './events-details/events-details.component';
import { LocationsListComponent } from './events-details/locations-list/locations-list.component';
import { ResponsiblePersonsComponent } from './events-details/responsible-persons/responsible-persons.component';
import { TestTypesListComponent } from './events-details/test-types-list/test-types-list.component';
import { EventsListComponent } from './events-list/events-list.component';



const routes: Routes = [
  { path: '', component: EventsListComponent },
  { 
    path: 'add-event', 
    component: AddEditEventsComponent,
   
  },
  { 
    path: 'edit-event/:id', 
    component: AddEditEventsComponent,

   },
  {
    path: 'event/:id',
    component: EventsDetailsComponent,
    
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: EventOverviewComponent },
      { 
        path: 'persons', 
        component: ResponsiblePersonsComponent,
        
      },
      { 
        path: 'locations', 
        component: LocationsListComponent,
       
      },
      { 
        path: 'test-types', 
        component: TestTypesListComponent,
      
      },
      { 
        path: 'users', 
        component: AttachedUsersListComponent,
       
      },
      {
        path: 'evaluators',
        component: EvaluatorsComponent,
  
      }
    ]
  },
  {
    path: 'attach-person/:id', 
    component: AttachComponent,
  
  },
  {
    path: 'attach-location/:id', 
    component: AttachComponent,
  },
  {
    path: 'attach-user/:id', 
    component: AttachComponent,
 
  },
  {
    path: 'attach-test-type/:id', 
    component: AttachComponent,
  
  },
  {
    path: 'attach-evaluator/:id', 
    component: AttachComponent,
   
  },
]
 
@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class EventsRoutingModule { }
