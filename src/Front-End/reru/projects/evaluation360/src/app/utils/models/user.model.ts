export interface UserModel {
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
    userStatusEnum: number;
    accessModeEnum: number;
    permissions: string[];
    candidatePositionNames: string[];
    fullName: string[];
}
