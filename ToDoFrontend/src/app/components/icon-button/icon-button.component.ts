import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-icon-button',
  imports: [],
  templateUrl: './icon-button.component.html',
  styles: ``
})
export class IconButtonComponent {
  @Input() iconName: string = 'add';
  @Output() clickEvent = new EventEmitter<void>();

  protected onClick(): void {
    this.clickEvent.emit();
  }
}
