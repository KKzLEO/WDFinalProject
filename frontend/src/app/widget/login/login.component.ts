import { Component } from "@angular/core";
import '../../../assets/js/uikit.min.js';
import { ViewChild } from "@angular/core";
import { ElementRef } from "@angular/core";
import { LoginDataModel } from "src/app/widget/login/login-data-model";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import { Output } from "@angular/core";
import { EventEmitter } from "@angular/core";
import swal from 'sweetalert';
declare var UIkit : any;

@Component({
    selector: "login",
    templateUrl: "./login.component.html"
})

export class LoginComponent {
    constructor(private proxy:ProxyService,private config:ApiUrlConfigService){

    }
    @ViewChild("loginWindow") loginWindow : ElementRef;


    public loginData : LoginDataModel = new LoginDataModel();

    ngOninit(){

    }

    public openLoginWindow(){
        UIkit.modal(this.loginWindow.nativeElement).show();
    }

    public closeLoginWindow(){
        UIkit.modal(this.loginWindow.nativeElement).hide();
    }

    public login(){
        let apiUrl = this.config.apiAccessUrl + "/login";
        this.proxy.post(apiUrl,this.loginData).subscribe(
            r=>{
                if(r.status === 900){
                    swal(r.message);
                }else{
                    swal(r.message);
                    this.closeLoginWindow();
                }
            },
            e=>{
                swal("系統錯誤");
            }

        )
    }
}