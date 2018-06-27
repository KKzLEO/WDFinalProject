import { Component } from "@angular/core";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { ShoppingService } from "src/app/service/shopping.service";


@Component({
    selector: "shopping-cart-page",
    templateUrl: "./shopping-cart-page.component.html"
})


export class ShoppingCartPageComponent {

    public courseList : CourseDataModel[] = new Array<CourseDataModel>();
    public totalPrice : number = 0;

    constructor(private shoppingService:ShoppingService){

    }
    
    ngOnInit(){
        this.courseList = JSON.parse(localStorage.getItem("shopping-cart"));
        if(this.courseList == null) this.courseList = new Array<CourseDataModel>();
        this.calcTotalPrice();
    }

    public removeShoppingCartItem(index:number){
        this.courseList.splice(index,1);
        localStorage.setItem("shopping-cart",JSON.stringify(this.courseList));
        this.calcTotalPrice();
        this.shoppingService.notify();
    }

    public calcTotalPrice(){
        this.courseList.forEach(course=>{
            this.totalPrice += course.price;
        });
    }


}