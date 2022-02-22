import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { SelectionTypeEnum } from "../../enums/selection-type.enum";
import { SequenceTypeEnum } from "../../enums/sequence-type.enum";
import { TestTemplateQuestionCategoryOrder } from "./test-template-question-category-order.model";

export class QuestionCategoryPreview {
    testTemplateId: number;
    categoryId: number;
    questionCount?: number;
    questionType: QuestionUnitTypeEnum;
    selectionType: SelectionTypeEnum;
    sequenceType: SequenceTypeEnum;
    selectedQuestions: TestTemplateQuestionCategoryOrder[];
}
