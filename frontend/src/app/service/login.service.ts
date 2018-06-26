import { Injectable } from "@angular/core";
import { Subject } from 'rxjs';
import { LoginDataModel } from '../widget/login/login-data-model';

@Injectable()
export class LoginService {
    public subject = new Subject();

    public subscribe(observable){
        this.subject.subscribe(observable);
    }

    public cancelSubscribe(observable){
        this.subject.unsubscribe();
    }
}