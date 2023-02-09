import { ClassProvider, Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
	providedIn: 'root',
})
export class AuthenticationHeaderInterceptor implements HttpInterceptor {
	constructor(
    private authenticationService: AuthenticationService
    )
	{}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		request = this.tokenization(request);

		return next.handle(request);
	}

	tokenization(request) {
		if (this.authenticationService.isAuthenticated()) {
			request = request.clone({
				setHeaders: this.setTokenOnHeader(this.authenticationService.authorizationHeaderValue),
				withCredentials: true,
			});
		}

		return request;
	}

	setTokenOnHeader(token) {
		return {
			Authorization: token,
		};
	}
}

export const AUTHENTICATION_HEADER_INTERCEPTOR_PROVIDER: ClassProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: AuthenticationHeaderInterceptor,
	multi: true,
};
