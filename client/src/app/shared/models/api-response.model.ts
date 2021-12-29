export enum ApiResponseStatus {
  Error = 0,
  Success = 1
}

export interface FieldErrors {
  [field: string]: Array<string>;
}

export class ApiResponse {
  status: ApiResponseStatus;
  errorMessages: Array<string>;
  warningMessages: Array<string>;
  successMessages: Array<string>;
  fieldErrorMessages: FieldErrors;
  data: any;

  constructor() {
    this.status = ApiResponseStatus.Success;
    this.errorMessages = [];
    this.warningMessages = [];
    this.successMessages = [];
    this.fieldErrorMessages = {};
  }
}
