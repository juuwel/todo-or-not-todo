import {Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {Router} from '@angular/router';
import {resolve} from '@angular/compiler-cli';

interface AuthResponse {
  userId: string;
  email: string;
  token: string;
}

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

  public async logIn(username: string, password: string): Promise<boolean> {
    return new Promise((resolve) => {
      this.httpClient.post<AuthResponse>(`${this.baseUrl}/login`, {
        Email: username,
        Password: password,
      }).pipe(
        tap((res: AuthResponse) => {
          localStorage.setItem('token', res.token);
          this.router.navigate(['/']);
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

  public async register(username: string, password: string): Promise<boolean> {
    return new Promise((resolve) => {
      this.httpClient.post(`${this.baseUrl}/register`, {
        Email: username,
        Password: password,
      }).pipe(
        tap(() => {
          resolve(true);
        })
      ).subscribe({
        error: (err) => {
          console.log(err);
          resolve(false);
        }
      });
    });
  }
}
