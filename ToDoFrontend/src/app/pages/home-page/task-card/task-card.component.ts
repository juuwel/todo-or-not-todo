import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IconButtonComponent } from "../../../components/icon-button/icon-button.component";
import { CreateTaskItemDto, TaskItemDto, UpdateTaskItemDto, uuid } from '../../../datamodel/task.types';

@Component({
  selector: 'app-task-card',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    IconButtonComponent
  ],
  templateUrl: './task-card.component.html',
  styles: ``
})
export class TaskCardComponent implements OnInit {
  @Input() isAddCard = false;
  @Input() task: TaskItemDto | undefined;

  @Output() taskCreatedEmitter = new EventEmitter<CreateTaskItemDto>();
  @Output() taskUpdatedEmitter = new EventEmitter<UpdateTaskItemDto>();
  @Output() taskCompletedStatusEmitter = new EventEmitter<uuid>();
  @Output() taskDeletedEmitter = new EventEmitter<uuid>();

  protected taskForm: FormGroup;
  protected isInputMode = false;

  protected color: string = this.getRandomColor();
  protected inputColor: string = this.createShadeFromBgColor(30);
  protected buttonColor: string = this.createShadeFromBgColor(-30);

  constructor(
    private fb: FormBuilder,
  ) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
    });
  }

  ngOnInit(): void {
    if (this.task) {
      this.taskForm.patchValue({
        title: this.task.title,
        description: this.task.description,
      });
    }
  }

  private getRandomColor(): string {
    const randomRed = Math.floor((Math.random() * 128) + 127); // Ensure the value is between 127 and 255 to avoid too dark colors
    const randomGreen = Math.floor((Math.random() * 128) + 127);
    const randomBlue = Math.floor((Math.random() * 128) + 127);
    return `rgb(${randomRed}, ${randomGreen}, ${randomBlue})`;
  }

  private createShadeFromBgColor(percent: number): string {
    const rgb = this.color.match(/\d+/g)!.map(Number); // Extract RGB values
    if (!rgb) return this.color; // Fallback if parsing fails

    const adjustedRgb = rgb.map(value => {
      const adjustment = Math.floor((255 - value) * (percent / 100)); // Calculate adjustment for lightening
      return Math.min(255, Math.max(0, value + adjustment)); // Ensure the value stays between 0 and 255
    });

    return `rgb(${adjustedRgb.join(', ')})`;
  }

  protected onSaveClicked(): void {
    this.isInputMode = false;
    const title = this.taskForm.get('title')!.value;
    const description = this.taskForm.get('description')!.value;
    const task: CreateTaskItemDto = { title, description };
    if (this.isAddCard) {
      this.taskCreatedEmitter.emit(task);
    } else {
      const updatedTask: UpdateTaskItemDto = {
        id: this.task!.id,
        title,
        description,
      };
      this.taskUpdatedEmitter.emit(updatedTask);
    }
  }

  protected onDeleteClicked(): void {
    if (this.task) {
      this.taskDeletedEmitter.emit(this.task.id);
    }
  }

  protected onCompleteClicked(): void {
    if (this.task) {
      this.taskCompletedStatusEmitter.emit(this.task.id);
    }
  }

  protected startAddingTask() {
    this.isInputMode = true;
  }

  protected cancelAddingTask() {
    this.isInputMode = false;

    if (this.task) {
      this.taskForm.patchValue({
        title: this.task.title,
        description: this.task.description,
      });

      return;
    }

    this.taskForm.reset();
  }
}
