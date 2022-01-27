import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileGeneralComponent } from './profile-general/profile-general.component';
import { MyProfileComponent } from './my-profile.component';
import { ProfileBulletinComponent } from './profile-bulletin/profile-bulletin.component';
import { ProfileStudiesComponent } from './profile-studies/profile-studies.component';
import { ProfileCimComponent } from './profile-cim/profile-cim.component';
import { ProfilePositionsComponent } from './profile-positions/profile-positions.component';
import { ProfileDocumentsComponent } from './profile-documents/profile-documents.component';
import { MyVacationsComponent } from './my-vacations/my-vacations.component';
import { SubordinateVacationsComponent } from './subordinate-vacations/subordinate-vacations.component';
import { MyRequestsComponent } from './my-requests/my-requests.component';
import { SubordinateRequestsComponent } from './subordinate-requests/subordinate-requests.component';
import { ProfileRanksComponent } from './profile-ranks/profile-ranks.component';
import { ProfileFamilyComponent } from './profile-family/profile-family.component';
import { TimeSheetTableComponent } from './time-sheet-table/time-sheet-table.component';

const routes: Routes = [{
  component:MyProfileComponent,
  path: '',
  children:[
    { path: '', component: ProfileGeneralComponent, pathMatch: 'full' },
    { path: 'bulletin', component: ProfileBulletinComponent },
    { path: 'studies', component: ProfileStudiesComponent },
    { path: 'ranks', component: ProfileRanksComponent },
    { path: 'family', component: ProfileFamilyComponent },
    { path: 'cim', component: ProfileCimComponent },
    { path: 'positions', component: ProfilePositionsComponent },
    { path: 'documents', component: ProfileDocumentsComponent },
    { path: 'my-vacations', component: MyVacationsComponent },
    { path: 'subordinate-vacations', component: SubordinateVacationsComponent },
    { path: 'my-requests', component: MyRequestsComponent },
    { path: 'subordinate-requests', component: SubordinateRequestsComponent },
    { path: 'time-sheet-table', component: TimeSheetTableComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyProfileRoutingModule { }
