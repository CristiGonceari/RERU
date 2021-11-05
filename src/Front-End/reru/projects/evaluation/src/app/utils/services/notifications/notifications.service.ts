import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService, AppSettingsService } from '@erp/shared';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin, Observable } from 'rxjs';
import { I18nService } from '../i18n/i18n.service';

@Injectable({
	providedIn: 'root'
})
export class NotificationsApiService extends AbstractService {

	type: string;
	messageText: string;
	private readonly urlRoute = 'Notifications';

	constructor(protected appConfigService: AppSettingsService,
		private client: HttpClient,
		public notificationService: NotificationsService,
		public translate: I18nService) {
		super(appConfigService);
		this.setIntrvl();
	}

	setIntrvl() {
		setInterval(() => this.getNotification(), 30000);
	}

	getNotification() {
		this.getMy().subscribe(res => {
			if (res && res.data.length) {
				let test = res.data.map(el => el.id);
				let message = res.data.map(el => el.messageCode);
				for (let index = 0; index <= message.length; index++) {
					const element = message[index];
					const notificationId = test[index];
					if (element) {
						forkJoin([
							this.translate.get('notification.title.notify'),
							this.translate.get('message-code.' + element),
						]).subscribe(([type, message]) => {
							this.type = type;
							this.messageText = message;
							this.notificationService
								.info(this.type, this.messageText, { timeOut: 29000, showProgressBar: true })
								.click.subscribe(() => this.seen(notificationId).subscribe());
						});
					}
				}
			}
		})
	}

	getMy(): Observable<any> {
		return this.client.get<any>(`${this.baseUrl}/${this.urlRoute}/my`);
	}

	seen(id: number): Observable<any> {
		return this.client.post<any>(`${this.baseUrl}/${this.urlRoute}/${id}/seen`, id);
	}
}
