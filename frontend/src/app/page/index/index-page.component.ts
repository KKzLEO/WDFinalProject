import { Component } from "@angular/core";
import { LoginComponent } from "../../widget/login/login.component";
import { ViewChild } from "@angular/core";


@Component({
    selector: "index-page",
    templateUrl: "./index-page.component.html"
})


export class IndexPageComponent{

    @ViewChild("loginComponent") loginComponent : LoginComponent;

    ngOnInit(){
        
    }
    
    public register(){
    }
        
    public callBackend(){
        // call backend and get data

    }
}