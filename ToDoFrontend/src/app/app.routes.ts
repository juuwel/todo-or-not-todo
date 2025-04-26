import { Routes } from '@angular/router';
import { AppConstants } from './appConstants';
import { authGuard } from './guards/auth-gard.guard';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';

export const routes: Routes = [
  { path: AppConstants.Routes.HOME, component: HomePageComponent, pathMatch: 'full', canActivate: [authGuard] },
  { path: AppConstants.Routes.LOGIN, component: LoginPageComponent, pathMatch: 'full', canActivate: [authGuard] },
  { path: '', redirectTo: AppConstants.Routes.HOME, pathMatch: 'full' },
  { path: '**', redirectTo: AppConstants.Routes.HOME, pathMatch: 'full' },
];
