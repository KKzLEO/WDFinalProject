import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NavigationEnd } from "@angular/router";
import { CourseService } from "src/app/service/course.service";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { CourseFilterModel } from "src/app/page/course/course-filter-model";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";


@Component({
    selector: "maintain-page",
    templateUrl: "./maintain-page.component.html"
})

export class MaintainPageComponent {
    constructor(private configService:ApiUrlConfigService,private router:Router,private courseService:CourseService,private proxy:ProxyService){
        this.courseService.addListener(this.getAllData.bind(this));
    }

    public courseList : CourseDataModel[] = new Array<CourseDataModel>();

    ngOnInit(){
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0)
        });
        this.courseService.searchCourse(new CourseFilterModel());
    }
    

    public getAllData(courseList: Array<CourseDataModel>){
        this.courseList = courseList;
    }

    public removeCourse(courseSerilNo:string){
        let apiUrl = this.configService.apiCourseUrl + '/delete';
        this.proxy.post(apiUrl,{courseSerilNo:courseSerilNo}).subscribe(
            r=>{
                this.courseService.searchCourse(new CourseFilterModel());
            },e=>{
                
            }
        );

    }
}