import { Pipe, PipeTransform } from '@angular/core';
import {Employee} from 'src/app/employee.model';
@Pipe({
  name: 'employeeFilter'
})
export class EmployeeFilterPipe implements PipeTransform {

  transform(employees: Employee[], employeeToSearch: string) {
    if(!employees || !employeeToSearch){
      return employees;
    }

    return employees.filter(emp => (emp.FirstName.toLowerCase().indexOf(employeeToSearch.toLowerCase()) !== -1 || emp.LastName.toLowerCase().indexOf(employeeToSearch.toLowerCase()) !== -1)); 
  }
}