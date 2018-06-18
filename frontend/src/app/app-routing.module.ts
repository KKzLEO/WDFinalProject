import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexPageComponent } from 'src/app/page/index/index-page.component';
import { TeachersPageComponent } from 'src/app/page/teachers/teachers-page.component';
import { ShoppingCartPageComponent } from 'src/app/page/shopping-cart/shopping-cart-page.component';

// import { DashboardContainerComponent } from './dashboard/dashboard-container.component';
// import { AppSpaComponent } from './app-spa.component';
// import { SampleControlComponent } from './sample/sample-control.component';
// import { LoginComponent } from './login/login.component';
// import { AuthGuardService } from './login/authguard.service';
// import { AuthService } from './login/auth.service';
// import { AboutPageComponent } from './page/about-page.component';
// import { ErrorPageComponent } from './page/error-page.component';
// import { LoginfailPageComponent } from './page/loginfail-page.component';
// import { SampleMessageComponent } from './sample/sample-message.component';
// import { SampleAdvComponent } from './sample/sample-adv.component';
// import { SampleStandardmatainComponent } from './sample/sample-standardmatain.component';
// import { SampleStandardFilterComponent } from './sample/sample-standardfilter.component';
// import { SampleStandardExecuteComponent } from './sample/sample-standardexecute.component';
// import { SampleStandardmatainSimpleComponent } from './sample/sample-standardmatain-simple.component';
// import { OperationOverViewComponent } from './powerbi/operation-overview.component';
// import { OperationManPowerComponent } from './powerbi/operation-manpower.component';
// import { OperationMonthlyReportComponent } from './powerbi/operation-monthlyreport.component';
// import { OperationTestReportComponent } from './powerbi/operation-testreport.component';
// const falbackRoute:Routes=[
//         {path:'**', component:DashboardContainerComponent}
//     ]

const routes: Routes = [
    { path: 'index', pathMatch: 'full', component: IndexPageComponent },
    { path: 'teachers', pathMatch: 'full', component: TeachersPageComponent },
    { path: 'shopping-cart', pathMatch: 'full', component: ShoppingCartPageComponent },
    // { path: 'loginfail', pathMatch: 'full', component: LoginfailPageComponent },
    // { path: 'about', pathMatch: 'full', component: AboutPageComponent },
    // { path: 'error', pathMatch: 'full', component: ErrorPageComponent },
    { path: '', redirectTo: '/index', pathMatch: 'full' },
    // {
    //     path: 'spa',
    //     component: AppSpaComponent,
    //     children: [
    //         { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    //         { path: 'dashboard', component: DashboardContainerComponent, canActivate: [AuthGuardService] },
    //         { path: 'sample', component: SampleControlComponent, canActivate: [AuthGuardService] },
    //         { path: 'samplemsg', component: SampleMessageComponent, canActivate: [AuthGuardService] },
    //         { path: 'sampleadv', component: SampleAdvComponent, canActivate: [AuthGuardService] },
    //         { path: 'samplestandardmatain', component: SampleStandardmatainComponent, canActivate: [AuthGuardService] },
    //         { path: 'samplestandardmatainsimple', component: SampleStandardmatainSimpleComponent, canActivate: [AuthGuardService] },
    //         { path: 'samplestandardfilter', component: SampleStandardFilterComponent, canActivate: [AuthGuardService] },
    //         { path: 'samplestandardexecute', component: SampleStandardExecuteComponent, canActivate: [AuthGuardService] },
    //         { path: 'operationoverview', component: OperationOverViewComponent, canActivate: [AuthGuardService] },
    //         { path: 'operationmanpower', component: OperationManPowerComponent, canActivate: [AuthGuardService] },
    //         { path: 'operationmonthlyreport', component: OperationMonthlyReportComponent, canActivate: [AuthGuardService] },
    //         { path: 'operationtestreport', component: OperationTestReportComponent, canActivate: [AuthGuardService] }      
    //     ], canActivate: [AuthGuardService]

    // }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule],
    providers: [],
})
export class AppRoutingModule { }
