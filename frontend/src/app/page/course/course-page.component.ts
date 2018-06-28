import { Component } from "@angular/core";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { ShoppingService } from "src/app/service/shopping.service";


@Component({
    selector: "course-page",
    templateUrl: "./course-page.component.html",
    styleUrls: ["./course-page.css"]
})

export class CoursePageComponent {
    constructor(private shoppingService:ShoppingService){}

    public courseData : CourseDataModel= new CourseDataModel();
    public learningText : string[] = [];
    ngOnInit(){
        this.courseData = JSON.parse(localStorage.getItem("course-detail"));
        if(this.courseData == null) this.courseData = new CourseDataModel();
        this.processCourseData();
    }

    public processCourseData(){
        this.learningText = this.courseData.learningText.split(";");
    }

    public addToShoppingCart(){
        if(localStorage.getItem("shopping-cart") == null){
            let shoppingCartCourses : CourseDataModel[] = new Array<CourseDataModel>();
            shoppingCartCourses.push(this.courseData);
            localStorage.setItem("shopping-cart",JSON.stringify(shoppingCartCourses));
            swal({title:"加入成功",icon:"success",text:"請至購物車查看"});
        }else{
            let shoppingCartCourses : CourseDataModel[] = new Array<CourseDataModel>();
            let isExist : boolean = false;
            shoppingCartCourses = JSON.parse(localStorage.getItem("shopping-cart"));
            shoppingCartCourses.forEach(course=>{
                if(!isExist){
                    if(course.courseSerilNo == this.courseData.courseSerilNo){
                        isExist = true;
                    }
                }
            });
            if(isExist){
                swal({title:"購物車已有相同課程囉！",icon:"warning",text:"請至購物車查看"});
            }else{
                shoppingCartCourses.push(this.courseData);
                localStorage.setItem("shopping-cart",JSON.stringify(shoppingCartCourses));
                swal({title:"加入成功",icon:"success",text:"請至購物車查看"});
            }   
        }
        this.shoppingService.notify();
    }

    
}