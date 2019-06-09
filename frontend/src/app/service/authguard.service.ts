import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';
import { Observable } from "rxjs";
import { ApiUrlConfigService } from 'src/app/config/api-url-config.service';
import { ProxyService } from 'src/app/proxy/proxy.service';
@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private proxy:ProxyService,private config:ApiUrlConfigService) { }

    canActivate(route:ActivatedRouteSnapshot, state:RouterStateSnapshot):Observable<boolean>|boolean {

        return new Observable(m => {
                this.proxy.proxyPost(this.config.validateTokenUrl,{}).subscribe(
                    r => {
                        if(r.status == '200'){
                            m.next(true);
                            m.complete();
                        }else{
                            m.next(false);
                            m.complete();
                        }
                    },
                    e => {
                        m.next(false);
                        m.complete();
                    }
                )
            });
 
    }

}