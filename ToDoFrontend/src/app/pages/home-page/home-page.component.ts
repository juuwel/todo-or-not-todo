import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CreateTaskItemDto, TaskItemDto, UpdateTaskItemDto, uuid } from '../../datamodel/task.types';
import { TaskService } from '../../services/task.service';
import { TaskStore } from '../../stores/task.store';
import { TaskCardComponent } from "./task-card/task-card.component";

@Component({
  selector: 'app-home-page',
  imports: [
    AsyncPipe,
    ReactiveFormsModule,
    TaskCardComponent
  ],
  templateUrl: './home-page.component.html',
  styles: ``
})
export class HomePageComponent {
  constructor(
    private taskService: TaskService,
    public taskStore: TaskStore,
  ) {
    this.taskService.getAllTasks();
  }

  async taskCreated($event: CreateTaskItemDto) {
    await this.taskService.createTask($event);
  }

  async taskUpdated($event: UpdateTaskItemDto) {
    await this.taskService.updateTask($event);
  }

  async taskDeleted($event: uuid) {
    await this.taskService.deleteTask($event);
  }

  async toggleTaskStatus($event: uuid) {
    await this.taskService.toggleTaskStatus($event);
  }

  trackByTaskId(index: number, task: TaskItemDto): string {
    return task.id;
  }
}
