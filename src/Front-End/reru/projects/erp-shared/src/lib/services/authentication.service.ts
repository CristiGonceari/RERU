import { Inject, Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { catchError } from 'rxjs/operators';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { BaseService } from './base.service';
// import { BaseService } from "../../shared/base.service";
// import { ConfigService } from '../../shared/config.service';
import { AppSettingsService } from './app-settings.service';
import { MOCK_AUTHENTICATION } from '../constants/injection-tokens';

@Injectable({
	providedIn: 'root',
})
export class AuthenticationService extends BaseService {
	// Observable navItem source
	private _authNavStatusSource = new BehaviorSubject<boolean>(false);
	// Observable navItem stream
	authNavStatus$ = this._authNavStatusSource.asObservable();

	private manager: UserManager;
	private user: User | null;

	constructor(
		private appSettingsService: AppSettingsService,
		@Inject(MOCK_AUTHENTICATION) private MOCK_AUTHENTICATION: boolean
	) {
		super();
	}

	initAuthenticationService(): Promise<any> {
		if (this.MOCK_AUTHENTICATION) return Promise.resolve();
		this.manager = new UserManager(this.getClientSettings());
		return this.manager.getUser().then(user => {
			this.user = user;
			this._authNavStatusSource.next(this.isAuthenticated());
		});
	}

	login() {
		return this.manager.signinRedirect();
	}

	async completeAuthentication() {
		this.user = await this.manager.signinRedirectCallback();
		this._authNavStatusSource.next(this.isAuthenticated());
	}

	// register(userRegistration: any) {
	// 	return this.http
	// 		.post(this.configService.authApiURI + '/account', userRegistration)
	// 		.pipe(catchError(this.handleError));
	// }

	isAuthenticated(): boolean {
		if (this.MOCK_AUTHENTICATION) return true;
		return this.user != null && !this.user.expired;
	}

	get authorizationHeaderValue(): string {
		if (this.MOCK_AUTHENTICATION) return 'Bearer token';
		return `${this.user.token_type} ${this.user.access_token}`;
	}

	get name(): string {
		return this.user != null ? this.user.profile.name : '';
	}

	async signout() {
		await this.manager.signoutRedirect();
	}

	getClientSettings(): UserManagerSettings {
		var settings = this.appSettingsService.settings;
		console.log("Settings", settings);
		console.log("Settings post_logout_redirect_uri", settings.APP_BASE_URL);

		return {
			authority: settings.IDENTITY_AUTHORITY,
			client_id: settings.CLIENT_ID,
			redirect_uri: settings.IDENTITY_AUTHORITY_REDIRECT_URI,
			// post_logout_redirect_uri: settings.APP_BASE_URL,
			response_type: 'id_token token',
			scope: 'openid ' + settings.MODULE_SCOPE,
			filterProtocolClaims: true,
			loadUserInfo: false,
			// automaticSilentRenew: true,
			// silent_redirect_uri: 'http://localhost:4200/silent-refresh.html',
			//response_mode: "query"
		};
	}
}
