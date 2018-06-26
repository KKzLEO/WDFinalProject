import { Component } from "@angular/core";
import { AccessComponent } from "../../widget/access/access.component";
import { ViewChild } from "@angular/core";
import { CourseFilterModel } from "src/app/page/course/course-filter-model";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { CourseService } from "src/app/service/course.service";


@Component({
    selector: "index-page",
    templateUrl: "./index-page.component.html"
})


export class IndexPageComponent{

    public courseFilterArg : CourseFilterModel;
    public courseList : CourseDataModel[] = new Array<CourseDataModel>();
    constructor(private courseService:CourseService,private proxy:ProxyService,private configService:ApiUrlConfigService){
        this.courseService.addListener(this.courseServiceReceiver.bind(this));
    }

    @ViewChild("accessComponent") accessComponent : AccessComponent;

    ngOnInit(){
        
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
}