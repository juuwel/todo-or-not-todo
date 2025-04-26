import {Component} from '@angular/core';
import {AppMessageStore} from '../../services/app-message.store';
import {AsyncPipe, NgClass, NgIf} from '@angular/common';

@Component({
  selector: 'app-message-banner',
  imports: [
    AsyncPipe,
    NgIf,
    NgClass
  ],
  templateUrl: './message-banner.component.html',
})
export class MessageBannerComponent {
  constructor(public appMessageStore: AppMessageStore) {
  }
}
