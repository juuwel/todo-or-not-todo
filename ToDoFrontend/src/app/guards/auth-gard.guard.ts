import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AppConstants } from '../appConstants';
import { AuthStore } from '../stores/auth.store';

export const authGuard: CanActivateFn = (route, state) => {
  const authStore = inject(AuthStore);
  const router = inject(Router);
  const isLoggedIn = authStore.isLoggedIn;
  const isLoginRoute = state.url === `/${AppConstants.Routes.LOGIN}`;

  if (isLoggedIn && isLoginRoute) {
    // If logged in and trying to access the login page, redirect to home
    router.navigate([AppConstants.Routes.HOME]);
    return false;
  }

  if (!isLoggedIn && !isLoginRoute) {
    // If not logged in and trying to access a protected route, redirect to login
    router.navigate([AppConstants.Routes.LOGIN]);
    return false;
  }

  return true; // Allow access if conditions are met
};
