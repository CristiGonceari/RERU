import { UsersComponent } from './users.component';
import { EditComponent } from './edit/edit.component';
import { UserListComponent } from './user-list/user-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddComponent } from './add/add.component';
import { SetPasswordComponent } from './set-password/set-password.component';
import { RemoveComponent } from './remove/remove.component';

const routes: Routes = [
    {
        path: '',
        component: UsersComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },
            {
                path: 'list',
                component: UserListComponent,
            },
            {
                path: 'new',
                component: AddComponent,
            },
            {
                path: 'edit/:id',
                component: EditComponent,
            },
            {
                path: 'set/:id',
                component: SetPasswordComponent,
            },
            {
                path: 'remove/:id',
                component: RemoveComponent,
            },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    exports: [RouterModule]
})
export class UsersRoutingModule { }
