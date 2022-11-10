import {MedicalColumnEnum} from '../../enums/medical-column.enum';

export class Position {
    id?: number;
    name: string;
    isActive: boolean;
    description: string;
    from: string;
    to: string;
    responsiblePerson: string;
    responsiblePersonId: number;
    medicalColumn: MedicalColumnEnum;
    requiredDocuments: [];
    events: [];
}
