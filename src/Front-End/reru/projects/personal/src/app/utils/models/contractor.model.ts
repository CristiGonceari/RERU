import { ContactModel } from './contact.model';
import { AvatarModel } from './avatar.model'; 

export interface Contractor {
    id?: number;
    code?: string;
    firstName: string;
    lastName: string;
    fatherName: string;
    idnp: string;
    employerState?: string;
    positionId?: number;
    departmentName?: string;
    organizationRoleId?: number;
    organizationRoleName?: string;
    functionId?: number;
    functionName?: string;
    sex: number;
    stateLanguageLevel: number;
    candidateCitizenshipId: number;
    candidateNationalityId: number;
    homePhone: string;
    phoneNumber: string;
    workPhone: string;
    bloodTypeId?: number;
    birthDate: string;
    contacts?: ContactModel[];
    hasUserProfile?: boolean;
    hasBulletin?: boolean;
    hasModernLanguages?: boolean;
    hasRecommendationsForStudy?: boolean;
    hasStudies?: boolean;
    hasAvatar?: boolean;
    hasCim?: boolean;
    hasPositions?: boolean;
    hasIdentityDocuments?: boolean;
    hasEmploymentRequest?:boolean;
    hasMaterialStatus?:boolean;
    hasKinshipRelations?:boolean;
    hasKinshipRelationCriminalData?:boolean;
    hasKinshipRelationWithUserProfiles?:boolean;
    hasMilitaryObligations?:boolean;
    hasAutobiography?:boolean;
    files?: any[];
    fromDate?: string;
    toDate?: string;
    departmentId?: number;
    avatar?: AvatarModel[];
    userStatus?: number;

    currentPosition?: {
        id?: number;
        fromDate: string;
        departmentId: number;
        organizationRoleId: number;
    }
}
