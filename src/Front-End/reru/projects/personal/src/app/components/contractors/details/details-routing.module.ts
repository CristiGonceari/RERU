import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AutobiographyComponent } from './autobiography/autobiography.component';
import { BulletinDetailsComponent } from './bulletin-details/bulletin-details.component';
import { CimComponent } from './cim/cim.component';
import { DetailsComponent } from './details.component';
import { DismissalRequestComponent } from './dismissal-request/dismissal-request.component';
import { DocumentsTableComponent } from './documents-table/documents-table.component';
import { FamilyComponent } from './family/family.component';
import { GeneralComponent } from './general/general.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { PositionTableComponent } from './position-table/position-table.component';
import { RanksComponent } from './ranks/ranks.component';
import { StudyDetailsComponent } from './study-details/study-details.component';
import { VacantionsComponent } from './vacantions/vacantions.component';

const routes: Routes = [{
  component: DetailsComponent,
  path: '',
  children: [
    { path: '', redirectTo: 'general-data/1', pathMatch: 'full' },
    { path: 'general-data/1', component: GeneralComponent, pathMatch: 'full' },
    // { path: 'positions', component: PositionTableComponent },
    { path: 'bulletin/2', component: BulletinDetailsComponent },
    { path: 'studies/3', component: StudyDetailsComponent },
    { path: 'ranks/5', component: RanksComponent },
    { path: 'family/4', component: FamilyComponent },
    // { path: 'cim', component: CimComponent },
    // { path: 'documents', component: DocumentsTableComponent },
    // { path: 'permissions', component: PermissionsComponent },
    // { path: 'vacantions', component: VacantionsComponent },
    // { path: 'dismissal', component: DismissalRequestComponent },
    { path: 'autobiography/6', component: AutobiographyComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DetailsRoutingModule { }
