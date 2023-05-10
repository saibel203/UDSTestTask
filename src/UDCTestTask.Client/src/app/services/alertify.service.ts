import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() { }

  alertifySettings = alertify.set('notifier', 'position', 'top-right');

  success(message: string): void {
    alertify.success(message);
  }

  error(message: string): void {
    alertify.error(message);
  }

  warning(message: string): void {
    alertify.warning(message);
  }

  message(message: string): void {
    alertify.message(message);
  }

  confirm(message: string, acceptFunction: Function, cancelMessage: string): void {
    alertify.confirm(message, acceptFunction,
      () => {
        this.success(cancelMessage);
      });
  }
}
