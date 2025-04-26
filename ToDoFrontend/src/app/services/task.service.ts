import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateTaskItemDto, TaskItemDto, UpdateTaskItemDto, uuid } from '../datamodel/task.types';
import { AppMessageStore } from '../stores/app-message.store';
import { TaskStore } from '../stores/task.store';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private readonly baseUrl = 'http://localhost:1000/api/v1';
  private readonly taskBaseUrl = this.baseUrl + "/tasks";

  constructor(
    private httpClient: HttpClient,
    private taskStore: TaskStore,
    private appMessageStore: AppMessageStore
  ) { }

  public async getAllTasks() {
    this.httpClient.get<TaskItemDto[]>(this.taskBaseUrl).subscribe(tasks => {
      this.taskStore.tasks$ = tasks;
    });
  }

  public async toggleTaskStatus(taskId: uuid): Promise<void> {
    this.httpClient.patch<TaskItemDto>(`${this.taskBaseUrl}/status/${taskId}`, {}).subscribe(
      {
        next: (updatedTask) => {
          const index = this.taskStore.tasks.findIndex(task => task.id === updatedTask.id);
          if (index !== -1) {
            const newTasks = [...this.taskStore.tasks];
            newTasks[index] = updatedTask;
            this.taskStore.tasks$ = newTasks;
          }
        },
        error: (error) => {
          console.error('Error toggling task status:', error);
          this.appMessageStore.setMessage('Error toggling task status', error.message);
        }
      }
    );
  }

  public async deleteTask(taskId: uuid): Promise<void> {
    this.httpClient.delete<void>(`${this.taskBaseUrl}/${taskId}`)
      .subscribe(
        {
          next: () => {
            this.taskStore.tasks$ = this.taskStore.tasks.filter(task => task.id !== taskId);
          },
          error: (error) => {
            console.error('Error deleting task:', error);
            this.appMessageStore.setMessage('Error deleting task', error.message);
          }
        }
      );
  }

  public async createTask(createTaskItemDto: CreateTaskItemDto): Promise<void> {
    this.httpClient.post<TaskItemDto>(this.taskBaseUrl, createTaskItemDto)
      .subscribe(
        {
          next: (createdTask) => {
            this.taskStore.tasks$ = [...this.taskStore.tasks, createdTask];
            this.appMessageStore.setMessage('Task created successfully', 'success');
          },
          error: (error) => {
            console.error('Error creating task:', error);
            this.appMessageStore.setMessage('Error creating task', error.message);
          }
        }
      );
  }

  public async updateTask(updateTaskItemDto: UpdateTaskItemDto): Promise<void> {
    this.httpClient.put<TaskItemDto>(`${this.taskBaseUrl}`, updateTaskItemDto)
      .subscribe(
        {
          next: (updatedTask) => {
            const index = this.taskStore.tasks.findIndex(task => task.id === updateTaskItemDto.id);
            if (index !== -1) {
              const newTasks = [...this.taskStore.tasks];
              newTasks[index] = updatedTask;
              this.taskStore.tasks$ = newTasks;
            }
            this.appMessageStore.setMessage('Task updated successfully', 'success');
          },
          error: (error) => {
            console.error('Error updating task:', error);
            this.appMessageStore.setMessage('Error updating task', error.message);
          }
        }
      );
  }
}
