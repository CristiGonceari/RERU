import { Injectable } from '@angular/core';
//import { IAppSettings } from '../models/app-settings.model';
import { AppSettingsService } from './app-settings.service';

@Injectable({
	providedIn: 'root',
})
export class AbstractService {
	private readonly apiVersion = 'api';
	protected appUrl: string;
	protected identityUrl: string;

	constructor(protected service: AppSettingsService) {
    console.log('===> abstract service constructor');
		this.initConfig();
	}

	initConfig(): void {
	//	this.config = this.service.settings;
	  this.appUrl = this.service.settings.APP_BASE_URL;
		this.identityUrl = this.service.settings.IDENTITY_AUTHORITY;
	}

	createBaseUrl(SERVER_URL: string): string {
		return `${SERVER_URL}/${this.apiVersion}`;
	}
  protected get baseUrl(): string
  {
    return this.createBaseUrl(this.service.settings.SERVER_URL);
  }

  protected get coreUrl(): string
  {
    return this.createBaseUrl(this.service.settings.CORE_URL);
  }
}
