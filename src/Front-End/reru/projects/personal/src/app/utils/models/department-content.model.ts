import { SelectItem } from './select-item.model';

export interface DepartmentTemplateModel {
    departmentId: number;
    departmentName: string;
    roles: DepartmentContentRoles[];
}

export interface DepartmentActualModel {
    departmentId: number;
    departmentName: string;
    roles: DepartmentContentRoles[];
}

export interface DepartmentSummaryModel {
    departmentId: number;
    departmentName: string;
    roles: DepartmentContentRoles[];
}

export interface DepartmentContentRoles {
    contractorIds: SelectItem[];
    departmentRoleContentId: number;
    organizationRoleCount: number;
    organizationRoleId: number;
    organizationRoleName: number;
    currentCount?: number;
}

export enum DepartmentContentTypeEnum {
    Template,
    Actual,
    Summary
}
