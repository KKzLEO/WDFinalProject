import { Component } from '@angular/core';
import { ViewChild } from '@angular/core';
import { AccessComponent } from 'src/app/widget/access/access.component';
import { UserService } from 'src/app/service/user.service';
import { MemberDataModel } from 'src/app/widget/access/member-data-model';
import { ProxyService } from 'src/app/proxy/proxy.service';
import { ApiUrlConfigService } from 'src/app/config/api-url-config.service';
import { Headers } from '@angular/http';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    public isLoggedin: boolean = false;
    public isAdmin : boolean = false;
    public userData:MemberDataModel;

    constructor(private userService:UserService,private proxy:ProxyService,private config:ApiUrlConfigService){
      this.isLoggedin = this.userService.isLoggedin;
      this.isAdmin = this.userService.isAdmin;
      this.userService.addListener(this.userServiceReceiver.bind(this));
    }

    @ViewChild("accessComponent") accessComponent : AccessComponent;
    
    public openLoginWindow(){
      this.accessComponent.openLoginWindow();
    }

    public openRegisterWindow(){
      this.accessComponent.openRegisterWindow();
    }

    public userServiceReceiver(message:boolean){
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
          swal("登出失敗");
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




}
