import { Injectable } from '@angular/core';
import {CreateTaskItemDto, TaskItemDto, uuid} from '../datamodel/task.types';
import {HttpClient} from '@angular/common/http';
import {TaskStore} from '../stores/task.store';
import {AuthStore} from '../stores/auth.store';
import {firstValueFrom} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private readonly baseUrl = 'http://localhost:1000/api/v1';
  private readonly taskBaseUrl = this.baseUrl + "/tasks";

  constructor(
    private httpClient: HttpClient,
    private taskStore: TaskStore,
  ) { }

  public async getAllTasks() {
    this.httpClient.get<TaskItemDto[]>(this.taskBaseUrl).subscribe(tasks => {
      this.taskStore.tasks = tasks;
    });
  }

  public async toggleTaskStatus(taskId: uuid): Promise<void> {

  }

  public async deleteTask(taskId: uuid): Promise<void> {

  }

  public async createTask(title: string, description: string): Promise<void> {
    const task: CreateTaskItemDto = {
      title: title,
      description: description,
    };
  }
}
