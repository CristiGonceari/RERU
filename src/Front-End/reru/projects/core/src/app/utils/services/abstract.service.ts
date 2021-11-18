// import { Injectable } from '@angular/core';
// import { AppSettingsService, IAppSettings } from '@erp/shared';

// @Injectable({
// 	providedIn: 'root',
// })
// export class AbstractService {
// 	private readonly apiVersion = 'api';
// 	protected config: IAppSettings = null;
// 	protected appUrl: string;
// 	protected baseUrl: string;
// 	protected coreUrl: string;
// 	protected identityUrl: string;
// 	constructor(protected service: AppSettingsService) {
// 		this.initConfig();
// 		this.baseUrl = this.createBaseUrl(this.config.SERVER_URL);
// 		this.coreUrl = this.createBaseUrl(this.config.CORE_URL);
// 	}

// 	initConfig(): void {
// 		this.config = this.service.settings;
// 		this.appUrl = this.service.settings.APP_BASE_URL;
// 		this.baseUrl = this.service.settings.SERVER_URL;
// 		this.coreUrl = this.service.settings.CORE_URL;
// 		this.identityUrl = this.service.settings.IDENTITY_AUTHORITY;
// 	}

// 	createBaseUrl(SERVER_URL: string): string {
// 		return `${SERVER_URL}/${this.apiVersion}`;
// 	}
// }
