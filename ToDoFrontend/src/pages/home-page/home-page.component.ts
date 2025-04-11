import { Component } from '@angular/core';
import {TaskService} from '../../services/task.service';

@Component({
  selector: 'app-home-page',
  imports: [],
  templateUrl: './home-page.component.html',
  styles: ``
})
export class HomePageComponent {
  public tasks: string[] = [];

  constructor(public taskService: TaskService) {
    this.tasks = this.taskService.getTasks();
  }

  protected getRandomColor(): string {
    const randomColor = Math.floor(Math.random() * 16777215).toString(16);
    return `#${randomColor.padStart(6, '0')}`;
  }
}
