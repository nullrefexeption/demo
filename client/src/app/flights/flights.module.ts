import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightsComponent } from './flights.component';
import {FlightsRoutingModule} from "./flights-routing.module";
import {TableModule} from "primeng/table";
import {CalendarModule} from "primeng/calendar";
import {FormsModule} from "@angular/forms";
import {DialogModule} from "primeng/dialog";
import {DropdownModule} from "primeng/dropdown";
import {CoreModule} from "../core/core.module";
import {InputTextModule} from "primeng/inputtext";
import {InputNumberModule} from "primeng/inputnumber";

@NgModule({
    declarations: [
        FlightsComponent,
    ],
    exports: [
        FlightsComponent
    ],
  imports: [
    CommonModule,
    FlightsRoutingModule,
    TableModule,
    CalendarModule,
    FormsModule,
    DialogModule,
    DropdownModule,
    CoreModule,
    InputTextModule,
    InputNumberModule
  ]
})
export class FlightsModule { }
