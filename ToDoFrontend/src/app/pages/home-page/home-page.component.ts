import { Component } from '@angular/core';
import {TaskService} from '../../services/task.service';
import {TaskStore} from '../../stores/task.store';
import {AsyncPipe} from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {TaskItemDto} from '../../datamodel/task.types';

@Component({
  selector: 'app-home-page',
  imports: [
    AsyncPipe,
    ReactiveFormsModule
  ],
  templateUrl: './home-page.component.html',
  styles: ``
})
export class HomePageComponent {
  protected isAddingTask: boolean = false;
  protected taskForm: FormGroup;

  constructor(
    private taskService: TaskService,
    public taskStore: TaskStore,
    private fb: FormBuilder,
  ) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
    });

    this.taskService.getAllTasks();
  }

  protected getRandomColor(): string {
    const randomColor = Math.floor(Math.random() * 16777215).toString(16);
    return `#${randomColor.padStart(6, '0')}`;
  }

  protected startAddingTask(): void {
    this.isAddingTask = true;
  }

  protected async createTask() {
    const title = this.taskForm.get('title')!.value;
    const description = this.taskForm.get('description')!.value;
    await this.taskService.createTask(title, description);
  }
}
