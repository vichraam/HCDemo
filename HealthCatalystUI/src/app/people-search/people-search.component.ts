import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-people-search',
  templateUrl: './people-search.component.html',
  styleUrls: ['./people-search.component.css']
})
export class PeopleSearchComponent implements OnInit {
  peopleToSearch: string;
  sortBy: Subject<string> = new Subject();
  constructor() { }

  ngOnInit(){
  }
  selectChangeHandler(event: any){
    this.sortBy.next(event.target.value);
  }
}
