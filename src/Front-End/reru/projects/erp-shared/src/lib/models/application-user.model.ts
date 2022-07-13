import { ApplicationUserModuleModel } from './application-user-module.model';
import { TenantModel } from './tenant.model';

export interface ApplicationUserModel {
	isAuthenticated: boolean;
	isCandidateStatus: boolean;
	user: {
		avatar: string;
		lastname: string;
		firstName: string;
		email: string;
		permissions: string[];
		modules: ApplicationUserModuleModel[];
	};
	tenant: TenantModel;
}
