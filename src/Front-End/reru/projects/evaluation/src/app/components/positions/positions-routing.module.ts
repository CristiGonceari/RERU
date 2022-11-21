import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from '@erp/shared';
import { AddEditPositionComponent } from './add-edit-position/add-edit-position.component';
import { PositionDetailsComponent } from './position-details/position-details.component';
import { PositionOverviewComponent } from './position-details/position-overview/position-overview.component';
import { DiagramComponent } from './position-details/diagram/diagram.component';
import { PositionsComponent } from './positions.component';

const routes: Routes = [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
	{
		path: 'list',
		component: PositionsComponent,
		canActivate: [AuthenticationGuard]
	},
    {
        path: 'add-position',
        component: AddEditPositionComponent,
    },
    {
        path: 'edit-position/:id',
        component: AddEditPositionComponent,
    },
    { 
        path: 'position/:id',
        component: PositionDetailsComponent,
        children: [
          { path: '', redirectTo: 'overview', pathMatch: 'full' },
          { path: 'overview', component: PositionOverviewComponent },
          { path: 'diagram', component: DiagramComponent },
        ]
      },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class PositionsRoutingModule {}