import { Component, Input } from '@angular/core';
import { IEmployee } from 'src/app/models/IEmployee.interface';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.scss']
})
export class EmployeeDetailsComponent {
  @Input() employeeId?: number;

  employee?: IEmployee;

  constructor(private employeeService: EmployeeService) {
  }

  ngOnChanges() {
    this.getEmployeeData(this.employeeId!);
  }

  ngOnInit(): void {

  }

  getEmployeeData(id: number): void {
    this.employeeService.getEmployeeById(id).subscribe(
      (employeeData: IEmployee) => {
        this.employee = employeeData;
      }
    );
  }
}
