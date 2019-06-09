import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PageModule } from 'src/app/module/page.module';
import { AccessModule } from 'src/app/module/access.module';
import { ProxyService } from './proxy/proxy.service';
import { HttpClientModule } from '@angular/common/http'; 
import { HttpModule } from '@angular/http';
import swal from 'sweetalert';
import { ApiUrlConfigService } from 'src/app/config/api-url-config.service';
import { FormsModule } from '@angular/forms';
import { UserService } from 'src/app/service/user.service';
import { CourseService } from 'src/app/service/course.service';
import { ShoppingService } from 'src/app/service/shopping.service';
import { CustomerFooterComponent } from 'src/app/widget/customer-footer/customer-footer.component'
import { FacebookService } from 'ngx-facebook';
import { AuthGuardService } from 'src/app/service/authguard.service';
import { MaintainPageComponent } from 'src/app/page/maintain/maintain-page.component';


@NgModule({
  declarations: [
    AppComponent,
    CustomerFooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PageModule,
    AccessModule,
    HttpClientModule,
    HttpModule,
    FormsModule
    
  ],
  providers: [
    ProxyService,
    ApiUrlConfigService,
    UserService,
    CourseService,
    ShoppingService,
    FacebookService,
    AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
