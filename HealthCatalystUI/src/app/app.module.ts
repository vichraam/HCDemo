import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { EmployeeSearchComponent } from './employee-search/employee-search.component';
import { EmployeeListComponent } from './employee-search/employee-list/employee-list.component';
import { EmployeeFilterPipe } from './pipe/employee-filter.pipe';
import { EmployeeInfoComponent } from './employee-info/employee-info.component';
import { HomeComponent } from './home/home.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
 

const appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'employeeSearch', component: EmployeeSearchComponent},
  {path: 'employeeInfo', component: EmployeeInfoComponent},
  {path: 'employeeInfo/:id', component: EmployeeInfoComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    EmployeeSearchComponent,
    EmployeeListComponent,
    EmployeeFilterPipe,
    EmployeeInfoComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule, 
    FormsModule, 
    HttpClientModule, 
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  // providers: [{provide: HTTP_INTERCEPTORS, useClass: CorsInterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
