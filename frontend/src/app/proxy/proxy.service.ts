
import { Headers, Http, RequestOptions, ResponseContentType } from '@angular/http';
import { ProxyCoreService } from './proxy-core.service';
import { Injectable } from '@angular/core';


import { from, Observable, Subject } from 'rxjs';
import { map, catchError } from "rxjs/operators";

//有access的Http服務
@Injectable()
export class ProxyService extends ProxyCoreService {
    private proxyHeaders: Headers;
    constructor(http: Http) {
        super(http);
        //預設header
        this.proxyHeaders = new Headers({ 'Content-Type': "application/json"
    
    });
    }

    /**
     * proxyGet http get to api with token
     * 
     * @param {string} webApiUrl 
     * @returns {Observable<any>} 
     * 
     * @memberof ProxyService
     */
    public proxyGet(webApiUrl: string): Observable<any> {
        return this.http.get(webApiUrl, this.getProxyOption()).pipe(map(this.extractData),catchError(this.handleError));
    }

    /**
     * proxyPost http post to api with token
     * 
     * @param {string} webApiUrl 
     * @param {*} dataObj 
     * @returns {Observable<any>} 
     * 
     * @memberof ProxyService
     */
    public proxyPost(webApiUrl: string, dataObj: any): Observable<any> {
        return this.http.post(webApiUrl, dataObj, this.getProxyOption()).pipe(map(this.extractData),catchError(this.handleError));
    }

    private getProxyOption(): RequestOptions {
        return new RequestOptions({ headers: this.proxyHeaders });
    }

    public setProxyHeaderNew(){
        this.proxyHeaders = new Headers();
    }

    public setProxyHeadersMutiPart() {
        this.proxyHeaders = new Headers({
            'Content-Type': "multipart/form-data"
        });
    }

    public setProxyHeaderJson(){
        this.proxyHeaders = new Headers({
            'Content-Type': "application/json"
        });
    }

}
