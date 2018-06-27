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
    selector: "customer-footer",
    templateUrl: "./customer-footer.component.html"
})


export class CustomerFooterComponent{
    
}