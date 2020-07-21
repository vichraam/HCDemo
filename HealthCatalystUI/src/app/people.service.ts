import { Injectable, ɵConsole } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { People } from './people.model';
import { environment } from '../environments/environment';

@Injectable({providedIn: 'root'})
export class PeopleService{
    serviceUrl : string = environment.serviceApiEndPoint ;
    
    peoples: People[];

    constructor(private http: HttpClient) {}

    GetAllPeoples(){
        return this.http.get<People[]>(this.serviceUrl);
    }

    GetPeople(id: number){
        return this.http.get<People>
        (
            this.serviceUrl + "/" + id.toString()
        );
    }
    
    CreatePeople(people: People){
        console.log(people);
        console.log(this.serviceUrl);
        this.http.post(
            this.serviceUrl, 
            people
        )
        .subscribe(
            response => {console.log(response); }
        );
    }

    UpdatePeople(people: People){
        console.log(this.serviceUrl);
        console.log(people);
        this.http.put(
            this.serviceUrl, 
            people
        )
        .subscribe(
            response => {console.log(response); }
        );
    }

    DeletePeople(id: number){
        return this.http.delete(this.serviceUrl + "/" + id.toString());
    }
}