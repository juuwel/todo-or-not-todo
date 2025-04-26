import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { AppConstants } from '../appConstants';
import { AuthResponse, ProblemDetails } from '../datamodel/api-response.types';
import { AuthStore } from '../stores/auth.store';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly baseUrl = environment.userMsBaseUrl;
  private readonly authBaseUrl = this.baseUrl + "/auth";
  private readonly featureFlagBaseUrl = this.baseUrl + "/featureFlag/isEnabled";

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private authStore: AuthStore,
  ) {
    // Check if the user is already logged in when the service is initialized
    const token = localStorage.getItem('token');
    this.authStore.isLoggedIn = !!token;
  }

  public authenticationCallback(token: string) {
    localStorage.setItem('token', token);
    this.authStore.isLoggedIn = true;
    this.router.navigate([AppConstants.Routes.HOME]);
  }

  public async logIn(username: string, password: string): Promise<boolean> {
    return new Promise((resolve) => {
      this.httpClient.post<AuthResponse>(`${this.authBaseUrl}/login`, {
        Email: username,
        Password: password,
      }).pipe(
        tap(async (res: AuthResponse) => {
          this.authenticationCallback(res.token);
          resolve(true); // Resolve true on success
        })
      ).subscribe({
        error: (err) => {
          console.log(err);
          resolve(false); // Resolve false on error
        }
      });
    });
  }

  public logOut() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
    this.authStore.isLoggedIn = false;
  }

  public getToken(): string | null {
    return localStorage.getItem('token');
  }

  public async register(username: string, password: string): Promise<ProblemDetails | null> {
    return new Promise((resolve) => {
      this.httpClient.post<AuthResponse>(`${this.authBaseUrl}/register`, {
        Email: username,
        Password: password,
      }).pipe(
        tap((res: AuthResponse) => {
          this.authenticationCallback(res.token);
          resolve(null);
        })
      ).subscribe({
        error: (err) => {
          console.log(err);
          const problemDetails = new ProblemDetails(
            err.error.detail,
            err.error.status,
            err.error.title,
            err.error.error
          );
          resolve(problemDetails);
        }
      });
    });
  }

  public checkFeatureFlag(featureFlag: string) {
    return this.httpClient.get<boolean>(`${this.featureFlagBaseUrl}/${featureFlag}`, {});
  }
}
