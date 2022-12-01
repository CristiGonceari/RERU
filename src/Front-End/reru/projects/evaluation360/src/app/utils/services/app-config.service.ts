import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { AppSettings, IAppSettings } from '@erp/shared';
// import { IAppConfig } from '../models/app-config.model';

@Injectable({
	providedIn: 'root',
})
export class AppConfigService {
	private readonly http: HttpClient;
	private readonly path = 'assets/config/appsettings.json';
	private appConfig: IAppSettings = new AppSettings();
	constructor(handler: HttpBackend) {
		this.http = new HttpClient(handler);
	}

	load(): Promise<IAppSettings> {
		return this.http
			.get(this.path)
			.toPromise()
			.then((config: IAppSettings) => (this.appConfig = config))
			.catch(error => {
				console.error('Error loading app-config.json, using environment file instead');
				console.error(error);
				//this.appConfig = environment;

				return Promise.resolve(this.appConfig);
			});
	}

	get config(): IAppSettings {
		return this.appConfig;
	}
}
