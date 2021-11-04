export interface IAppSettings {
	CLIENT_ID: string;
	IDENTITY_AUTHORITY: string;
	IDENTITY_AUTHORITY_REDIRECT_URI: string;
	MODULE_SCOPE: string;
	SERVER_URL: string;
	CORE_URL: string;
	APP_BASE_URL: string;
	PRODUCTION: boolean;
	DEFAULT_LANGUAGE: string;
}

export class AppSettings implements IAppSettings {
	CLIENT_ID: string;
	SERVER_URL: string;
	CORE_URL: string;
	IDENTITY_AUTHORITY: string;
	IDENTITY_AUTHORITY_REDIRECT_URI: string;
	MODULE_SCOPE: string;
	APP_BASE_URL: string;
	PRODUCTION: boolean;
	DEFAULT_LANGUAGE: string;

	constructor(config?: IAppSettings) {
		if (config) {
			this.CLIENT_ID = config.CLIENT_ID;
			this.SERVER_URL = config.SERVER_URL;
			this.CORE_URL = config.CORE_URL;
			this.IDENTITY_AUTHORITY = config.IDENTITY_AUTHORITY;
			this.IDENTITY_AUTHORITY_REDIRECT_URI = config.IDENTITY_AUTHORITY_REDIRECT_URI;
			this.MODULE_SCOPE = config.MODULE_SCOPE;
			this.APP_BASE_URL = config.APP_BASE_URL;
			this.PRODUCTION = config.PRODUCTION;
			this.DEFAULT_LANGUAGE = config.DEFAULT_LANGUAGE;
		} else {
			this.CLIENT_ID = null;
			this.SERVER_URL = null;
			this.CORE_URL = null;
			this.APP_BASE_URL = null;
			this.IDENTITY_AUTHORITY = null;
			this.IDENTITY_AUTHORITY_REDIRECT_URI = null;
      this.MODULE_SCOPE = null
			this.PRODUCTION = false;
			this.DEFAULT_LANGUAGE = 'ro';
		}
	}
}
