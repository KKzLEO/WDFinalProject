import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
@Injectable()
export class ApiUrlConfigService {
    constructor(){}

    get apiRootUrl():string{
        return environment.rootApiUrl;
    }

    get apiAccessUrl():string{
        return this.apiRootUrl + "/access";
    }


}