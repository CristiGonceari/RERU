import { UserModuleAccessModel } from './user-module-access.model';

export class UserForRemove {
    firstName: string;
    lastName: string;
    email: string;
    fatherName: string;
    idnp: string;
    moduleAccess: UserModuleAccessModel[];
  }