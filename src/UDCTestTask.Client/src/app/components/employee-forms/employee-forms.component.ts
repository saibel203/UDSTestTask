import { Component, EventEmitter, Input, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IEmployee } from 'src/app/models/IEmployee.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-forms',
  templateUrl: './employee-forms.component.html',
  styleUrls: ['./employee-forms.component.scss']
})
export class EmployeeFormsComponent {
  constructor(private fb: FormBuilder, private employeeService: EmployeeService,
    private alertifyService: AlertifyService) { }

  @ViewChild('createEmployee') createEmployeeTempate!: TemplateRef<any>;
  @ViewChild('refreshEmployeeData') reafreshEmployeeTempate!: TemplateRef<any>;

  @Output() employeesToParent = new EventEmitter<IEmployee[]>();

  @Input() modalRef?: BsModalRef;
  @Input() employee?: IEmployee;
  @Input() selectedEmployeeId: number = 0;

  newEmployeeForm?: FormGroup;
  refreshEmployeeForm?: FormGroup;
  isNewEmployeeFormSubmitted?: boolean;
  isRefreshEmployeeFormSubmitted?: boolean;
  selectedGender: string = 'default';
  genders: string[] = ['Male', 'Female'];

  ngOnChanges(): void {
    this.restoreDataByPath();
    this.createRefreshEmployeeForm();
    console.log(this.employee);
  }

  ngOnInit(): void {
    this.createNewEmployeeForm();
  }

  createNewEmployeeForm(): void {
    this.newEmployeeForm = this.fb.group({
      firstName: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      lastName: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      gender: ['default', [Validators.required, Validators.minLength(3), Validators.maxLength(7),
      this.allowedValuesValidator(this.genders)]],
      city: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]]
    });
  }

  createRefreshEmployeeForm(): void {
    console.log(this.employee);
    this.refreshEmployeeForm = this.fb.group({
      refreshFirstName: [this.employee?.firstName, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      refreshLastName: [this.employee?.lastName, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      refreshGender: [this.employee?.gender, [Validators.required, Validators.minLength(3), Validators.maxLength(7),
      this.allowedValuesValidator(this.genders)]],
      refreshCity: [this.employee?.city, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]]
    });
  }

  allowedValuesValidator(allowedValues: string[]) {
    return (control: FormControl) => {
      const value = control.value;
      const isValid = allowedValues.includes(value);
      return isValid ? null : { allowedValues: { value } };
    };
  }

  onSubmit(): void {
    this.isNewEmployeeFormSubmitted = true;

    if (this.newEmployeeForm?.valid) {
      this.employeeService.createEmployee(this.userData()).subscribe(
        () => {
          this.alertifyService.success('The employee has been successfully created');
          this.onReset();
          this.modalRef?.hide();
          this.employeeService.getAllEmployees().subscribe(
            (employeesData: IEmployee[]) => {
              this.employeesToParent.emit(employeesData);
            }
          );
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

    if (this.refreshEmployeeForm?.valid && this.employee?.employeeId !== 0 && this.employee !== undefined && this.employee !== null) {
      this.employeeService.refreshEmployeeData(this.refreshUserData()).subscribe(
        () => {
          this.alertifyService.success('The employee has been successfully refreshed');
          this.modalRef?.hide();
          this.employeeService.getAllEmployees().subscribe(
            (employeesData: IEmployee[]) => {
              this.employeesToParent.emit(employeesData);
            }
          );
        }
      )
    } else {
      this.alertifyService.error('First select an employee');
      this.modalRef?.hide();
      this.onRefreshReset();
      this.employee = null!;
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
