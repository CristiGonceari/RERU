
import {
	HttpInterceptor,
	HttpHandler,
	HttpRequest,
	HttpEvent,
	HttpResponse,
	HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { ClassProvider, Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NotificationsService } from 'angular2-notifications';
import { I18nService } from '../services/i18n.service';
import { Router } from '@angular/router';
import { AbstractService } from '../services/abstract.service';
import { AppSettingsService } from '../services/app-settings.service';

@Injectable()
export class InternalNotifyInterceptor extends AbstractService implements HttpInterceptor {
	type: string;
	messageText: string;

	constructor(
		public notificationService: NotificationsService,
		public translate: I18nService,
		public router: Router,
		protected configService: AppSettingsService,
	) {
		super(configService);
	}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(req).pipe(
			tap(evt => {
				if (evt instanceof HttpResponse && evt.url.includes('test-notification')) {
					if (evt && evt.body && evt.body.data.testId) {
						this.notificationService.info('Start Test', 'Testul e pe cale de a incepe', {
							timeOut: 29000,
							showProgressBar: true,
						}).click.subscribe(() => {
							let host = window.location.host;
							console.warn('url for run test', `http://${host}/reru-evaluation/#/my-activities/start-test/${evt.body.data.testId}`);
							this.router.navigateByUrl(`http://${host}/reru-evaluation/#/my-activities/start-test/${evt.body.data.testId}`)
						});
					}
				}
			})
		);
	}
}

export const INTERNAL_NOTIFY_INTERCEPTOR: ClassProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: InternalNotifyInterceptor,
	multi: true,
};
