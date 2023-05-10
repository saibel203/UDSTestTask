import { Component, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
    private fb: FormBuilder, private alertifyService: AlertifyService) { }

  modalRef?: BsModalRef;
  employee?: IEmployee;
  employees?: IEmployee[];
  newEmployeeForm?: FormGroup;
  refreshEmployeeForm?: FormGroup;
  selectedEmployeeId?: number;
  isNewEmployeeFormSubmitted?: boolean;
  isRefreshEmployeeFormSubmitted?: boolean;

  ngOnInit(): void {
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

  getEmployeeById(employeeId: number) {
    this.employeeService.getEmployeeById(employeeId).subscribe(
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

    if (this.refreshEmployeeForm?.valid) {
      this.employeeService.refreshEmployeeData(this.employee!).subscribe(
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

  userData(): IEmployee {
    return this.employee = {
      employeeId: 0,
      firstName: this.firstName?.value,
      lastName: this.lastName?.value,
      gender: this.gender?.value,
      city: this.city?.value
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
    return this.refreshEmployeeForm?.get('firstName') as FormControl;
  }
  get refreshLastName() {
    return this.refreshEmployeeForm?.get('lastName') as FormControl;
  }
  get refreshGender() {
    return this.refreshEmployeeForm?.get('gender') as FormControl;
  }
  get refreshCity() {
    return this.refreshEmployeeForm?.get('city') as FormControl;
  }
}
