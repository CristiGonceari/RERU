import { BasicTemplateEnum } from "../../enums/basic-template.enum";
import { SequenceTypeEnum } from "../../enums/sequence-type.enum";
import { TestTemplateModeEnum } from "../../enums/test-template-mode.enum";
import { TestTemplateStatusEnum } from "../../enums/test-template-status.enum";

export class TestTemplate {
    id: number;
    name: string;
    questionCount?: number;
    minPercent: number;
    duration: number;
    isGridTest?: boolean;
    categoriesCount?: number;
    categoriesSequence: SequenceTypeEnum;
    status: TestTemplateStatusEnum;
    mode: TestTemplateModeEnum;
    roles: any[]
    basicTestTemplate?: BasicTemplateEnum | null;
}
