import { Component } from '@angular/core';
import { ViewChild } from '@angular/core';
import { AccessComponent } from 'src/app/widget/access/access.component';
import { UserService } from 'src/app/service/user.service';
import { MemberDataModel } from 'src/app/widget/access/member-data-model';
import { ProxyService } from 'src/app/proxy/proxy.service';
import { ApiUrlConfigService } from 'src/app/config/api-url-config.service';
import { Headers } from '@angular/http';
import { CourseFilterModel } from 'src/app/page/course/course-filter-model';
import { CourseService } from 'src/app/service/course.service';
import { Router } from '@angular/router';
import { ShoppingService } from 'src/app/service/shopping.service';
import { CourseDataModel } from 'src/app/page/course/course-data-model';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    public theme : string ="white";

    public isLoggedin: boolean = false;
    public isAdmin : boolean = false;
    public userData:MemberDataModel;
    public courseFilterArg : CourseFilterModel = new CourseFilterModel();
    public courseList : CourseDataModel[] = new Array<CourseDataModel>();
    
    @ViewChild("accessComponent") accessComponent : AccessComponent;
    constructor(private shoppingService:ShoppingService,private router:Router,private courseService:CourseService,private userService:UserService,private proxy:ProxyService,private config:ApiUrlConfigService){
      this.isLoggedin = this.userService.isLoggedin;
      this.isAdmin = this.userService.isAdmin;
      this.userService.addListener(this.userServiceReceiver.bind(this));
    }

    ngOnInit(){
      this.searchCourse();
      setTimeout(() => {
        if(this.router.url === '/shopping-cart') this.theme="white";
      }, 100);
      this.shoppingService.addListener(this.shoppingServiceReceiver.bind(this));
      this.courseList = JSON.parse(localStorage.getItem("shopping-cart"));
      if(this.courseList == null) this.courseList = new Array<CourseDataModel>();
    }

    ngAfterViewInit(){

    }
    
    public openLoginWindow(){
      this.accessComponent.openLoginWindow();
    }

    public openRegisterWindow(){
      this.accessComponent.openRegisterWindow();
    }

    public userServiceReceiver(message){
      this.isLoggedin = this.userService.isLoggedin;
      this.isAdmin = this.userService.isAdmin;
    }

    public logout(){
      swal({
        title: "確定要登出？",
        icon: "warning",
        buttons: ['取消', '確定']
      })
      .then((willDelete) => {
        if (willDelete) {
          this.userService.logout();
        } else {
        }
      });
      
    }

    public testauth(){
      let apiUrl = this.config.apiAccessUrl + "/testauth";
      let headers = new Headers({ "Content-Type": "application/x-www-form-urlencoded" });
      this.proxy.setHeaders(headers);
      this.proxy.post(apiUrl,"").subscribe(
          r=>{

          },
          e=>{
              swal("系統錯誤");
          }

      )
    }

    public searchCourse(){
        this.courseService.searchCourse(this.courseFilterArg);
    }

    public goShoppingCartPage(){
        this.router.navigateByUrl('shopping-cart');
        this.changeNavTheme("white");
    }

    public goTeachersPagePage(){
      this.router.navigateByUrl('teachers');
      this.changeNavTheme("white");
  }

    public goHomePage(){
        window.location.href = "./index";
    }

    public changeNavTheme(theme){
        this.theme = theme;
    }

    public themeClass(className){
        if(this.theme == '') return className;
        return className + "-" + this.theme + "-theme";
    }

    public shoppingServiceReceiver(){
        this.courseList = JSON.parse(localStorage.getItem("shopping-cart"));
        if(this.courseList == null) this.courseList = new Array<CourseDataModel>();
    }
}
