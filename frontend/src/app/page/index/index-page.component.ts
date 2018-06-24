import { Component } from "@angular/core";



@Component({
    selector: "index-page",
    templateUrl: "./index-page.component.html"
})


export class IndexPageComponent{
    
    public test : string[] = ["dfdsf","sdfsdfsdf"]
    public testIf : boolean = true;
    ngOnInit(){
        
    }

    public callBackend(){
        // call backend and get data

    }
}