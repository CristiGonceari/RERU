import { QuestionUnitStatusEnum } from "../../enums/question-unit-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";

export class QuestionUnitPreview {
    id: number;
    index: number;
    status: QuestionUnitStatusEnum;
    questionType: QuestionUnitTypeEnum;
    question: string;
    categoryName: string;
}
