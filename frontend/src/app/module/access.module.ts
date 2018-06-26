import { AppRoutingModule } from './../app-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from 'src/app/widget/login/login.component';
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
    LoginComponent,
    RegisterComponent
  ],
  exports: [
    LoginComponent,
    RegisterComponent
  ]

})
export class AccessModule { }
