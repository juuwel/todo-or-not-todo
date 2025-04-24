import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {uuid} from '../datamodel/task.types';

@Injectable({
  providedIn: 'root'
})
export class AuthStore  {
  private readonly isRegisteringEnabledSubject = new BehaviorSubject<boolean>(false);
  private readonly isLoggedInSubject = new BehaviorSubject<boolean>(false);
  private readonly userIdSubject = new BehaviorSubject<uuid | null>(null);

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

  public get userId$(): Observable<uuid | null> {
    return this.userIdSubject.asObservable();
  }

  public set userId(value: uuid) {
    this.userIdSubject.next(value);
  }
}
