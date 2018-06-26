import { AppRoutingModule } from './../app-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccessComponent } from 'src/app/widget/access/access.component';
import { RegisterComponent } from 'src/app/widget/register/register.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
@NgModule({
  imports: [
    AppRoutingModule,
    CommonModule,
    BrowserModule,
    FormsModule
  ],
  declarations: [
    AccessComponent,
    RegisterComponent
  ],
  exports: [
    AccessComponent,
    RegisterComponent
  ]

})
export class AccessModule { }
