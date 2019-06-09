import { Component } from "@angular/core";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { ShoppingService } from "src/app/service/shopping.service";
import { Router } from "@angular/router";
import { NavigationEnd } from "@angular/router";


@Component({
    selector: "shopping-cart-page",
    templateUrl: "./shopping-cart-page.component.html"
})


export class ShoppingCartPageComponent {

    public courseList : CourseDataModel[] = new Array<CourseDataModel>();
    public totalPrice : number = 0;

    constructor(private router:Router,private shoppingService:ShoppingService){

    }
    
    ngOnInit(){
        this.courseList = JSON.parse(localStorage.getItem("shopping-cart"));
        if(this.courseList == null) this.courseList = new Array<CourseDataModel>();
        this.calcTotalPrice();
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0)
        });
    }

    public removeShoppingCartItem(index:number){
        this.courseList.splice(index,1);
        localStorage.setItem("shopping-cart",JSON.stringify(this.courseList));
        this.calcTotalPrice();
        this.shoppingService.notify();
    }

    public calcTotalPrice(){
        this.totalPrice = 0;
        this.courseList.forEach(course=>{
            this.totalPrice += course.price;
        });
    }

    public goCourseDetailPage(index:number){
        localStorage.setItem("course-detail",JSON.stringify(this.courseList[index]));
        this.router.navigateByUrl("course-detail");
    }


}