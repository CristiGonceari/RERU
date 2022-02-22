import { TestTemplateModeEnum } from "../../enums/test-template-mode.enum";

export class AddEditTestTemplate {
    id?: number;
    name: string;
    questionCount: number;
    minPercent?: number;
    duration?: number;
    mode: TestTemplateModeEnum;
}
