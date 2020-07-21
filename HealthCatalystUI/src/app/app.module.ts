import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { PeopleSearchComponent } from './people-search/people-search.component'; 
import { PeopleListComponent } from './people-search/people-list/people-list.component';
import { PeopleFilterPipe } from './pipe/people-filter.pipe';
import { PeopleInfoComponent } from './people-info/people-info.component';
import { HomeComponent } from './home/home.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
 

const appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'peopleSearch', component: PeopleSearchComponent},
  {path: 'peopleInfo', component: PeopleInfoComponent},
  {path: 'peopleInfo/:id', component: PeopleInfoComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    PeopleSearchComponent,
    PeopleListComponent,
    PeopleFilterPipe,
    PeopleInfoComponent,
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
