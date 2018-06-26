
import { Response, Http, RequestOptions, Headers } from '@angular/http';
import { Injectable } from '@angular/core';

import { from, Observable, Subject } from 'rxjs';
import { map,catchError } from "rxjs/operators";

//核心Http服務
@Injectable()
export class ProxyCoreService {

    protected options;
    private headers:Headers;

    constructor(protected http: Http) {
        
        //預設header
        this.headers = new Headers({ 'Content-Type': "application/json" });
    }

    /**
     * get pure http get
     * 
     * @param {string} webApiUrl 
     * @returns {Observable<any>} 
     * 
     * @memberof ProxyCoreService
     */
    public get(webApiUrl: string): Observable<any> {
        return this.http.get(webApiUrl,this.getOption()).pipe(map(this.extractData),catchError(this.handleError));
    }

    /**
     * post pure http post
     * 
     * @param {string} webApiUrl 
     * @param {*} dataObj 
     * @returns {Observable<any>} 
     * 
     * @memberof ProxyCoreService
     */
    public post(webApiUrl: string, dataObj: any): Observable<any> {
        return this.http.post(webApiUrl, dataObj, this.getOption()).pipe(map(this.extractData),catchError(this.handleError));
    }

    private getOption():RequestOptions{
         return new RequestOptions({ headers: this.headers });
    }

    public getHeaders():Headers{
        return this.headers;
    }

    public setHeaders(newHeaders : Headers){
        this.headers = newHeaders;
    }

    //將資料json轉成物件
    protected extractData(res: Response) {
        let body = res.json();
        if (body == null) {
            return null;
        }
        return body || {};
    }

    protected handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        return Observable.throw(errMsg);
    }
}
