import { Pipe, PipeTransform } from '@angular/core';
import {People} from 'src/app/people.model';
@Pipe({
  name: 'peopleFilter'
})
export class PeopleFilterPipe implements PipeTransform {

  transform(peoples: People[], peopleToSearch: string) {
    if(!peoples || !peopleToSearch){
      return peoples;
    }

    return peoples.filter(emp => (emp.FirstName.toLowerCase().indexOf(peopleToSearch.toLowerCase()) !== -1 || emp.LastName.toLowerCase().indexOf(peopleToSearch.toLowerCase()) !== -1)); 
  }
}