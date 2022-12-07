// import { Injectable } from '@angular/core';
// import { IAppSettings } from '@erp/shared';
// import { AppConfigService } from './app-config.service';

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
// 	constructor(protected service: AppConfigService) {
// 		this.initConfig();
// 		this.baseUrl = this.createBaseUrl(this.config.SERVER_URL);
// 		this.coreUrl = this.createBaseUrl(this.config.CORE_URL);
// 	}

// 	initConfig(): void {
// 		this.config = this.service.config;
// 		this.appUrl = this.service.config.APP_BASE_URL;
// 		this.baseUrl = this.service.config.SERVER_URL;
// 		this.coreUrl = this.service.config.CORE_URL;
// 		this.identityUrl = this.service.config.IDENTITY_AUTHORITY;
// 	}

// 	createBaseUrl(SERVER_URL: string): string {
// 		return `${SERVER_URL}/${this.apiVersion}`;
// 	}
// }
