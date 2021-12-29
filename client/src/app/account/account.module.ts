import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import {SharedModule} from "../shared/shared.module";
import {AccountRoutingModule} from "./account-routing.module";
import {RouterModule} from "@angular/router";

@NgModule({
    declarations: [
        LoginComponent
    ],
    exports: [
        LoginComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        AccountRoutingModule,
      RouterModule
    ]
})
export class AccountModule { }
