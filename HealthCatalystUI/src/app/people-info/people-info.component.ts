import { Component, OnInit, ViewChild, ɵConsole } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { People } from '../people.model';
import { PeopleService } from '../people.service';
import { NgForm } from '@angular/forms';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-people-info',
  templateUrl: './people-info.component.html',
  styleUrls: ['./people-info.component.css']
})
export class PeopleInfoComponent implements OnInit {
  // @ViewChild("empForm") peopleForm : NgForm;
  @ViewChild('empForm', {static: true}) peopleForm: NgForm;
  employePicturePath : File;
  people : People =
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
  constructor(private peopleService: PeopleService, private route: ActivatedRoute, private router : Router,  private toastr: ToastrService) { }

  ngOnInit() {
    console.log("test");
    console.log(this.peopleForm);
    console.log("test");
    this.id = this.route.snapshot.params['id'];
    console.log(this.route.snapshot.params);
    
    if(this.id != null){
      this.peopleService.GetPeople(this.id)
      .subscribe(
          response => {
            console.log(response); 
            this.people = response; 
            this.peopleForm.setValue({
              AddressLine1: this.people.AddressLine1,
              AddressLine2: this.people.AddressLine2,
              Age: this.people.Age,
              City: this.people.City,
              FirstName: this.people.FirstName,
              Interests: this.people.Interests,
              LastName: this.people.LastName,
              State: this.people.State,
              Zipcode: this.people.Zipcode,
              Id: this.people.Id,
              PicturePath: this.people.PicturePath
            });
          }
      );
      console.log(this.people);
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
      this.toastr.success("The people picture imported successfully");
    }
  }

  handleReaderLoaded(e) {
    this.people.PicturePath = 'data:image/png;base64,' + btoa(e.target.result);
  }

  OnSubmitForm(form: NgForm){
    console.log("on submit");
    console.log(this.people);
    console.log(this.peopleForm);

    this.people = {
      AddressLine1 : this.peopleForm.value.AddressLine1,
      AddressLine2 : this.peopleForm.value.AddressLine2,
      Age : this.peopleForm.value.Age,
      City : this.peopleForm.value.City,
      FirstName : this.peopleForm.value.FirstName,
      Interests : this.peopleForm.value.Interests,
      LastName : this.peopleForm.value.LastName,
      State : this.peopleForm.value.State,
      Zipcode : this.peopleForm.value.Zipcode,
      Id:  this.peopleForm.value.Id,
      PicturePath: this.people.PicturePath
    };
    console.log(this.people.Id);
    if(this.people.Id !== 0){
      this.peopleService.UpdatePeople(this.people);
      this.toastr.success("The changes are updated successfully");
    }
    else{
      this.peopleService.CreatePeople(this.people);
      this.toastr.success("The new people record created successfully");
    }
    
    this.router.navigate(['/peopleSearch']).then(() => {
      window.location.reload();
    });

    console.log(form);
  }
}
