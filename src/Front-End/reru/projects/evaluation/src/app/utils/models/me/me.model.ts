import { Tenant } from "./tenant.model";
import { ApplicationUserModel } from "@erp-shared/src/lib/models/ApplicationUserModel";

export class Me {
    isAuthenticated: boolean;
    user: ApplicationUserModel;
    tenant: Tenant;
}
