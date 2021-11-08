import { AnswerStatusEnum } from "../../enums/answer-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { TestOptions } from "./test-options.model";

export class TestQuestion {
    id: number;
    questionCategoryId: number;
    questionUnitId: number;
    answersCount: number;
    timeLimit?: number;
    question: string;
    categoryName: string;
    answerText: string;

    answerStatus: AnswerStatusEnum;
    questionType: QuestionUnitTypeEnum;
    options: TestOptions[];
    hashedOptions: TestOptions[];
}
