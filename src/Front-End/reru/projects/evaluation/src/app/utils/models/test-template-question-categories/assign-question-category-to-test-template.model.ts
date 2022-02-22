import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { SelectionTypeEnum } from "../../enums/selection-type.enum";
import { SequenceTypeEnum } from "../../enums/sequence-type.enum";
import { TestCategoryQuestion } from "./test-category-question.model";

export class AssignQuestionCategoryToTestTemplate {
    testTemplateId: number;
    questionCategoryId: number;
    categoryIndex?: number;
    questionCount?: number;
    timeLimit?: number;
    questionType?: QuestionUnitTypeEnum;
    selectionType: SelectionTypeEnum;
    sequenceType: SequenceTypeEnum;
    testCategoryQuestions: TestCategoryQuestion[];
}
