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
declare var UIkit : any;

@Component({
    selector: "access",
    templateUrl: "./access.component.html"
})

export class AccessComponent {

    constructor(private userService:UserService){
        
    }
    @ViewChild("loginWindow") loginWindow : ElementRef;
    @ViewChild("registerWindow") registerWindow : ElementRef;

    public loginData : MemberDataModel = new MemberDataModel();
    public registerData : MemberDataModel = new MemberDataModel();

    ngOninit(){
        this.registerData.titleCode= "1";
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
        this.clear();
    }

    public register(){
        this.userService.register(this.registerData);
    }

    public clear(){
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

}