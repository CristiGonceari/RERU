import {
	HttpInterceptor,
	HttpHandler,
	HttpRequest,
	HttpEvent,
	HttpResponse,
	HttpErrorResponse,
	HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { ClassProvider, Injectable } from '@angular/core';
import { forkJoin, Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../services/i18n.service';
import { NotificationUtil } from '../utils/notification.util';

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {
	type: string;
	errorMessage: string;

	constructor(public notificationService: NotificationsService, public translate: I18nService) {}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(req).pipe(
			tap(evt => {
				if (evt instanceof HttpResponse) {
					if (evt.body && evt.body.success && evt.body.messages.length) {
						let mText = evt.body.messages.map(mt => mt.messageText);
						this.notificationService.success('Success', mText, NotificationUtil.getDefaultMidConfig());
					}
				}
			}),
			catchError((httpError: HttpResponse<HttpErrorResponse>) => {
				if (httpError instanceof HttpErrorResponse) {
					try {
						switch (httpError.status) {
							case 0: 
								this.translate.get('notification.title.error').subscribe((type) => {
									this.type = type;
									this.errorMessage = 'Server offline!';
									this.notificationService.error(
										this.type, 
										this.errorMessage, 
										NotificationUtil.getDefaultMidConfig()
									);
								});
								break;
							default:
								const errorText = httpError?.error?.messages?.map(mt => mt.code);
								for (let index = 0; index <= errorText?.length; index++) {
									const code = errorText[index];
									if (code) {
										forkJoin([
											this.translate.get('notification.title.error'),
											this.translate.get('message-code.' + code),
										]).subscribe(([type, message]) => {
											this.type = type;
											this.errorMessage = message;
											this.notificationService.error(this.type, this.errorMessage, NotificationUtil.getDefaultMidConfig());
										});
									}
								}
								break;
						}
						
					} catch (e) {
						console.error('Error', 'An error occurred', e);
					}
				}
				
				return throwError(httpError);
			})
		);
	}
}

export const HTTP_RESPONSE_INTERCEPTOR_PROVIDER: ClassProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: ResponseInterceptor,
	multi: true,
};
