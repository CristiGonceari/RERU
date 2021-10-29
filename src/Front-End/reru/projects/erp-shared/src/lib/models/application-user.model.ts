import { ApplicationUserModuleModel } from './application-user-module.model';
import { TenantModel } from './tenant.model';

export interface ApplicationUserModel {
	isAuthenticated: boolean;
	user: {
		avatar: string;
		lastname: string;
		name: string;
		email: string;
		permissions: string[];
		modules: ApplicationUserModuleModel[];
	};
	tenant: TenantModel;
}
