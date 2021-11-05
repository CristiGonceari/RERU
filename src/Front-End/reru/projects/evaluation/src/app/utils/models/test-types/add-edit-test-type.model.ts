import { TestTypeModeEnum } from "../../enums/test-type-mode.enum";

export class AddEditTestType {
    id?: number;
    name: string;
    questionCount: number;
    minPercent?: number;
    duration?: number;
    mode: TestTypeModeEnum;
}
