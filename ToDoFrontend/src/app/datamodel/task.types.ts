export type uuid = string;

export interface TaskItemDto {
  id: uuid;
  userId: uuid;
  title: string;
  description: string;
  completedAt?: Date;
  createdAt: Date;
}

export interface CreateTaskItemDto {
  title: string;
  description: string;
}


