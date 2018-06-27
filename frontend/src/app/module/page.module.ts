import { AppRoutingModule } from './../app-routing.module';
import { NgModule } from '@angular/core';
import { IndexPageComponent } from 'src/app/page/index/index-page.component';
import { ShoppingCartPageComponent } from 'src/app/page/shopping-cart/shopping-cart-page.component';
import { TeachersPageComponent } from 'src/app/page/teachers/teachers-page.component';
import { CommonModule } from '@angular/common';
import { AccessModule } from 'src/app/module/access.module';
import { CourseListPageComponent } from 'src/app/page/course/course-list-page.component';
import { MaintainPageComponent } from 'src/app/page/maintain/maintain-page.component';
import { CoursePageComponent } from 'src/app/page/course/course-page.component';

@NgModule({
  imports: [
    AppRoutingModule,
    CommonModule,
    AccessModule
  ],
  declarations: [
    // Page
    IndexPageComponent,
    ShoppingCartPageComponent,
    TeachersPageComponent,
    CourseListPageComponent,
    MaintainPageComponent,
    CoursePageComponent
  ],
  exports: [
    // Page
    IndexPageComponent,
    ShoppingCartPageComponent,
    TeachersPageComponent,
    CourseListPageComponent,
    MaintainPageComponent,
    CoursePageComponent
  ]

})
export class PageModule { }
