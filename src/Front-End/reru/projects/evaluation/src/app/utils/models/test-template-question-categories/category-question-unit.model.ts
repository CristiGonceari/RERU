import { QuestionUnitStatusEnum } from "../../enums/question-unit-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";

export class CategoryQuestionUnit {
    index: number;
    questionUnitId: number;
    question: string;
    questionType: QuestionUnitTypeEnum;
    statis: QuestionUnitStatusEnum;
    optionsCount: number;
}
