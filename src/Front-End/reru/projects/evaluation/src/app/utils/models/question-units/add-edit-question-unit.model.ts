import { QuestionUnitStatusEnum } from "../../enums/question-unit-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { Tag } from "./tag.model";

export class AddEditQuestionUnit {
    id?: number;
    questionCategoryId: number;
    question: string;
    tags: string[];
    questionType: QuestionUnitTypeEnum;
    status: QuestionUnitStatusEnum;
    questionPoints: number;
}
