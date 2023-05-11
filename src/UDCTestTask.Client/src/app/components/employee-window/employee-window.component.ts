import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IEmployee } from 'src/app/models/IEmployee.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { EmployeeService } from 'src/app/services/employee.service';
import { EmployeeFormsComponent } from '../employee-forms/employee-forms.component';

@Component({
  selector: 'app-employee-window',
  templateUrl: './employee-window.component.html',
  styleUrls: ['./employee-window.component.scss']
})
export class EmployeeWindowComponent {
  constructor(private employeeService: EmployeeService, private modalService: BsModalService,
    private alertifyService: AlertifyService, private router: Router) { }

  @ViewChild(EmployeeFormsComponent) childFormComponent!: EmployeeFormsComponent;

  modalRef?: BsModalRef;
  employee?: IEmployee;
  employees?: IEmployee[];
  selectedEmployeeId: number = 0;

  ngOnInit(): void {
    this.restoreDataByPath();
    this.allEmployees();
  }

  getEmployeesListAfterFormAction(eventData: IEmployee[]) {
    this.employees = eventData;
  }

  allEmployees() {
    this.employeeService.getAllEmployees().subscribe(
      (employeesData: IEmployee[]) => {
        this.employees = employeesData;
      }
    );
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  onDeleteEmployee(id: number): void {
    this.alertifyService.confirm('Are you sure you want to delete the employee?',
      () => {
        this.employeeService.removeEmployee(id).subscribe(
          (data: IEmployee[]) => {
            this.employees = data;
            this.alertifyService.success('The employee was successfully deleted');
            this.restoreData();
          }
        );
      },
      'The remove of the employee has been canceled');
  }

  getCurrentUserData(id: number) {
    this.selectedEmployeeId = id;
    this.employeeService.getEmployeeById(id).subscribe(
      (employeeData: IEmployee) => {
        this.employee = employeeData;
      }
    );
  }

  restoreDataByPath(): void {
    if (this.employee === undefined) {
      let pathString = +window.location.href.substring(window.location.href.lastIndexOf('/') + 1);

      if (pathString !== 0) {
        this.getCurrentUserData(pathString);
      }
    }
  }

  restoreData(): void {
    this.router.navigate(['/']);
    this.employee = null!;
    this.selectedEmployeeId = 0;
  }
}
