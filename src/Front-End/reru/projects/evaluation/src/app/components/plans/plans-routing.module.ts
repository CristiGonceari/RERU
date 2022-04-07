import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AttachComponent } from './plan-details/events/attach/attach.component';
import { PlanDetailsComponent } from './plan-details/plan-details.component';
import { PlanOverviewComponent } from './plan-details/plan-overview/plan-overview.component';
import { ResponsablePersonsComponent } from './plan-details/responsable-persons/responsable-persons.component';
import { EventsComponent } from './plan-details/events/events.component';
import { PlansComponent } from './plans.component';
import { AddEditPlansComponent } from './add-edit-plans/add-edit-plans.component';

const routes: Routes = [
  {path: '', component: PlansComponent},
  { path: 'edit/:id', component: AddEditPlansComponent },
  { path: 'add', component: AddEditPlansComponent },
  { 
    path: 'plan/:id',
    component: PlanDetailsComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: PlanOverviewComponent },
      { path: 'events', component: EventsComponent },
      { path: 'persons', component: ResponsablePersonsComponent }
    ]
  },
  { path: 'attach-event/:id', component: AttachComponent },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlansRoutingModule { }
