import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextInputComponent } from './components/text-input/text-input.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [
    TextInputComponent,
    NavBarComponent
  ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        RouterModule,
        FormsModule,
    ],
  exports: [
    TextInputComponent,
    ReactiveFormsModule,
    NavBarComponent,
    RouterModule,
    FormsModule
  ]
})
export class SharedModule { }
