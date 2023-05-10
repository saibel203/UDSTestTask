import { Component } from '@angular/core';
import { IEmployee } from 'src/app/models/IEmployee.interface';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-window',
  templateUrl: './employee-window.component.html',
  styleUrls: ['./employee-window.component.scss']
})
export class EmployeeWindowComponent {
  constructor(private employeeService: EmployeeService) {}

  employees?: IEmployee[];

  ngOnInit(): void {
    this.allEmployees();
  }

  allEmployees() {
    this.employeeService.getAllEmployees().subscribe(
      (employeesData: IEmployee[]) => {
        this.employees = employeesData;
        console.log(employeesData);
      }
    );
  }

  getEmployeeById(employeeId: number) {
    this.employeeService.getEmployeeById(employeeId).subscribe(
      (employeeData: IEmployee) => {
        console.log(employeeData);
      }
    );
  }

  newEmployee(employee: IEmployee) {
    this.employeeService.createEmployee(employee).subscribe(
      (employeeData: IEmployee) => {
        console.log(employeeData);
      }
    );
  }

  removeEmployee(employeeId: number) {
    this.employeeService.removeEmployee(employeeId).subscribe();
  }

  refreshEmployeeData(employeeData: IEmployee) {
    this.employeeService.refreshEmployeeData(employeeData).subscribe(
      (data: IEmployee) => {
        console.log(data);
      }
    );
  }
}
