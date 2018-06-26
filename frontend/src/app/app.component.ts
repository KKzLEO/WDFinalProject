import { Component } from '@angular/core';
import { ViewChild } from '@angular/core';
import { LoginComponent } from 'src/app/widget/login/login.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    @ViewChild("loginComponent") loginComponent : LoginComponent;
    
    public openLoginWindow(){
        this.loginComponent.openLoginWindow();
    }

}
