import { SelectItem } from './select-item.model';

export interface ModuleRoleModel {
    id?: number;
    name: string;
    roles: SelectItem[];
}
