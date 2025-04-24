import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavbarComponent} from '../components/navbar/navbar.component';
import {MessageBannerComponent} from '../components/message-banner/message-banner.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, MessageBannerComponent],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'ToDoFrontend';
}
