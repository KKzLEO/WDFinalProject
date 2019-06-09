import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
@Injectable()
export class ApiUrlConfigService {
    constructor(){}

    get apiRootUrl():string{
        return environment.rootApiUrl;
    }

    get apiAccessUrl():string{
        return this.apiRootUrl + "/access";
    }

    get apiCourseUrl():string{
        return this.apiRootUrl + "/course";
    }

    get apiCreateCourseUrl():string{
        return this.apiCourseUrl + "/create";
    }

    get apiUpdateCourseUrl():string{
        return this.apiCourseUrl + "/update";
    }

    get apiDeleteCourseUrl():string{
        return this.apiCourseUrl + "/delete";
    }

    get apiCodeUrl():string{
        return this.apiRootUrl + "/code";
    }

    get apiGetCourseImageUrl():string{
        return this.apiCourseUrl + "/image/";
    }

    get apiUploadImageUrl():string{
        return this.apiCourseUrl + "/uploadimage";
    }

    get validateTokenUrl():string{
        return this.apiRootUrl + "/access/validatetoken";
    }

    get apiGetCourseCatgory(){
        return this.apiCodeUrl + "/coursecategory";
    }

    get apiGetTeacherList(){
        return this.apiCodeUrl + "/teacherlist";
    }
}