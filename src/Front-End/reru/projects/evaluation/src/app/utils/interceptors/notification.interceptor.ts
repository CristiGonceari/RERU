import { ClassProvider, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { NotificationsApiService } from '../services/notifications/notifications.service'

@Injectable()
export class NotificationInterceptor {

  constructor(
    public notifyService: NotificationsApiService
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request);
  }
}

export const NOTIFICATION_INTERCEPTOR_PROVIDER: ClassProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: NotificationInterceptor,
  multi: true
};
