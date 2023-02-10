import { UserModule } from './user-module.model';

export class MyProfile {
    mediaFileId?: string;
    candidatePositionName?: string;
    email: string;
    firstName: string;
    lastName: string;
    fatherName: string;
    idnp: string;
    birthday;
    isActive: boolean;
    modules: UserModule[];
}