import { Component } from "@angular/core";
import { AccessComponent } from "../../widget/access/access.component";
import { ViewChild } from "@angular/core";
import { CourseFilterModel } from "src/app/page/course/course-filter-model";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { CourseService } from "src/app/service/course.service";
import { ShoppingService } from "src/app/service/shopping.service";
import { Router } from "@angular/router";
import { NavigationEnd } from "@angular/router";


@Component({
    selector: "index-page",
    templateUrl: "./index-page.component.html",
    styleUrls: ["./index-page.css"]
})


export class IndexPageComponent{

    public courseFilterArg : CourseFilterModel;
    public courseList : CourseDataModel[] = new Array<CourseDataModel>();
    constructor(private router:Router,private shoppingService:ShoppingService,private courseService:CourseService,private proxy:ProxyService,private configService:ApiUrlConfigService){
        this.courseService.addListener(this.courseServiceReceiver.bind(this));
    }

    @ViewChild("accessComponent") accessComponent : AccessComponent;

    ngOnInit(){
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0)
        });
    }
    
    public register(){
        this.accessComponent.openRegisterWindow();
    }

    public courseServiceReceiver(courseList : Array<CourseDataModel>){
        this.courseList = courseList;
        this.courseList.forEach(course=>{
            course.courseImageUrl = this.configService.apiGetCourseImageUrl + course.courseImageName;
        });
    }

    public addCourseToShoppingCart(index:number){
        if(localStorage.getItem("shopping-cart") == null){
            let shoppingCartCourses : CourseDataModel[] = new Array<CourseDataModel>();
            shoppingCartCourses.push(this.courseList[index]);
            localStorage.setItem("shopping-cart",JSON.stringify(shoppingCartCourses));
            swal({title:"加入成功",icon:"success",text:"請至購物車查看"});
        }else{
            let shoppingCartCourses : CourseDataModel[] = new Array<CourseDataModel>();
            let isExist : boolean = false;
            shoppingCartCourses = JSON.parse(localStorage.getItem("shopping-cart"));
            shoppingCartCourses.forEach(course=>{
                if(!isExist){
                    if(course.courseSerilNo == this.courseList[index].courseSerilNo){
                        isExist = true;
                    }
                }
            });
            if(isExist){
                swal({title:"購物車已有相同課程囉！",icon:"warning",text:"請至購物車查看"});
            }else{
                shoppingCartCourses.push(this.courseList[index]);
                localStorage.setItem("shopping-cart",JSON.stringify(shoppingCartCourses));
                swal({title:"加入成功",icon:"success",text:"請至購物車查看"});
            }   
        }
        this.shoppingService.notify();
    }

    public goCourseDetailPage(index:number){
        localStorage.setItem("course-detail",JSON.stringify(this.courseList[index]));
        this.router.navigateByUrl("course-detail");
    }

    public watchAllCourse(){
        var courseView = document.getElementById("CourseList");
        if(courseView != null) courseView.scrollIntoView();
    }

}