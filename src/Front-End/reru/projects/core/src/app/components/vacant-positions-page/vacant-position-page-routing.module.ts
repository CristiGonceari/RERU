import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VacantPositionsPageComponent } from './vacant-positions-page.component';

const routes: Routes = [
	{
		path: '',
		component: VacantPositionsPageComponent
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class VacantPositionPageRoutingModule { }
