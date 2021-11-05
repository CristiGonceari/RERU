import { QuestionUnitStatusEnum } from "../../enums/question-unit-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";

export class QuestionUnit {
    id: number;
    status: QuestionUnitStatusEnum;
    questionType: QuestionUnitTypeEnum;
    answersCount: number;
    usedInTestsCount: number;
    question: string;
    categoryName: string;
    categoryId: number;
    tags: string[];
    questionPoints: number;
}
