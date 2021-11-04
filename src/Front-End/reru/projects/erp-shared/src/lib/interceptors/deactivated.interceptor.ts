import {
	HttpErrorResponse,
	HttpEvent,
	HttpHandler,
	HttpInterceptor,
	HttpRequest,
	HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { ClassProvider, Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin, Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ExpiredSessionModalComponent } from '../modals/expired-session-modal/expired-session-modal.component';
import { I18nService } from '../services/i18n.service';
import { NotificationUtil } from '../utils/notification.util';

@Injectable({
	providedIn: 'root',
})
export class DeactivatedInterceptor implements HttpInterceptor {
	deactivatedTitle: string;
	deactivatedMsg: string;

	constructor(
		public notificationService: NotificationsService,
		private modalService: NgbModal,
		public translate: I18nService
	) {}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(req).pipe(
			tap(evt => {
				if (evt instanceof HttpErrorResponse) {
					let errText = evt.error.messages.map(mt => mt.messageText);
					for (let index = 0; index <= errText.length; index++) {
						if (errText[index] && errText[index].includes('deactivated')) {
							this.showExpiredSessionModal();
							this.notificationService.error('Error', errText, NotificationUtil.getDefaultMidConfig());
						}
					}
				}
			})
		);
	}

	showExpiredSessionModal(): void {
		forkJoin([this.translate.get('deactivated.title'), this.translate.get('deactivated.deactivated-msg')]).subscribe(
			([deactivatedTitle, deactivatedMsg]) => {
				this.deactivatedTitle = deactivatedTitle;
				this.deactivatedMsg = deactivatedMsg;
				const modalRef: any = this.modalService.open(ExpiredSessionModalComponent, {
					centered: true,
					backdrop: 'static',
					keyboard: false,
				});
				modalRef.componentInstance.title = this.deactivatedTitle;
				modalRef.componentInstance.description = this.deactivatedMsg;
			}
		);
	}
}

export const DEACTIVATED_INTERCEPTOR_PROVIDER: ClassProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: DeactivatedInterceptor,
	multi: true
};
