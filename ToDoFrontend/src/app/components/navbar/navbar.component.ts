import {Component} from '@angular/core';
import {RouterLink} from '@angular/router';
import { AppConstants } from '../../appConstants';
import {AuthStore} from '../../stores/auth.store';
import {AsyncPipe} from '@angular/common';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'navbar',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  protected readonly LogInRoute = AppConstants.Routes.LOGIN;
  protected readonly HomeRoute = AppConstants.Routes.HOME;

  constructor(
    public authStore: AuthStore,
    private authService: AuthService
  ) {

  }

  public logOut(): void {
    this.authService.logOut();
  }
}
