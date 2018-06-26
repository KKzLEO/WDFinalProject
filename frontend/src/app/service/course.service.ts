import { Injectable } from "@angular/core";
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { MemberDataModel } from '../widget/access/member-data-model';
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import swal from 'sweetalert';
import { CourseFilterModel } from "src/app/page/course/course-filter-model";
import { CourseDataModel } from "src/app/page/course/course-data-model";
@Injectable()
export class CourseService {
    constructor(private proxy:ProxyService,private configService:ApiUrlConfigService){}
    

    public listeners = [];
    public addListener(listener:Function){
        this.listeners.push(listener);
    }

    public removeListener(listener){
        this.listeners.splice(this.listeners.indexOf(listener), 1);
    }

    public notify(message?) {
		this.listeners.forEach(listener => {
			listener(message);
		})
	}

    public searchCourse(filterArg:CourseFilterModel){
        let apiUrl = this.configService.apiCourseUrl + "/search";
        this.proxy.post(apiUrl,filterArg).subscribe(
            r =>{
                let result : CourseDataModel = r.data;
                this.notify(result);
            },
            e =>{
                swal({title:"系統錯誤",icon:"warning"});
            }
        );

    }
}