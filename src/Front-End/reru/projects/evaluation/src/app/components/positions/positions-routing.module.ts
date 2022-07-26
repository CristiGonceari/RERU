import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from '@erp/shared';
import { AddEditPositionComponent } from './add-edit-position/add-edit-position.component';
import { PositionsDiagramComponent } from './positions-diagram/positions-diagram.component';
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
        path: 'diagram/:id',
        component: PositionsDiagramComponent,
    }
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class PositionsRoutingModule {}