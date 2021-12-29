import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import {ErrorInterceptor} from "./core/interceptors/error.interceptor";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {JwtInterceptor} from "./core/interceptors/jwt.interceptor";
import {RouterModule} from "@angular/router";
import {SharedModule} from "./shared/shared.module";
import {AppRoutingModule} from "./app-routing.module";
import {CoreModule} from "./core/core.module";
import {HomeModule} from "./home/home.module";
import { RecaptchaModule } from "ng-recaptcha";
import {ToastModule} from "primeng/toast";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    SharedModule,
    CoreModule,
    HomeModule,
    HttpClientModule,
    AppRoutingModule,
    RecaptchaModule,
    ToastModule,
    ConfirmDialogModule,
    BrowserAnimationsModule
  ],
  providers: [
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
