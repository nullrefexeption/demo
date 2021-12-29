import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import {SharedModule} from "../shared/shared.module";
import {FlightsModule} from "../flights/flights.module";

@NgModule({
  declarations: [
    HomeComponent
  ],
    imports: [
        CommonModule,
        SharedModule,
        FlightsModule
    ],
  exports: [HomeComponent]
})
export class HomeModule { }
