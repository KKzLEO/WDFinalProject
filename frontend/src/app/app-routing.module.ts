import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexPageComponent } from 'src/app/page/index/index-page.component';
import { TeachersPageComponent } from 'src/app/page/teachers/teachers-page.component';
import { ShoppingCartPageComponent } from 'src/app/page/shopping-cart/shopping-cart-page.component';
import { CoursePageComponent } from 'src/app/page/course/course-page.component';
import { MaintainPageComponent } from 'src/app/page/maintain/maintain-page.component';
import { AuthGuardService } from 'src/app/service/authguard.service';
import { MaintainUserPageComponent } from 'src/app/page/maintain/maintain-user-page.component';

const routes: Routes = [
    { path: 'index', pathMatch: 'full', component: IndexPageComponent },
    { path: 'teachers', pathMatch: 'full', component: TeachersPageComponent },
    { path: 'shopping-cart', pathMatch: 'full', component: ShoppingCartPageComponent },
    { path: 'course-detail', pathMatch: 'full', component: CoursePageComponent },
    // { path: 'maintain', pathMatch: 'full', component: MaintainPageComponent},
    // { path: 'maintain-user', pathMatch: 'full', component: MaintainUserPageComponent},
    { path: 'maintain', pathMatch: 'full', component: MaintainPageComponent,canActivate: [AuthGuardService] },
    { path: 'maintain-user', pathMatch: 'full', component: MaintainUserPageComponent,canActivate: [AuthGuardService]},
    // { path: 'loginfail', pathMatch: 'full', component: LoginfailPageComponent },
    // { path: 'about', pathMatch: 'full', component: AboutPageComponent },
    // { path: 'error', pathMatch: 'full', component: ErrorPageComponent },
    { path: '', redirectTo: '/index', pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule],
    providers: [],
})
export class AppRoutingModule { }
