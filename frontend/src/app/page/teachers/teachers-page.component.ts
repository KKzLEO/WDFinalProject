import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NavigationEnd } from "@angular/router";
import { UserService } from "src/app/service/user.service";
import { ViewChild } from "@angular/core";
import { AccessComponent } from "src/app/widget/access/access.component";


@Component({
    selector: "teachers-page",
    templateUrl: "./teachers-page.component.html",
    styleUrls: ['./teachers-page.css']
})

export class TeachersPageComponent {
    
    constructor(private router:Router,private userService:UserService){}
    @ViewChild("accessComponent") accessComponent : AccessComponent;
    ngOnInit(){
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0)
        });
    }

    public register(){
        this.accessComponent.openRegisterWindow();
    }

}