
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
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GoToTestModalComponent } from '../modals/go-to-test-modal/go-to-test-modal.component';


@Injectable()
export class InternalNotifyInterceptor extends AbstractService implements HttpInterceptor {
	type: string;
	messageText: string;
	title: string;
	description: string;

	constructor(
		public notificationService: NotificationsService,
		public translate: I18nService,
		public router: Router,
		protected configService: AppSettingsService,
		private modalService: NgbModal
	) {
		super(configService);
	}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(req).pipe(
			tap(evt => {
				if (evt instanceof HttpResponse && evt.url.includes('test-notification')) {
					if (evt && evt.body && evt.body.data.length > 0) {
						forkJoin([
							this.translate.get('modal.attention'),
							this.translate.get('tests.start-test'),
						  ]).subscribe(([title, description]) => {
							this.title = title;
							this.description = description;
						  });
						  this.notificationService.warn(this.title, this.description);
						  this.notificationService.warn(this.title, this.description, {
							timeOut: 29000,
							showProgressBar: true,
						}).click.subscribe(() => {
							const modalRef: any = this.modalService.open(GoToTestModalComponent, { centered: true, size: 'lg' });
   							 modalRef.componentInstance.testData = evt.body.data;
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
