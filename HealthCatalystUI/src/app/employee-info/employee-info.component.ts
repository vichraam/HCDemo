import { Component, OnInit, ViewChild, ɵConsole } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../employee.model';
import { EmployeeService } from '../employee.service';
import { NgForm } from '@angular/forms';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-info',
  templateUrl: './employee-info.component.html',
  styleUrls: ['./employee-info.component.css']
})
export class EmployeeInfoComponent implements OnInit {
  // @ViewChild("empForm") employeeForm : NgForm;
  @ViewChild('empForm', {static: true}) employeeForm: NgForm;
  employePicturePath : File;
  employee : Employee =
  {
      AddressLine1 : '',
      AddressLine2 : '',
      Age : 0,
      City : '',
      FirstName : '',
      Interests : '',
      LastName : '',
      State : '',
      Zipcode : '',
      Id: 0,
      PicturePath: ''
    };
  id: number;
  constructor(private employeeService: EmployeeService, private route: ActivatedRoute, private router : Router,  private toastr: ToastrService) { }

  ngOnInit() {
    console.log("test");
    console.log(this.employeeForm);
    console.log("test");
    this.id = this.route.snapshot.params['id'];
    console.log(this.route.snapshot.params);
    
    if(this.id != null){
      this.employeeService.GetEmployee(this.id)
      .subscribe(
          response => {
            console.log(response); 
            this.employee = response; 
            this.employeeForm.setValue({
              AddressLine1: this.employee.AddressLine1,
              AddressLine2: this.employee.AddressLine2,
              Age: this.employee.Age,
              City: this.employee.City,
              FirstName: this.employee.FirstName,
              Interests: this.employee.Interests,
              LastName: this.employee.LastName,
              State: this.employee.State,
              Zipcode: this.employee.Zipcode,
              Id: this.employee.Id,
              PicturePath: this.employee.PicturePath
            });
          }
      );
      console.log(this.employee);
    }
  }

  onFileSelected(event){
    console.log("inside file selected");
    console.log(event);
    this.employePicturePath = event.target.files[0];
    console.log(this.employePicturePath);

    if(this.employePicturePath){
      const reader = new FileReader();
      reader.onload = this.handleReaderLoaded.bind(this);
      reader.readAsBinaryString(this.employePicturePath );
      this.toastr.success("The employee picture imported successfully");
    }
  }

  handleReaderLoaded(e) {
    this.employee.PicturePath = 'data:image/png;base64,' + btoa(e.target.result);
  }

  OnSubmitForm(form: NgForm){
    console.log("on submit");
    console.log(this.employee);
    console.log(this.employeeForm);

    this.employee = {
      AddressLine1 : this.employeeForm.value.AddressLine1,
      AddressLine2 : this.employeeForm.value.AddressLine2,
      Age : this.employeeForm.value.Age,
      City : this.employeeForm.value.City,
      FirstName : this.employeeForm.value.FirstName,
      Interests : this.employeeForm.value.Interests,
      LastName : this.employeeForm.value.LastName,
      State : this.employeeForm.value.State,
      Zipcode : this.employeeForm.value.Zipcode,
      Id:  this.employeeForm.value.Id,
      PicturePath: this.employee.PicturePath
    };
    console.log(this.employee.Id);
    if(this.employee.Id !== 0){
      this.employeeService.UpdateEmployee(this.employee);
      this.toastr.success("The changes are updated successfully");
    }
    else{
      this.employeeService.CreateEmployee(this.employee);
      this.toastr.success("The new employee record created successfully");
    }
    
    this.router.navigate(['/employeeSearch']).then(() => {
      window.location.reload();
    });

    console.log(form);
  }
}
