import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeeService } from './services/employee.service';
import { EmployeeWindowComponent } from './components/employee-window/employee-window.component';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertifyService } from './services/alertify.service';
import { EmployeeDetailsComponent } from './components/employee-details/employee-details.component';
import { HttpErrorInterceptor } from './interceptors/httpError-interceptor';
import { EmployeeFormsComponent } from './components/employee-forms/employee-forms.component';

const appRoutes: Routes = [
  { path: '', component: EmployeeWindowComponent },
  { path: 'employee-details/:id', component: EmployeeDetailsComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    EmployeeWindowComponent,
    EmployeeDetailsComponent,
    EmployeeFormsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    ModalModule.forRoot()
  ],
  providers: [
    EmployeeService,
    AlertifyService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
