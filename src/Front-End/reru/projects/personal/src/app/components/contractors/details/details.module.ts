import { NgModule } from '@angular/core';
import { CommonModule, Location } from '@angular/common';

import { DetailsRoutingModule } from './details-routing.module';
import { GeneralComponent } from './general/general.component';
import { DetailsComponent } from './details.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { UtilsModule } from '../../../utils/utils.module';
import { SharedModule, SvgModule } from '@erp/shared';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PositionTableComponent } from './position-table/position-table.component';
import { PositionDropdownDetailsComponent } from './position-dropdown-details/position-dropdown-details.component';
import { PositionDropdownTableComponent } from './position-dropdown-table/position-dropdown-table.component';
import { DocumentsTableComponent } from './documents-table/documents-table.component';
import { BulletinDetailsComponent } from './bulletin-details/bulletin-details.component';
import { StudyDetailsComponent } from './study-details/study-details.component';
import { CimComponent } from './cim/cim.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { RanksComponent } from './ranks/ranks.component';
import { RankCardComponent } from './rank-card/rank-card.component';
import { FamilyComponent } from './family/family.component';
import { FamilyDropdownComponent } from './family-dropdown/family-dropdown.component';
import { VacantionsComponent } from './vacantions/vacantions.component';
import { VacantionCardComponent } from './vacantion-card/vacantion-card.component';
import { DismissalRequestComponent } from './dismissal-request/dismissal-request.component';
import { DismissalRequestCardComponent } from './dismissal-request-card/dismissal-request-card.component';
import { DataService } from './data.service';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { AutobiographyComponent } from './autobiography/autobiography.component';


@NgModule({
  declarations: [
    GeneralComponent,
    DetailsComponent,
    PositionTableComponent,
    PositionDropdownDetailsComponent,
    PositionDropdownTableComponent,
    DocumentsTableComponent,
    BulletinDetailsComponent,
    StudyDetailsComponent,
    CimComponent,
    PermissionsComponent,
    RanksComponent,
    RankCardComponent,
    FamilyComponent,
    FamilyDropdownComponent,
    VacantionsComponent,
    VacantionCardComponent,
    DismissalRequestComponent,
    DismissalRequestCardComponent,
    AutobiographyComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    TranslateModule,
    ReactiveFormsModule,
    DetailsRoutingModule,
    UtilsModule,
    SharedModule,
    NgbModule,
    SvgModule,
    CKEditorModule
  ],
  providers: [
    TranslatePipe,
    Location,
    DataService
  ]
})
export class DetailsModule { }
