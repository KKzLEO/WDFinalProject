import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NavigationEnd } from "@angular/router";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import { ViewChild } from "@angular/core";
import { ElementRef } from "@angular/core";
import '../../../assets/js/uikit.min.js';
import { CodeDataModel } from "src/app/model/code.data.model";
import { 
    ValidatorFn,
    Validator, 
    AbstractControl, 
    FormControl, 
    Validators,
    FormGroup
} from '@angular/forms';
import { MemberDataModel } from "src/app/widget/access/member-data-model";
import { confirmPasswordValidator } from "src/app/shared/confirm-password.directive";
declare var UIkit : any;

@Component({
    selector: "maintain-user-page",
    templateUrl: "./maintain-user-page.component.html",
    styleUrls: ["./maintain-user-page.css"]
})

export class MaintainUserPageComponent {
    
    public userDataList:MemberDataModel[] = Array<MemberDataModel>();
    public userData:MemberDataModel = new MemberDataModel();
    public genderCodeData:CodeDataModel[]= Array<CodeDataModel>();
    public titleCodeData:CodeDataModel[]=Array<CodeDataModel>();
    public op:string="create";
    public lastOp:string="create";
    public userDataForm = new FormGroup({
        phone: new FormControl('',[Validators.required,Validators.pattern(/^[0-9]*$/)]),
        email: new FormControl('',Validators.pattern(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)),
        cName: new FormControl('',Validators.required),
        eName: new FormControl('',Validators.required),
        birthday: new FormControl(''),
        genderCode: new FormControl(''),
        titleCode: new FormControl('',Validators.required),
        account: new FormControl('',Validators.required),
        password: new FormControl('',Validators.required),
        confirmPassword: new FormControl('',[Validators.required,confirmPasswordValidator()])
    });
    public isSubmit:boolean = false;

    @ViewChild("createUserWindow") userWindow : ElementRef;

    constructor(private configService:ApiUrlConfigService,private router:Router,private proxy:ProxyService){

    }

    ngOnInit(){
        this.getCodeData();
        this.getUserData();
    }

    public openWindow(opertaion:string,index?){
        this.op = opertaion;
        if(this.op=="create" && this.op == this.lastOp){
            this.openUserWindow();
        }else if(this.op=="create" && this.lastOp=="update"){
            this.clearUserData();
            this.openUserWindow();
        }else if(this.op=="update" && this.lastOp=="update"){
            // this.userDataForm.reset();
            this.handleUpdateWindow(index);
        }else if(this.op=="update" && this.lastOp=="create"){
            this.clearUserData();
            this.handleUpdateWindow(index);
        }

        this.lastOp = this.op;
    }

    public clearUserData(){
        this.userData = new MemberDataModel();
        this.isSubmit = false;
        this.userDataForm.reset();
    }

    public openUserWindow(){
        UIkit.modal(this.userWindow.nativeElement).show();
    }

    public closeUserWindow(){
        UIkit.modal(this.userWindow.nativeElement).close();
    }

    public handleUpdateWindow(index:number){
        this.userData = Object.assign({},this.userDataList[index]);
        this.openUserWindow();
    }

    public getCodeData(){
        let genderApiUrl = this.configService.apiGenderCodeUrl;
        let titleApiUrl = this.configService.apiTitleCodeUrl;
        this.proxy.proxyGet(genderApiUrl).subscribe(
            r=>{
                this.genderCodeData = r.data;
            }
        );
        this.proxy.proxyGet(titleApiUrl).subscribe(
            r=>{
                this.titleCodeData = r.data;
            }
        );

    }

    public getUserData(){
        let apiUrl = this.configService.apiQueryUserUrl;
        this.proxy.proxyPost(apiUrl,{}).subscribe(
            r=>{
                if(r.status === 200){
                    this.userDataList = r.data;
                    this.userDataList.map(user=>user.confirmPassword=user.password);
                }
            }
        );
    }

    public createUser(){
        this.isSubmit = true;
        if(this.userDataForm.invalid) return;

        let apiUrl = this.configService.apiCreateUserUrl;
        this.proxy.proxyPost(apiUrl,this.userData).subscribe(
            r=>{
                if(r.status === 200){
                    this.userDataList = r.data;
                    this.userDataList.map(user=>user.confirmPassword=user.password);
                }
                swal(r.message).then(a=>this.clearUserData());;
            },e=>{
                swal(e.message).then(a=>this.clearUserData());;
            }
        );
    }

    public updateUser(){
        this.isSubmit = true;
        this.userDataForm.controls['account'].disable();
        // this.userDataForm.controls['password'].disable();
        // this.userDataForm.controls['confirmPassword'].disable();
        if(!this.userDataForm.invalid){
            let apiUrl = this.configService.apiUpdateUserUrl;
            
            // 密碼沒改過 不用加密
            this.userData.isModifyPassword = this.userDataList.find(user=>{ return user.perSerilNo === this.userData.perSerilNo}).password !== this.userData.password

            this.proxy.proxyPost(apiUrl,this.userData).subscribe(
                r=>{
                    if(r.status === 200){
                        this.userDataList = r.data;
                        this.userDataList.map(user=>user.confirmPassword=user.password);
                    }
                    swal(r.message).then(a=>this.clearUserData());
                },e=>{
                    swal(e.message).then(a=>this.clearUserData());
                }
            );
        }

        
        this.userDataForm.controls['account'].enable();
        // this.userDataForm.controls['password'].enable();
        // this.userDataForm.controls['confirmPassword'].enable();
        // this.userDataForm.reset();
    }

    public deleteUser(index:number){
        
        let apiUrl = this.configService.apiDeleteUserUrl;

        swal({
            title: "確定要刪除？",
            icon: "warning",
            buttons: ['取消', '確定']
        })
        .then((willDelete) => {
        if (willDelete) {
            this.proxy.proxyPost(apiUrl, this.userDataList[index]).subscribe(
                r=>{
                    if(r.status === 200){
                        this.userDataList = r.data;
                        this.userDataList.map(user=>user.confirmPassword=user.password);
                    }
                    swal(r.message);
                },e=>{
                    swal(e.message);
                }
            );
        } else {
        }
        });
        
    }

}