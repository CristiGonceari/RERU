import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { SelectionTypeEnum } from "../../enums/selection-type.enum";
import { SequenceTypeEnum } from "../../enums/sequence-type.enum";

export class TestTemplateQuestionCategory {
    id: number;
    questionCategoryId: number;
    categoryIndex: number;
    categoryName: string;
    timeLimit?: string;
    questionType: QuestionUnitTypeEnum;
    selectionType: SelectionTypeEnum;
    sequenceType: SequenceTypeEnum;
    questionCount?: number;
}
