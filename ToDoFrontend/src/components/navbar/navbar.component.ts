import {Component} from '@angular/core';
import {RouterLink} from '@angular/router';
import { AppConstants } from '../../appConstants';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'navbar',
  imports: [RouterLink],
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  protected readonly LogInRoute = AppConstants.Routes.LOGIN;
  protected readonly HomeRoute = AppConstants.Routes.HOME;

  constructor(public authService: AuthService) {
  }
}
