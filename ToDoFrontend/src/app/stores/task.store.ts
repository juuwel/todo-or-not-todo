import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {TaskItemDto} from '../datamodel/task.types';

@Injectable({
  providedIn: 'root'
})
export class TaskStore  {
  private readonly tasksSubject = new BehaviorSubject<TaskItemDto[]>([]);

  public get tasks(): Observable<TaskItemDto[]> {
    return this.tasksSubject.asObservable();
  }

  public set tasks(value: TaskItemDto[]) {
    this.tasksSubject.next(value);
  }
}
