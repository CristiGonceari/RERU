import { MyProfileComponent } from './my-profile.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChangePersonalDataComponent } from './change-personal-data/change-personal-data.component';
import { AuthenticationGuard } from '@erp/shared';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { OverviewProfileComponent } from './overview-profile/overview-profile.component';
import { UserFilesComponent } from '../users/user-profile/user-files/user-files.component';

const routes: Routes = [
    {
        path: '',
        component: MyProfileComponent,
        canActivate: [AuthenticationGuard],
        children: [
            { path: 'overview', component: OverviewProfileComponent },
            { path: 'change-password', component: ChangePasswordComponent },
            { path: 'change-my-data', component: ChangePersonalDataComponent },
            { path: 'my-documents', component: UserFilesComponent },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    exports: [RouterModule]
})
export class MyProfileRoutingModule { }
