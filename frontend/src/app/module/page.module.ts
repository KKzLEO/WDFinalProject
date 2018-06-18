import { AppRoutingModule } from './../app-routing.module';
import { NgModule } from '@angular/core';
import { IndexPageComponent } from 'src/app/page/index/index-page.component';
import { ShoppingCartPageComponent } from 'src/app/page/shopping-cart/shopping-cart-page.component';
import { TeachersPageComponent } from 'src/app/page/teachers/teachers-page.component';

@NgModule({
  imports: [
    AppRoutingModule
  ],
  declarations: [
    // Page
    IndexPageComponent,
    ShoppingCartPageComponent,
    TeachersPageComponent
  ],
  exports: [
    // Page
    IndexPageComponent,
    ShoppingCartPageComponent,
    TeachersPageComponent
  ]

})
export class PageModule { }
