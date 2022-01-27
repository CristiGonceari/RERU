import { UserModuleAccessModel } from './user-module-access.model';

export class UserForRemove {
    name: string;
    lastName: string;
    email: string;
    fatherName: string;
    idnp: string;
    moduleAccess: UserModuleAccessModel[];
  }