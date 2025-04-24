import {Injectable} from '@angular/core';
import {BehaviorSubject, filter, Observable} from 'rxjs';
import {MessageTypes} from '../datamodel/message.types';

@Injectable({
  providedIn: 'root'
})
export class AppMessageStore {
  private messageSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  private messageType: MessageTypes | null = null;

  // Expose the observable for components to subscribe to
  public get message$(): Observable<string> {
    return this.messageSubject.asObservable().pipe(
      filter((message): message is string => message !== null)
    );
  }

  // Update the message value
  public setMessage(message: string, messageType: MessageTypes): void {
    this.messageSubject.next(message);
    this.messageType = messageType;
  }

  // Clear the message
  public clearMessage(): void {
    this.messageSubject.next(null);
    this.messageType = null;
  }

  public getMessageType(): MessageTypes | null {
    return this.messageType;
  }
}
