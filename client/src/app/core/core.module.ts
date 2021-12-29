import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from "../shared/shared.module";
import {ConfirmationService, MessageService} from "primeng/api";
import {NotificationService} from "./services/notification.service";
import {ShowForRoleDirective} from "./directives/show-for-role.directive";

@NgModule({
  declarations: [ShowForRoleDirective],
  imports: [
    CommonModule,
    SharedModule,
  ],
  providers: [
    MessageService,
    ConfirmationService,
    NotificationService
  ],
  exports: [
    ShowForRoleDirective
  ]
})
export class CoreModule { }
