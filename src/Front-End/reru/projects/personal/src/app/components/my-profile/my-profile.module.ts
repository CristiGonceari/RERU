import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UtilsModule } from '../../utils/utils.module';
import { SharedModule } from '@erp/shared';
import { MyProfileRoutingModule } from './my-profile-routing.module';
import { MyProfileComponent } from './my-profile.component';
import { ProfileGeneralComponent } from './profile-general/profile-general.component';
import { TranslatePipe, TranslateModule } from '@ngx-translate/core';
import { LanguagesListComponent } from './languages-list/languages-list.component';
import { VacationRequestsComponent } from './vacation-requests/vacation-requests.component';
import { LanguagesDropdownDetailsComponent } from './languages-dropdown-details/languages-dropdown-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileNotFoundComponent } from './profile-not-found/profile-not-found.component';
import { ProfileBulletinComponent } from './profile-bulletin/profile-bulletin.component';
import { ProfileStudiesComponent } from './profile-studies/profile-studies.component';
import { ProfileCimComponent } from './profile-cim/profile-cim.component';
import { ProfilePositionsComponent } from './profile-positions/profile-positions.component';
import { ProfileDocumentsComponent } from './profile-documents/profile-documents.component';
import { MyVacationsComponent } from './my-vacations/my-vacations.component';
import { DocumentsNotFoundComponent } from './documents-not-found/documents-not-found.component';
import { SubordinateVacationsComponent } from './subordinate-vacations/subordinate-vacations.component';
import { MyRequestsComponent } from './my-requests/my-requests.component';
import { MyVacationsCardComponent } from './my-vacations-card/my-vacations-card.component';
import { SubordinateVacationsCardComponent } from './subordinate-vacations-card/subordinate-vacations-card.component';
import { MyRequestsCardComponent } from './my-requests-card/my-requests-card.component';
import { SubordinateRequestsComponent } from './subordinate-requests/subordinate-requests.component';
import { SubordinateRequestsCardComponent } from './subordinate-requests-card/subordinate-requests-card.component';
import { ProfileRanksComponent } from './profile-ranks/profile-ranks.component';
import { ProfileRanksCardComponent } from './profile-ranks-card/profile-ranks-card.component';
import { ProfileFamilyComponent } from './profile-family/profile-family.component';
import { TimeSheetTableComponent } from './time-sheet-table/time-sheet-table.component';

@NgModule({
  declarations: [
    MyProfileComponent,
    ProfileGeneralComponent,
    LanguagesListComponent,
    VacationRequestsComponent,
    LanguagesDropdownDetailsComponent,
    ProfileNotFoundComponent,
    ProfileBulletinComponent,
    ProfileStudiesComponent,
    ProfileCimComponent,
    ProfilePositionsComponent,
    ProfileDocumentsComponent,
    MyVacationsComponent,
    DocumentsNotFoundComponent,
    SubordinateVacationsComponent,
    MyRequestsComponent,
    MyVacationsCardComponent,
    SubordinateVacationsCardComponent,
    MyRequestsCardComponent,
    SubordinateRequestsComponent,
    SubordinateRequestsCardComponent,
    ProfileRanksComponent,
    ProfileRanksCardComponent,
    ProfileFamilyComponent,
    TimeSheetTableComponent
  ],
  imports: [
    CommonModule,
    MyProfileRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    UtilsModule,
    SharedModule,
    NgbModule
  ],
  providers:[ TranslatePipe]
})
export class MyProfileModule { }
