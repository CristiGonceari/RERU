import { AnswerStatusEnum } from "../../enums/answer-status.enum";
import { TestAnswer } from "./test-answer.model";

export class AddTestQuestion {
    testId: number;
    questionIndex: number;
    status: AnswerStatusEnum;
    answers: TestAnswer[];
    
    constructor(addTestQuestion?: AddTestQuestion) {
        if (addTestQuestion) {
            this.testId = addTestQuestion.testId;
            this.questionIndex = addTestQuestion.questionIndex;
            this.status = addTestQuestion.status;
            this.answers = addTestQuestion.answers;
        } else {
            this.testId = null;
            this.questionIndex = null;
            this.status = null;
            this.answers = null;
        }
    }
}
