import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {tap} from 'rxjs';
import {Router} from '@angular/router';
import {AuthResponse, ProblemDetails} from '../datamodel/api-respones';
import {AppConstants} from '../appConstants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly baseUrl = "http://localhost:1001/auth";

  constructor(
    private httpClient: HttpClient,
    private router: Router,
  ) {
  }

  public authenticationCallback(token: string) {
    localStorage.setItem('token', token);
    this.router.navigate([AppConstants.Routes.HOME]);
  }

  public async logIn(username: string, password: string): Promise<boolean> {
    return new Promise((resolve) => {
      this.httpClient.post<AuthResponse>(`${this.baseUrl}/login`, {
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
  }

  public async register(username: string, password: string): Promise<ProblemDetails | null> {
    return new Promise((resolve) => {
      this.httpClient.post<AuthResponse>(`${this.baseUrl}/register`, {
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

  public isUserLoggedIn(): boolean {
    return localStorage.getItem('token') === null;
  }
}
