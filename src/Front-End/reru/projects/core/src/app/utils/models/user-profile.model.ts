import { UserModule } from './user-module.model';

export class MyProfile {
    avatar: string;
    email: string;
    name: string;
    lastName: string;
    isActive: boolean;
    modules: UserModule[];
}