import { Component, OnInit, Input } from '@angular/core';
import { People } from '../../people.model';
import { PeopleService } from '../../people.service';
import { Subject } from 'rxjs';


import { Router } from '@angular/router'; 
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-people-list',
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.css']
})
export class PeopleListComponent implements OnInit {
  @Input() peopleToSearch: string;
  @Input() sortBy: Subject<string>;
  delayTimeInMilliSeconds: number = 0;

  peoples: People[] = [];
  isLoading = false;
  isError = false;
  errorMessage : string = null;
  constructor(private peopleService: PeopleService, private router: Router, private toastr: ToastrService) { }
  
  ngOnInit(){
    this.loadPeopleList();
  }

  async loadPeopleList(){
    console.log("Started load process");
    console.log(new Date());
    this.isLoading = true;
    await this.delayProcess(this.delayTimeInMilliSeconds);
    console.log("Calling WebAPI");
    console.log(new Date());

    this.peopleService.GetAllPeoples()
    .subscribe(
        response => 
        {
          console.log(response); 
          this.peoples = response;  
          this.isLoading = false;
          this.sortPeopleList();
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
    this.router.navigate(['/peopleInfo', Id.toString() ]);
  }

  OnDelete(Id: number){
    console.log(Id);
    if(confirm("Are you sure to delete the people ?"))
    {
      this.peopleService.DeletePeople(Id).subscribe(
        response => 
        {
          console. log(response); 
          this.loadPeopleList();
        }
      );

      this.toastr.success("The people deleted successfuly.");
    }
  
  }

  delayProcess(delayDuration:number){
    return new Promise(resolve => setTimeout(resolve, delayDuration));
  }

  sortPeopleList(){
    
    this.sortBy.subscribe(v => { 
      if (this.peoples.length > 0) {
        if(v.toLowerCase() === "name"){
          this.peoples = this.peoples.sort((a,b) => (a.LastName > b.LastName) ? 1: -1  );
        }
        else if(v.toLowerCase() === "age"){
          this.peoples = this.peoples.sort((a,b) => (a.Age > b.Age) ? 1: -1  );
        }
        else if(v.toLowerCase() === "city"){
          this.peoples = this.peoples.sort((a,b) => (a.City > b.City) ? 1: -1  );
        }
        else if(v.toLowerCase() === "state"){
          this.peoples = this.peoples.sort((a,b) => (a.State > b.State) ? 1: -1  );
        }
        else{
          // do nothing
        }
      }
     });
  }
}
