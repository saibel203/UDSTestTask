import { Component, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IEmployee } from 'src/app/models/IEmployee.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-window',
  templateUrl: './employee-window.component.html',
  styleUrls: ['./employee-window.component.scss']
})
export class EmployeeWindowComponent {
  constructor(private employeeService: EmployeeService, private modalService: BsModalService,
    private fb: FormBuilder, private alertifyService: AlertifyService, private router: Router) { }

  modalRef?: BsModalRef;
  employee?: IEmployee;
  employees?: IEmployee[];
  newEmployeeForm?: FormGroup;
  refreshEmployeeForm?: FormGroup;
  selectedEmployeeId?: number;
  isNewEmployeeFormSubmitted?: boolean;
  isRefreshEmployeeFormSubmitted?: boolean;

  ngOnInit(): void {
    this.restoreDataByPath();
    this.createNewEmployeeForm();
    this.createRefreshEmployeeForm();
    this.allEmployees();
  }

  allEmployees() {
    this.employeeService.getAllEmployees().subscribe(
      (employeesData: IEmployee[]) => {
        this.employees = employeesData;
      }
    );
  }

  refreshEmployeeData(employeeData: IEmployee) {
    this.employeeService.refreshEmployeeData(employeeData).subscribe(
      (data: IEmployee) => {
        console.log(data);
      }
    );
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  createNewEmployeeForm(): void {
    this.newEmployeeForm = this.fb.group({
      firstName: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      lastName: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      gender: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(7)]],
      city: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]]
    });
  }

  createRefreshEmployeeForm(): void {
    this.refreshEmployeeForm = this.fb.group({
      refreshFirstName: [this.employee?.firstName, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      refreshLastName: [this.employee?.lastName, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      refreshGender: [this.employee?.gender, [Validators.required, Validators.minLength(3), Validators.maxLength(7)]],
      refreshCity: [this.employee?.city, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]]
    });
  }

  onSubmit(): void {
    this.isNewEmployeeFormSubmitted = true;

    if (this.newEmployeeForm?.valid) {
      this.employeeService.createEmployee(this.userData()).subscribe(
        () => {
          this.alertifyService.success('The employee has been successfully created');
          this.onReset();
          this.modalRef?.hide();
          this.allEmployees();
        }
      )
    }
  }

  onReset(): void {
    this.isNewEmployeeFormSubmitted = false;
    this.newEmployeeForm?.reset();
  }

  onRefreshSubmit(): void {
    this.isRefreshEmployeeFormSubmitted = true;
    console.log(this.refreshUserData());

    if (this.refreshEmployeeForm?.valid) {
      this.employeeService.refreshEmployeeData(this.refreshUserData()).subscribe(
        () => {
          if (this.employee?.employeeId === 0)
            this.alertifyService.error('Користувача не обрано!');

          this.alertifyService.success('The employee has been successfully created');
          this.modalRef?.hide();
          this.allEmployees();
        }
      )
    }
  }

  onRefreshReset(): void {
    this.isRefreshEmployeeFormSubmitted = false;
    this.refreshEmployeeForm?.reset();
  }

  onDeleteEmployee(id: number): void {
    this.alertifyService.confirm('Are you sure you want to delete the employee?',
      () => {
        this.employeeService.removeEmployee(id).subscribe(
          (data: IEmployee[]) => {
            this.employees = data;
          }
        );
        this.alertifyService.success('The employee was successfully deleted');
        this.restoreData();
      },
      'The remove of the employee has been canceled');
  }

  userData(): IEmployee {
    return this.employee = {
      employeeId: 0,
      firstName: this.firstName?.value,
      lastName: this.lastName?.value,
      gender: this.gender?.value,
      city: this.city?.value
    };
  }

  refreshUserData(): IEmployee {
    return this.employee = {
      employeeId: this.selectedEmployeeId,
      firstName: this.refreshFirstName?.value,
      lastName: this.refreshLastName?.value,
      gender: this.refreshGender?.value,
      city: this.refreshCity?.value
    };
  }

  getCurrentUserData(id: number) {
    this.selectedEmployeeId = id;
    this.employeeService.getEmployeeById(id).subscribe(
      (employeeData: IEmployee) => {
        console.log(employeeData);
        this.employee = employeeData;
        this.createRefreshEmployeeForm();
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
    this.onRefreshReset();
  }

  // ------------------------------------
  // Getter methods for all form controls
  // ------------------------------------

  get firstName() {
    return this.newEmployeeForm?.get('firstName') as FormControl;
  }
  get lastName() {
    return this.newEmployeeForm?.get('lastName') as FormControl;
  }
  get gender() {
    return this.newEmployeeForm?.get('gender') as FormControl;
  }
  get city() {
    return this.newEmployeeForm?.get('city') as FormControl;
  }

  get refreshFirstName() {
    return this.refreshEmployeeForm?.get('refreshFirstName') as FormControl;
  }
  get refreshLastName() {
    return this.refreshEmployeeForm?.get('refreshLastName') as FormControl;
  }
  get refreshGender() {
    return this.refreshEmployeeForm?.get('refreshGender') as FormControl;
  }
  get refreshCity() {
    return this.refreshEmployeeForm?.get('refreshCity') as FormControl;
  }
}
