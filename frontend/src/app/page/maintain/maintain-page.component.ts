import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NavigationEnd } from "@angular/router";
import { CourseService } from "src/app/service/course.service";
import { CourseDataModel } from "src/app/page/course/course-data-model";
import { CourseFilterModel } from "src/app/page/course/course-filter-model";
import { ProxyService } from "src/app/proxy/proxy.service";
import { ApiUrlConfigService } from "src/app/config/api-url-config.service";
import { ViewChild } from "@angular/core";
import { ElementRef } from "@angular/core";
import '../../../assets/js/uikit.min.js';
import { CodeDataModel } from "src/app/model/code.data.model";
import { InputStatus } from "src/app/enum/input.status";
import { 
    ValidatorFn,
    Validator, 
    AbstractControl, 
    FormControl, 
    Validators,
    FormGroup
} from '@angular/forms';
import { Http,RequestOptions,Headers } from "@angular/http";
declare var UIkit : any;


@Component({
    selector: "maintain-page",
    templateUrl: "./maintain-page.component.html"
})

export class MaintainPageComponent {
    constructor(private http: Http,private configService:ApiUrlConfigService,private router:Router,private courseService:CourseService,private proxy:ProxyService){
        this.courseService.addListener(this.getAllData.bind(this));
    }

    public courseList : CourseDataModel[] = new Array<CourseDataModel>();
    public newCourseData : CourseDataModel = new CourseDataModel();
    public courseCategoryList : CodeDataModel[] = new Array<CodeDataModel>();
    public teacherList : CodeDataModel[] = new Array<CodeDataModel>();
    public src : string;
    public op : string = "create";
    public lastOp : string = "create";
    public newCourseForm = new FormGroup({
        courseName: new FormControl('',Validators.required),
        // courseImage: new FormControl('',Validators.required),
        courseCategory: new FormControl('',Validators.required),
        courseTeacher: new FormControl('',Validators.required),
        coursePrice: new FormControl('',Validators.required),
        courseDesc: new FormControl('',Validators.required),
        courseLearningTarget: new FormControl('',Validators.required),
        courseRequirement: new FormControl('',Validators.required),
        courseIns: new FormControl('',Validators.required),
        courseTarget: new FormControl('',Validators.required),
        courseShortIntro: new FormControl('',Validators.required),
    });
    public isSubmit:boolean = false;
    @ViewChild("createCourseWindow") createCourseWindow : ElementRef;
    @ViewChild("uploadProgressBar") uploadProgressBar : ElementRef;

    ngOnInit(){
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0)
        });
        this.courseService.searchCourse(new CourseFilterModel());
        this.wrapCodeData();
    }

    public wrapCodeData(){
        this.proxy.get(this.configService.apiGetCourseCatgory).subscribe(
            r=>{
                this.courseCategoryList = r.data;
            }
        );
        this.proxy.get(this.configService.apiGetTeacherList).subscribe(
            r=>{
                this.teacherList = r.data;
            }
        );
    }

    public showImage($event){
        const file: File = $event.target.files[0];
        this.newCourseData.courseImageFile = file;
        const reader = new FileReader();
        reader.addEventListener('load', (event: any) => {
            this.src = event.target.result;
        });
    
        reader.readAsDataURL(file);
    }
    
    public openWindow(opertaion:string,index?){
        this.op = opertaion;
        if(this.op=="create" && this.op == this.lastOp){
            this.openCreateCourseWindow();
        }else if(this.op=="create" && this.lastOp=="update"){
            this.clearCourseFormData();
            this.openCreateCourseWindow();
        }else if(this.op=="update" && this.lastOp=="update"){
            this.openUpdateCourseWindow(index);
        }else if(this.op=="update" && this.lastOp=="create"){
            this.clearCourseFormData();
            this.openUpdateCourseWindow(index);
        }

        this.lastOp = this.op;
    }

    public openCreateCourseWindow(){
        UIkit.modal(this.createCourseWindow.nativeElement).show();
    }

    public closeCreateCourseWindow(){
        UIkit.modal(this.createCourseWindow.nativeElement).close();
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

    public createCourse(){
        this.isSubmit = true;
        if(this.newCourseForm.invalid) return;
        const formData: FormData = new FormData();
        formData.append('file', this.newCourseData.courseImageFile);
        this.proxy.setProxyHeaderNew();
        this.proxy.proxyPost(this.configService.apiUploadImageUrl,formData).subscribe(
            r=>{
                this.newCourseData.courseImageName = r.message;
                this.proxy.setProxyHeaderJson();
                this.proxy.proxyPost(this.configService.apiCreateCourseUrl,this.newCourseData).subscribe(
                    r=>{
                        this.courseList = r.data;
                        this.clearCourseFormData();
                        swal("新增成功");
                    },e=>{
                        swal("新增失敗");
                    }
                );
            },e=>{
                swal("新增失敗");
            }
        )
    }

    public deleteCourse(courseSerilNo:string){
        let data = new CourseDataModel();
        data.courseSerilNo = courseSerilNo;
        swal({
            title: "確定要刪除？",
            icon: "warning",
            buttons: ['取消', '確定']
        })
        .then((willDelete) => {
        if (willDelete) {
            this.proxy.proxyPost(this.configService.apiDeleteCourseUrl,data).subscribe(
                r=>{
                    this.courseList = r.data;
                    swal(r.message);
                },e=>{
                    swal("刪除失敗");
                }
            );
        } else {
        }
        });
        
    }

    public updateCourse(){
        this.isSubmit = true;
        if(this.newCourseForm.invalid) return;
        // 沒有更新圖片，直接送API
        console.log(this.newCourseData.courseImageFile);
        if(this.newCourseData.courseImageFile == null || this.newCourseData.courseImageFile == undefined){
            console.log("沒");
            this.proxy.proxyPost(this.configService.apiUpdateCourseUrl,this.newCourseData).subscribe(
                r=>{
                    this.courseList = r.data;
                    swal("更新成功");
                },e=>{
                    swal("更新失敗");
                }
            );
        }else{
            const formData: FormData = new FormData();
            formData.append('file', this.newCourseData.courseImageFile);
            this.proxy.setProxyHeaderNew();
            this.proxy.proxyPost(this.configService.apiUploadImageUrl,formData).subscribe(
                r=>{
                    this.newCourseData.courseImageName = r.message;
                    this.proxy.setProxyHeaderJson();
                    this.proxy.proxyPost(this.configService.apiUpdateCourseUrl,this.newCourseData).subscribe(
                        r=>{
                            this.courseList = r.data;
                            this.clearCourseFormData();
                            swal("更新成功");
                        },e=>{
                            swal("更新失敗");
                        }
                    );
                },e=>{
                    swal("更新失敗");
                }
            )
        }
    }

    public openUpdateCourseWindow(index){
        // this.clearCourseFormData();
        this.newCourseData = this.courseList[index];
        this.src = this.configService.apiGetCourseImageUrl + this.newCourseData.courseImageName;
        this.openCreateCourseWindow();
    }


    public clearCourseFormData(){
        this.newCourseData = new CourseDataModel();
        this.src = null;
        this.isSubmit = false;
        this.newCourseForm.reset();
    }
}