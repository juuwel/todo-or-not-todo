import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor {
  constructor(private readonly service: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.service.getToken();

    if (token) { // && this.sameOrigin(req)
      return next.handle(req.clone({
        headers: req.headers.set("Authorization", `Bearer ${token}`)
      }));
    }
    return next.handle(req);
  }

  private sameOrigin(req: HttpRequest<any>) {
    const isRelative = !req.url.startsWith("http://") || !req.url.startsWith("https://");
    return req.url.startsWith(location.origin) || isRelative;
  }
}
