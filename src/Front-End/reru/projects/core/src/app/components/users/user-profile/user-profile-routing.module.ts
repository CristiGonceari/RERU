import { UserProfileComponent } from './user-profile.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from '../../../utils/guards/auth.guard';
import { UserOverviewComponent } from './user-overview/user-overview.component';
import { ModuleAccessListComponent } from './module-access/module-access-list/module-access-list.component';
import { PermissionRouteGuard, AuthenticationGuard  } from '@erp/shared';
import { UserFilesComponent } from './user-files/user-files.component';

const routes: Routes = [
    {
        path: ':id',
        component: UserProfileComponent,
        canActivate: [AuthenticationGuard],
        children: [
            {
                path: 'overview',
                component: UserOverviewComponent,
            },
            {
                path: 'module-access',
                component: ModuleAccessListComponent,
            },
            {
                path: 'user-files',
                component: UserFilesComponent,
            },
            { path: 'module-access/:id', loadChildren: () => import('./module-access/module-access.module').then(m => m.ModuleAccessModule) },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    exports: [RouterModule]
})
export class UserProfileRoutingModule { }
