import { ContactModel } from './contact.model';
import { AvatarModel } from './avatar.model'; 

export interface Contractor {
    id?: number;
    code?: string;
    firstName: string;
    lastName: string;
    fatherName: string;
    employerState?: string;
    positionId?: number;
    departmentName?: string;
    organizationRoleId?: number;
    organizationRoleName?: string;
    sex: number;
    bloodTypeId?: number;
    birthDate: string;
    contacts?: ContactModel[];
    hasUserProfile?: boolean;
    hasBulletin?: boolean;
    hasStudies?: boolean;
    hasAvatar?: boolean;
    hasCim?: boolean;
    hasPositions?: boolean;
    hasIdentityDocuments?: boolean;
    hasEmploymentRequest?:boolean;
    files?: any[];
    fromDate?: string;
    toDate?: string;
    departmentId?: number;
    avatar?: AvatarModel[];

    currentPosition?: {
        id?: number;
        fromDate: string;
        departmentId: number;
        organizationRoleId: number;
    }
}
