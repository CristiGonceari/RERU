import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { SelectionTypeEnum } from "../../enums/selection-type.enum";
import { SequenceTypeEnum } from "../../enums/sequence-type.enum";
import { TestTypeQuestionCategoryOrder } from "./test-type-question-category-order.model";

export class QuestionCategoryPreview {
    testTypeId: number;
    categoryId: number;
    questionCount?: number;
    questionType: QuestionUnitTypeEnum;
    selectionType: SelectionTypeEnum;
    sequenceType: SequenceTypeEnum;
    selectedQuestions: TestTypeQuestionCategoryOrder[];
}
