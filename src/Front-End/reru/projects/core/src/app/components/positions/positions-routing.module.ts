import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from '@erp/shared';
import { AddEditPositionComponent } from './add-edit-position/add-edit-position.component';
import { PositionsComponent } from './positions.component';

const routes: Routes = [
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
    }
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class PositionsRoutingModule {}