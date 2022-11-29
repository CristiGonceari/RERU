import { ApplicationUserService, AppSettingsService, AbstractService } from '@erp/shared';
import { InitializerUserProfileService } from '../services/initializer-user-profile.service';

//TODOȘ De revenit și de revizuit

export const moduleInitializerFn = (appConfig: AppSettingsService, applicationUserService: ApplicationUserService, userProfileService: InitializerUserProfileService): (() => Promise<any>) => {
  return (): Promise<any> => {
    const helper = new AbstractService(appConfig);
    return appConfig.load()
                    .then((response) => { userProfileService.get(response).subscribe(() => {}); return response })
                    .then((response) => applicationUserService.loadCurrentUser());
  };
};
