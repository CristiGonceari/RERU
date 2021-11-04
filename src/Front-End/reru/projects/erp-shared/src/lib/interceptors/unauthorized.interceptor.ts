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
import { forkJoin, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ExpiredSessionModalComponent } from '../modals/expired-session-modal/expired-session-modal.component';
import { AuthenticationService } from '../services/authentication.service';
import { I18nService } from '../services/i18n.service';

@Injectable({
	providedIn: 'root',
})
export class UnauthorizedInterceptor implements HttpInterceptor {
	expiredTitle: string;
	expiredMsg: string;

	constructor(
		private authenticationService: AuthenticationService,
		private modalService: NgbModal,
		public translate: I18nService
	) {}

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(req).pipe(
			tap(evt => {
				if (evt instanceof HttpErrorResponse) {
					if (evt.status === 401) {
						forkJoin([
							this.translate.get('expired-token.title'),
							this.translate.get('expired-token.expired-msg'),
						]).subscribe(([title, description]) => {
							this.openExpiredSessionModal(title, description);
						});
					}
				}
			})
		);
	}

	openExpiredSessionModal(title: string, description: string): void {
		const modalRef: any = this.modalService.open(ExpiredSessionModalComponent, {
			centered: true,
			backdrop: 'static',
			keyboard: false,
		});
		modalRef.componentInstance.title = title;
		modalRef.componentInstance.description = description;
		modalRef.result.then(
			() => {
				this.authenticationService.login();
			},
			() => {}
		);
	}
}

export const UNAUTHORIZED_INTERCEPTOR_PROVIDER: ClassProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: UnauthorizedInterceptor,
	multi: true,
};
