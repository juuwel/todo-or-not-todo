import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, tap} from 'rxjs';
import {Router} from '@angular/router';
import {AuthResponse, ProblemDetails} from '../datamodel/api-respones';
import {AppConstants} from '../appConstants';
import {AuthStore} from './auth.store';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly baseUrl = 'http://localhost:1001';
  private readonly authBaseUrl = this.baseUrl + "/auth";
  private readonly featureFlagBaseUrl = this.baseUrl + "/featureFlag/isEnabled";

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private authStore: AuthStore,
  ) {
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
