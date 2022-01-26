import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
    { path: '', component: GeneralComponent, pathMatch: 'full' },
    { path: 'positions', component: PositionTableComponent },
    { path: 'bulletin', component: BulletinDetailsComponent },
    { path: 'studies', component: StudyDetailsComponent },
    { path: 'ranks', component: RanksComponent },
    { path: 'family', component: FamilyComponent },
    { path: 'cim', component: CimComponent },
    { path: 'documents', component: DocumentsTableComponent },
    { path: 'permissions', component: PermissionsComponent },
    { path: 'vacantions', component: VacantionsComponent },
    { path: 'dismissal', component: DismissalRequestComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DetailsRoutingModule { }
