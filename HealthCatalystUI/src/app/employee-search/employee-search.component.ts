import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-employee-search',
  templateUrl: './employee-search.component.html',
  styleUrls: ['./employee-search.component.css']
})
export class EmployeeSearchComponent implements OnInit {
  employeeToSearch: string;
  sortBy: Subject<string> = new Subject();
  constructor() { }

  ngOnInit(){
  }
  selectChangeHandler(event: any){
    this.sortBy.next(event.target.value);
  }
}
