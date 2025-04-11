import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor() { }

  public getTasks(): string[] {
    return [
      "Do the dishes",
      "Buy groceries",
      "Clean up",
      "Feed the dog",
      "Laundry",
      "Go to gym",
      "Homework"
    ];
  }
}
