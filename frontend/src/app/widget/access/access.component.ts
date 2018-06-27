import { Component } from "@angular/core";
import '../../../assets/js/uikit.min.js';
import { ViewChild } from "@angular/core";
import { ElementRef } from "@angular/core";
import { MemberDataModel } from "src/app/widget/access/member-data-model";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import { Output } from "@angular/core";
import { EventEmitter } from "@angular/core";
import swal from 'sweetalert';
import { UserService } from "src/app/service/user.service";
import { Observable } from "rxjs";
import { FacebookService, InitParams, LoginResponse, UIParams, UIResponse } from "ngx-facebook";
declare var UIkit : any;

@Component({
    selector: "access",
    templateUrl: "./access.component.html"
})

export class AccessComponent {

    constructor(private userService:UserService,private fb: FacebookService){
        let initParams: InitParams = {
            appId: '213790156122801',
            xfbml: true,
            version: 'v2.8'
        };
       
        fb.init(initParams);
        this.userService.addListener(this.userServiceReceiver.bind(this));
    }
    @ViewChild("loginWindow") loginWindow : ElementRef;
    @ViewChild("registerWindow") registerWindow : ElementRef;

    public loginData : MemberDataModel = new MemberDataModel();
    public registerData : MemberDataModel = new MemberDataModel();

    ngOninit(){
        this.registerData.titleCode= "1";
    }

    public userServiceReceiver(message){
        if(message=="go_register") this.goRegisterPage();
    }

    public openLoginWindow(){
        UIkit.modal(this.loginWindow.nativeElement).show();
    }

    public closeLoginWindow(){
        UIkit.modal(this.loginWindow.nativeElement).hide();
    }

    public openRegisterWindow(){
        UIkit.modal(this.registerWindow.nativeElement).show();
    }

    public closeRegisterWindow(){
        UIkit.modal(this.registerWindow.nativeElement).hide();
    }

    public login(){
        this.userService.login(this.loginData);
        this.clearLoginData();
    }

    public register(){
        this.userService.register(this.registerData);
        this.clearRegisterData();
    }

    public clearLoginData(){
        this.loginData.account = "";
        this.loginData.password = "";
    }

    public clearRegisterData(){
        this.registerData = new MemberDataModel();
        this.registerData.titleCode = "1";
    }

    public goLoginPage(){
        this.closeRegisterWindow();
        this.openLoginWindow();
    }

    public goRegisterPage(){
        this.closeLoginWindow();
        this.openRegisterWindow();
    }

    public loginWithFacebook(): void {
        this.fb.login()
          .then((response: LoginResponse) =>{
            this.BindingFbIdAndLogin(response.authResponse.accessToken);
            
          })
          .catch((error: any) => console.error(error));
    }
    public logoutWithFacebook(): void {
        this.fb.logout().then(() => console.log('Logged out!'));
    }

    public share(url: string) {
 
        let params: UIParams = {
          href: 'https://github.com/zyra/ngx-facebook',
          method: 'share'
        };
       
        this.fb.ui(params)
          .then((res: UIResponse) => console.log(res))
          .catch((e: any) => console.error(e));
       
    }

    public BindingFbIdAndLogin(token:string){
        this.loginData.fbToken = token;
        this.registerData.fbToken = token;
        this.login();
    }
}