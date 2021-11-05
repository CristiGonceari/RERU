import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutsComponent } from './components/layouts/layouts.component';


const routes: Routes = [{
  path: '',
  component: LayoutsComponent,
  canActivate: [],
  children: []
},
  { path: 'categories', loadChildren: () => import('./components/categories/categories.module').then(m => m.CategoriesModule) },
  { path: 'faq', loadChildren: () => import('./components/faq/faq.module').then(m => m.FaqModule) },
  { path: 'polls', loadChildren: () => import('./components/polls/polls.module').then(m => m.PollsModule) },
  { path: 'questions', loadChildren: () => import('./components/questions/questions.module').then(m => m.QuestionsModule) },
  { path: 'locations', loadChildren: () => import('./components/locations/locations.module').then(m => m.LocationsModule) },
  { path: 'dashboard', loadChildren: () => import('./components/dashboard/dashboard.module').then(m => m.DashboardModule) },
  { path: 'statistics', loadChildren: () => import('./components/statistics/statistics.module').then(m => m.StatisticsModule) },
  { path: 'test-types', loadChildren: () => import('./components/test-types/test-types.module').then(m => m.TestTypesModule) },
  { path: 'plans', loadChildren: () => import('./components/plans/plans.module').then(m => m.PlansModule) },
  { path: 'tests', loadChildren: () => import('./components/tests/tests.module').then(m => m.TestsModule) },
  { path: 'events', loadChildren: () => import('./components/events/events.module').then(m => m.EventsModule) }]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
