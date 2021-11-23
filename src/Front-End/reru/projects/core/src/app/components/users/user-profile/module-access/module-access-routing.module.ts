import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
// import { AuthGuard } from '../../../../utils/guards/auth.guard';
import { AddEditModuleAccessComponent } from './add-edit-module-access/add-edit-module-access.component';
import { RemoveModuleAccessComponent } from './remove-module-access/remove-module-access.component';
import { ModuleAccessComponent } from './module-access.component';
import { PermissionRouteGuard, AuthenticationGuard } from '@erp/shared';

const routes: Routes = [
    {
        path: '',
        component: ModuleAccessComponent,
        canActivate: [AuthenticationGuard],
        children: [
            {
                path: 'give',
                component: AddEditModuleAccessComponent,
				data: { permission: 'P00000022' },
				canActivate: [PermissionRouteGuard]
            },
            {
                path: 'update',
                component: AddEditModuleAccessComponent,
				data: { permission: 'P00000024' },
				canActivate: [PermissionRouteGuard]
            },
            {
                path: 'remove',
                component: RemoveModuleAccessComponent,
				data: { permission: 'P00000023' },
				canActivate: [PermissionRouteGuard]
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    exports: [RouterModule]
})
export class ModuleAccessRoutingModule { }
