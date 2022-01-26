import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NomenclatureComponent } from './nomenclature.component';
import { ListComponent } from './list/list.component';
import { AddNomenclatureComponent } from './add-nomenclature/add-nomenclature.component';
import { DetailsNomenclatureComponent } from './details-nomenclature/details-nomenclature.component';
import { EditNomenclatureComponent } from './edit-nomenclature/edit-nomenclature.component';

const routes: Routes = [
  {
    path: '',
    component: NomenclatureComponent,
    children: [
      { path: '', component: ListComponent, pathMatch: 'full' },
      { path: 'new', component: AddNomenclatureComponent },
      { path: 'edit/:id', component: EditNomenclatureComponent },
      { path: ':id', component: DetailsNomenclatureComponent },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class NomenclatureRoutingModule { }
