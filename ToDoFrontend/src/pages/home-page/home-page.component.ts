import { Component } from '@angular/core';

@Component({
  selector: 'app-home-page',
  imports: [],
  templateUrl: './home-page.component.html',
  styles: ``
})
export class HomePageComponent {

  protected readonly tasks = [
    "Do the dishes",
    "Buy groceries",
    "Clean up",
    "Feed the dog",
    "Laundry",
    "Go to gym",
    "Homework"
  ];

  protected getRandomColor(): string {
    const randomColor = Math.floor(Math.random() * 16777215).toString(16);
    return `#${randomColor.padStart(6, '0')}`;
  }
}
