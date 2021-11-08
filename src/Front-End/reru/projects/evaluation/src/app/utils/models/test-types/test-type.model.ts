import { SequenceTypeEnum } from "../../enums/sequence-type.enum";
import { TestTypeModeEnum } from "../../enums/test-type-mode.enum";
import { TestTypeStatusEnum } from "../../enums/test-type-status.enum";

export class TestType {
    id: number;
    name: string;
    questionCount?: number;
    minPercent: number;
    duration: number;
    categoriesCount?: number;
    categoriesSequence: SequenceTypeEnum;
    status: TestTypeStatusEnum;
    mode: TestTypeModeEnum;
}
