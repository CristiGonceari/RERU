import { UserModule } from './user-module.model';

export class MyProfile {
    avatar: string;
    email: string;
    name: string;
    lastName: string;
    fatherName: string;
    idnp: string;
    isActive: boolean;
    modules: UserModule[];
}