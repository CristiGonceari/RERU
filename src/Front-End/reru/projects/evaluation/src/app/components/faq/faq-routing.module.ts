import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FaqAddEditComponent } from './faq-add-edit/faq-add-edit.component';
import { FaqListComponent } from './faq-list/faq-list.component';
import { FaqDetailsComponent } from './faq-details/faq-details.component';
import { PermissionRouteGuard } from '@erp/shared';
import { FaqOverviewComponent } from './faq-details/faq-overview/faq-overview.component';

const routes: Routes = [
  { path: '', component: FaqListComponent },
  {
    path: 'faq-details/:id',
    component: FaqDetailsComponent,
    // data: { permission: 'P03010903' },
    // canActivate: [PermissionRouteGuard],
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', component: FaqOverviewComponent }
    ]
  },
  {
    path: 'add-article',
    component: FaqAddEditComponent,
    // data: { permission: 'P03010901' },
    // canActivate: [PermissionRouteGuard],
  },
  {
    path: 'edit-article/:id',
    component: FaqAddEditComponent,
    // data: { permission: 'P03010901' },
    // canActivate: [PermissionRouteGuard],
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class FaqRoutingModule { }
