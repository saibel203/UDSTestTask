import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IEmployee } from '../models/IEmployee.interface';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  apiWebPath: string = environment.apiWebPath;

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<IEmployee[]> {
    const fullPath: string = this.apiWebPath + '/employee/all';
    return this.http.get<IEmployee[]>(fullPath);
  }

  getEmployeeById(employeeId: number): Observable<IEmployee> {
    const fullPath: string = this.apiWebPath + '/employee/getById/' + employeeId;
    return this.http.get<IEmployee>(fullPath);
  }

  createEmployee(employee: IEmployee): Observable<IEmployee> {
    const fullPath: string = this.apiWebPath + '/employee/new';
    return this.http.post<IEmployee>(fullPath, employee);
  }

  removeEmployee(employeeId: number): Observable<IEmployee[]> {
    const fullPath: string = this.apiWebPath + '/employee/remove/' + employeeId;
    return this.http.delete<IEmployee[]>(fullPath);
  }

  refreshEmployeeData(employee: IEmployee): Observable<IEmployee> {
    const fullPath: string = this.apiWebPath + '/employee/refresh';
    return this.http.put<IEmployee>(fullPath, employee);
  }
}
