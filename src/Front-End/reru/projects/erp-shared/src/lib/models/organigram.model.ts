export interface HeadOrganigramModel {
    organigramId: number;
    parentDepartmentId: number;
    organigramHeadName: string;
    type: number;
}

export interface ContentOrganigramModel {
    id: number;
    name: string;
    relationId: number;
    type: number;
}

export interface OrganigramUserModel {
    id: number;
    firstName: string;
    lastName: string;
    fatherName: string;
    email: string;
    idnp: string;
    departmentColaboratorId: number;
    departmentName: string;
    roleColaboratorId: number;
    roleName: string;
    functionColaboratorId: number;
    functionName: string;
    userStatusEnum: number;
    accessModeEnum: number;
    permissions: string[];
    candidatePositionNames: string [];
    fullName: string;
}
