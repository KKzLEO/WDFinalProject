import { Injectable } from "@angular/core";
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { MemberDataModel } from '../widget/access/member-data-model';
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import swal from 'sweetalert';
import { FacebookService } from "ngx-facebook";
@Injectable()
export class UserService {

    public isLoggedin = false;
    public isAdmin = false;
    public listeners = [];

    constructor(private fb:FacebookService,private proxy:ProxyService,private config:ApiUrlConfigService){
        this.isLoggedin = localStorage.getItem("user") == null ? false : true;
        this.isAdmin = localStorage.getItem("user") == null ? false : (JSON.parse(localStorage.getItem("user")).titleCode == '0' ? true : false);
    }

    public addListener(listener:Function){
        this.listeners.push(listener);
    }

    public removeListener(listener){
        this.listeners.splice(this.listeners.indexOf(listener), 1);
    }

    public notify(message?) {
		this.listeners.forEach(listener => {
			listener(message);
		})
	}

    /**
    *  登入使用者，並通知所有訂閱者
    */
    public login(member:MemberDataModel) {
        let apiUrl = this.config.apiAccessUrl + "/login";
        this.proxy.post(apiUrl,member).subscribe(
            r=>{
                if(r.status === 900){
                    swal(r.message);
                    this.notify("go_register");
                }else{
                    swal({title:r.message,text:"歡迎登入，"+ r.data.cName,icon: "success"});
                    localStorage.setItem('user',JSON.stringify(r.data));
                    if(r.data.titleCode == '0') this.isAdmin = true;
                    else this.isAdmin = false;
                    this.isLoggedin = true;
                    this.notify(true);
                }
            },
            e=>{
                swal("系統錯誤");
            }

        )
      }
  
    /**
     * 登出使用者，並通知所有訂閱者
    */
    public logout() : void {
        let apiUrl = this.config.apiAccessUrl + "/logout";
        this.proxy.post(apiUrl,null).subscribe(
            r=>{
                if(r.status === 900){
                    swal(r.message);
                }else{
                    swal(r.message);
                    localStorage.removeItem('user');
                    this.isLoggedin = false;
                    this.logoutWithFacebook();
                    this.notify(false);
                }
            },
            e=>{
                swal("系統錯誤");
            }

        )
    }

    public register(userData:MemberDataModel){
        let apiUrl = this.config.apiAccessUrl + "/register";
        this.proxy.post(apiUrl,userData).subscribe(
            r=>{
                if(r.status === 900){
                    swal(r.message);
                }else{
                    swal(r.message);
                }
            },
            e=>{
                swal("系統錯誤");
            }

        )
    }

    public logoutWithFacebook(): void {
        this.fb.logout().then(() => console.log('Logged out!'));
    }
  
}