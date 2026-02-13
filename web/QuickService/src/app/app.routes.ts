import { Routes } from '@angular/router';
import { AuthPageComponent } from './features/auth/pages/auth-pages/auth-pages.component';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./features/home/home.routes').then(m => m.HOME_ROUTES)
  },
  {
    path: 'login',
    component: AuthPageComponent,
    data: { type: 'login' }
  },
  {
    path: 'register',
    component: AuthPageComponent,
    data: { type: 'register' }
  }
];