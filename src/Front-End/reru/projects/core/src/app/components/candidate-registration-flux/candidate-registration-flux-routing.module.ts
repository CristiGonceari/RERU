import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CandidateRegistrationFluxComponent } from './candidate-registration-flux.component';
import { AuthenticationGuard } from '@erp/shared';
import { GeneralDataFormComponent } from './general-data-form/general-data-form.component';
import { BulletinComponent } from './bulletin/bulletin.component';
import { StudyComponent } from './study/study.component';
import { MaterialStatusComponent } from './material-status/material-status.component';
import { MilitaryObligationComponent } from './military-obligation/military-obligation.component';
import { AutobiographyComponent } from './autobiography/autobiography.component';
import { DeclarationComponent } from './declaration/declaration.component';


const routes: Routes = [
  { 
    path: ':id/step',
    component: CandidateRegistrationFluxComponent, 
    canActivate: [AuthenticationGuard],
    children: [
        { path: '', redirectTo: '1', pathMatch: 'full' },
        { path: '1', component: GeneralDataFormComponent },
        { path: '2', component: BulletinComponent },
        { path: '3', component: StudyComponent },
        { path: '4', component: MaterialStatusComponent },
        { path: '5', component: MilitaryObligationComponent },
        { path: '6', component: AutobiographyComponent },
        { path: '7', component: DeclarationComponent },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class CandidateRegistrationFluxRoutingModule { }