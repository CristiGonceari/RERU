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
import { forkJoin, Observable, of, throwError } from 'rxjs';
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
				console.log('response interceptor');
				if (evt instanceof HttpResponse) {
					if (evt.body && evt.body.success && evt.body.messages.length) {
						let mText = evt.body.messages.map(mt => mt.messageText);
						console.log(evt.body);
						this.notificationService.success('Success', mText, NotificationUtil.getDefaultMidConfig());
					}
				}
			}),
			catchError((err: any) => {
				if (err instanceof HttpErrorResponse) {
					try {
						let errText = err.error.messages.map(mt => mt.code);
						for (let index = 0; index <= errText.length; index++) {
							const element = errText[index];
							if (element) {
								forkJoin([
									this.translate.get('notification.title.error'),
									this.translate.get('message-code.' + element),
								]).subscribe(([type, message]) => {
									this.type = type;
									this.errorMessage = message;
									this.notificationService.error(this.type, this.errorMessage, NotificationUtil.getDefaultMidConfig());
								});
							}
						}
					} catch (e) {
						console.error('Error', 'An error occurred', e);
					}
				}
				
				return throwError(err);
			})
		);
	}
}

export const HTTP_RESPONSE_INTERCEPTOR_PROVIDER: ClassProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: ResponseInterceptor,
	multi: true,
};
