import { Injectable, ɵConsole } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Employee } from './employee.model';
import { environment } from '../environments/environment';

@Injectable({providedIn: 'root'})
export class EmployeeService{
    serviceUrl : string = environment.serviceApiEndPoint ;
    
    employees: Employee[];

    constructor(private http: HttpClient) {}

    GetAllEmployees(){
        return this.http.get<Employee[]>(this.serviceUrl);
    }

    GetEmployee(id: number){
        return this.http.get<Employee>
        (
            this.serviceUrl + "/" + id.toString()
        );
    }
    
    CreateEmployee(employee: Employee){
        console.log(employee);
        console.log(this.serviceUrl);
        this.http.post(
            this.serviceUrl, 
            employee
        )
        .subscribe(
            response => {console.log(response); }
        );
    }

    UpdateEmployee(employee: Employee){
        console.log(this.serviceUrl);
        console.log(employee);
        this.http.put(
            this.serviceUrl, 
            employee
        )
        .subscribe(
            response => {console.log(response); }
        );
    }

    DeleteEmployee(id: number){
        return this.http.delete(this.serviceUrl + "/" + id.toString());
    }
}