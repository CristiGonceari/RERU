import { ClassProvider, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HTTP_INTERCEPTORS,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class IdnpInterceptor implements HttpInterceptor {
  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const idnp: any = localStorage.getItem('idnp');
    if (idnp) {
      const headers = new HttpHeaders({ Idnp: idnp });
      request = request.clone({ headers });
    }

    return next.handle(request);
  }
}

export const IDNP_INTERCEPTOR_PROVIDER: ClassProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: IdnpInterceptor,
  multi: true
};
