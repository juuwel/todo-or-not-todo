import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthStore  {
  private readonly isRegisteringEnabledSubject = new BehaviorSubject<boolean>(false);
  private readonly isLoggedInSubject = new BehaviorSubject<boolean>(false);

  public get isRegisteringEnabled$(): Observable<boolean> {
    return this.isRegisteringEnabledSubject.asObservable();
  }

  public set isRegisteringEnabled(value: boolean) {
    this.isRegisteringEnabledSubject.next(value);
  }

  public get isLoggedIn$(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  public set isLoggedIn(value: boolean) {
    this.isLoggedInSubject.next(value);
  }
}
