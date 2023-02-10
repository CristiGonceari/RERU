// import { IAppSettings } from '../models/app-settings.model';
// import { AbstractService } from '../services/abstract.service';
import { AppSettingsService } from '../services/app-settings.service';
import { ApplicationUserService } from '../services/application-user.service';
import { AuthenticationService } from '../services/authentication.service';
//import { ApplicationUserService } from '../services/application-user.service';

// export const appInitializerFn = (appSettings: AppSettingsService, applicationUserService: ApplicationUserService): (() => Promise<any>) => {
//   return (): Promise<any> => {

//     const helper = new AbstractService(appSettings);
//     return appSettings.load().then((response: IAppSettings) => applicationUserService.loadCurrentUser(helper.createBaseUrl(response.CORE_URL)));
//   };
// };

export function appInitializerFn(
	appSettingsService: AppSettingsService,
	authenticationService: AuthenticationService,
	applicationUserService: ApplicationUserService
): () => Promise<any> {
	return (): Promise<any> => {
		return new Promise((resolve, reject) => {
			appSettingsService
				.load()
				.then(data => {
					return authenticationService.initAuthenticationService().then(() => applicationUserService.loadCurrentUser());
				})
				.then(() => {
					resolve(0);
				})
				.catch(error => {
					console.log(error);
					reject();
				});
		});
	};
}

// export function loadApplicationUserFn(applicationUserService: ApplicationUserService): () => Promise<any> {
// 	return () => {
// 		console.log('==> loadApplicationUserFn');
// 		return applicationUserService.loadCurrentUser();
// 	};
// }

// export function initAuthenticationServiceFn(
// 	authenticationService: AuthenticationService,
// 	applicationUserService: ApplicationUserService
// ): () => Promise<any> {
// 	return () => {

// }

// export function appInitializerFn(
// 	appSettingsService: AppSettingsService,
// 	appSettingsDependencies: (() => Function)[]
// ): () => Promise<any> {
// 	return (): Promise<any> => {
// 		return new Promise((resolve, reject) => {
// 			appSettingsService
// 				.load()
// 				.then(data => {
// 					console.log('==> appInitializerFn loaded appsettings');
// 					//	console.log(applicationUserService);
// 					return Promise.all(appSettingsDependencies.map(dep => dep()));
// 				})
// 				.then(() => {
// 					console.log('==> resolved appInitializerFn');
// 					resolve(0);
// 				})
// 				.catch(error => {
// 					console.log('==> rejected appInitializerFn');
// 					console.log(error);
// 					reject();
// 				});
// 		});
// 	};
// }

// export function loadApplicationUserFn(applicationUserService: ApplicationUserService): () => Promise<any> {
// 	return () => {
// 		console.log('==> loadApplicationUserFn');
// 		return applicationUserService.loadCurrentUser();
// 	};
// }

// export function initAuthenticationServiceFn(
// 	authenticationService: AuthenticationService,
// 	applicationUserService: ApplicationUserService
// ): () => Promise<any> {
// 	return () => {
// 		return authenticationService
// 			.initAuthenticationService()
// 			.then(() => loadApplicationUserFn(applicationUserService).call(0));
// 	};
// }
