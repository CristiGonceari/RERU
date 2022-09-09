import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import { AppSettings, IAppSettings } from '../models/app-settings.model';
import { environment } from '../environments/environment';

@Injectable({
	providedIn: 'root',
})
export class AppSettingsService {
	private readonly http: HttpClient;
	private readonly path = environment.PRODUCTION ? 'assets/config/appsettings.json' : 'assets/config/appsettings-dev.json';
	private appSettings: IAppSettings = new AppSettings();
	constructor(handler: HttpBackend) {
		console.log('===> init appsettings constructor');
		this.http = new HttpClient(handler);
	//	this.load();
	}

	load() //: Promise<IAppSettings>
  {
		console.log('===> init appsettings load');
		return this.http
			.get(this.path)
			.toPromise()
			.then((config: IAppSettings) => {
				this.appSettings = config;
				console.log('===> appsettings loaded');
			//	return config;
			})
			.catch(error => {
				console.error('Error loading app-config.json, using environment file instead');
				console.error(error);
			});
	}

	get settings(): IAppSettings {
		return this.appSettings;
	}
}
