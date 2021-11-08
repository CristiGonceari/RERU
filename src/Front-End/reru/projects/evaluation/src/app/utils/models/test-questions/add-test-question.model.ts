import { AnswerStatusEnum } from "../../enums/answer-status.enum";
import { TestAnswer } from "./test-answer.model";

export class AddTestQuestion {
    testId: number;
    questionIndex: number;
    status: AnswerStatusEnum;
    answers: TestAnswer[];
}
