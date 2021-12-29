import {Injectable} from '@angular/core';
import {ConfirmationService, MessageService} from 'primeng/api';
import {ApiResponse} from "../../shared/models/api-response.model";

@Injectable()
export class NotificationService {

  constructor(private messageService: MessageService, private confirmationService: ConfirmationService) {
  }

  public Success(message: string, title?: string) {
    this.messageService.add({severity: 'success', summary: title ? title : 'Успешно.', detail: message});
  }

  public Error(message: string, title?: string) {
    this.messageService.add({severity: 'error', summary: title ? title : 'Ошибка.', detail: message});
  }

  public Warning(message: string, title?: string) {
    this.messageService.add({severity: 'warn', summary: title ? title : 'Внимание.', detail: message});
  }

  public FromApiResponse(apiResponse: ApiResponse) {
    if (apiResponse.errorMessages.length > 0) {
      this.Error(apiResponse.errorMessages[0]);
      for (const errorMessage of apiResponse.errorMessages) {
        console.error(errorMessage);
      }
    }

    if (apiResponse.successMessages.length > 0) {
      for (const successMessage of apiResponse.successMessages) {
        this.Success(successMessage);
      }
    }

    if (apiResponse.warningMessages.length > 0) {
      this.Warning(apiResponse.warningMessages[0]);
      for (const warningMessage of apiResponse.warningMessages) {
        console.warn(warningMessage);
      }
    }
  }

  public Confirm(title: string = 'Подтверждение', message: string = 'Вы уверены?',
                 okCallback: () => any, denyCallback?: () => any) {
    if (title == null) {
      title = 'Подтверждение';
    }
    return this.confirmationService.confirm({
      message: message,
      header: title,
      accept: () => {
        if (okCallback) {
          okCallback();
        }
      },
      reject: () => {
        if (denyCallback != null) {
          denyCallback();
        }
      }
    });
  }
}
