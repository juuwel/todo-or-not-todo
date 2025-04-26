export interface AuthResponse {
  userId: string;
  email: string;
  token: string;
}

export class ProblemDetails {
  detail: string;
  status: string;
  title: string;
  error: {
    code: string;
    description: string;
  }[];

  constructor(detail: string, status: string, title: string, error: {
    code: string;
    description: string;
  }[] | undefined) {
    this.detail = detail;
    this.status = status;
    this.title = title;
    this.error = error ?? [];
  }

  toErrorMessage() {
    let innerErrorDetails = this.detail + '\n';

    for (const error of this.error) {
      innerErrorDetails += `- ${error.description}\n`;
    }

    return innerErrorDetails.trim();
  }
}
