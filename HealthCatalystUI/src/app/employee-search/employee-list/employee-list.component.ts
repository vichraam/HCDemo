import { Component, OnInit, Input } from '@angular/core';
import { Employee } from '../../employee.model';
import { EmployeeService } from '../../employee.service';
import { Subject } from 'rxjs';


import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  @Input() employeeToSearch: string;
  @Input() sortBy: Subject<string>;
  delayTimeInMilliSeconds: number = 0;

  employees: Employee[] = [];
  isLoading = false;
  isError = false;
  errorMessage : string = null;
  constructor(private employeeService: EmployeeService, private router: Router, private toastr: ToastrService) { }
  
  ngOnInit(){
    this.loadEmployeeList();
  }

  async loadEmployeeList(){
    console.log("Started load process");
    console.log(new Date());
    this.isLoading = true;
    await this.delayProcess(this.delayTimeInMilliSeconds);
    console.log("Calling WebAPI");
    console.log(new Date());

    this.employeeService.GetAllEmployees()
    .subscribe(
        response => 
        {
          console.log(response); 
          this.employees = response;  
          this.isLoading = false;
          this.sortEmployeeList();
          console.log("End Load process");
          console.log(new Date());
        },
        error  => {
          this.isError = true;
          this.errorMessage = error.error.errorMessage;
          console.log(error);
          
        }
    );
  }

  OnClick(Id: number){
    console.log(Id);
    this.router.navigate(['/employeeInfo', Id.toString() ]);
  }

  OnDelete(Id: number){
    console.log(Id);
    if(confirm("Are you sure to delete the employee ?"))
    {
      this.employeeService.DeleteEmployee(Id).subscribe(
        response => 
        {
          console. log(response); 
          this.loadEmployeeList();
        }
      );

      this.toastr.success("The employee deleted successfuly.");
    }
  
  }

  delayProcess(delayDuration:number){
    return new Promise(resolve => setTimeout(resolve, delayDuration));
  }

  sortEmployeeList(){
    
    this.sortBy.subscribe(v => { 
      if (this.employees.length > 0) {
        if(v.toLowerCase() === "name"){
          this.employees = this.employees.sort((a,b) => (a.LastName > b.LastName) ? 1: -1  );
        }
        else if(v.toLowerCase() === "age"){
          this.employees = this.employees.sort((a,b) => (a.Age > b.Age) ? 1: -1  );
        }
        else if(v.toLowerCase() === "city"){
          this.employees = this.employees.sort((a,b) => (a.City > b.City) ? 1: -1  );
        }
        else if(v.toLowerCase() === "state"){
          this.employees = this.employees.sort((a,b) => (a.State > b.State) ? 1: -1  );
        }
        else{
          // do nothing
        }
      }
     });
  }
}
