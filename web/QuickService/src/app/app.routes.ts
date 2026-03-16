import { Routes } from '@angular/router';
import { AuthPageComponent } from './features/auth/pages/auth-pages/auth-pages.component';
import { AuthGuard } from './core/Guards/auth-guard';
import path from 'path';
import { ExplorePageComponent } from './features/home/pages/explorer-page/explorer-page.component';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./features/home/home.routes').then(m => m.HOME_ROUTES)
  },
  {
    path: 'login',
    component: AuthPageComponent,
    data: { type: 'login' },
    // canActivate: [AuthGuard]
  },
  {
    path: 'register',
    component: AuthPageComponent,
    data: { type: 'register' },
    // canActivate: [AuthGuard]
  },
  {
    path: 'explorar',
    component: ExplorePageComponent,
    data: { type: 'explorar' }
  }

];