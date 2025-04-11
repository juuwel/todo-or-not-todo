import { Routes } from '@angular/router';
import {HomePageComponent} from '../pages/home-page/home-page.component';
import {LoginPageComponent} from '../pages/login-page/login-page.component';

export const routes: Routes = [
  { path: 'home', component: HomePageComponent, pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent, pathMatch: 'full' },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
