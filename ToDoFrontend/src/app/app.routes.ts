import { Routes } from '@angular/router';
import {HomePageComponent} from '../pages/home-page/home-page.component';
import {LoginPageComponent} from '../pages/login-page/login-page.component';
import { AppConstants } from '../appConstants';

export const routes: Routes = [
  { path: AppConstants.Routes.HOME, component: HomePageComponent, pathMatch: 'full' },
  { path: AppConstants.Routes.LOGIN, component: LoginPageComponent, pathMatch: 'full' },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
