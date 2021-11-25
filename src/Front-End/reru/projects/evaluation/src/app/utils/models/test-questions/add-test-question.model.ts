import { AnswerStatusEnum } from "../../enums/answer-status.enum";
import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { TestAnswer } from "./test-answer.model";

export class AddTestQuestion {
    testId: number;
    questionIndex: number;
    questionUnitId: number;
    status: AnswerStatusEnum;
    answers: TestAnswer[];
    questionType?: QuestionUnitTypeEnum;
    
    constructor(addTestQuestion?: AddTestQuestion) {
        if (addTestQuestion) {
            this.testId = addTestQuestion.testId;
            this.questionIndex = addTestQuestion.questionIndex;
            this.questionUnitId = addTestQuestion.questionUnitId;
            this.status = addTestQuestion.status;
            this.answers = addTestQuestion.answers;
        } else {
            this.testId = null;
            this.questionIndex = null;
            this.questionUnitId = null;
            this.status = null;
            this.answers = null;
        }
    }
}
