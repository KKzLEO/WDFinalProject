import { Injectable } from "@angular/core";
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { MemberDataModel } from '../widget/access/member-data-model';
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import swal from 'sweetalert';
@Injectable()
export class ShoppingService {
    constructor(private proxy:ProxyService,private configService:ApiUrlConfigService){}
    

    public listeners = [];
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
}