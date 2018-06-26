import { Component } from "@angular/core";
import { AccessComponent } from "../../widget/access/access.component";
import { ViewChild } from "@angular/core";


@Component({
    selector: "index-page",
    templateUrl: "./index-page.component.html"
})


export class IndexPageComponent{

    @ViewChild("accessComponent") accessComponent : AccessComponent;

    ngOnInit(){
        
    }
    
    public register(){
        this.accessComponent.openRegisterWindow();
    }
        
    public callBackend(){
        // call backend and get data

    }
}