import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { AnswerStatusEnum } from "../../enums/answer-status.enum";

export class TestQuestionSummary {
    index: number;
    answerStatus: AnswerStatusEnum;
    isClosed: boolean;
    questionType: QuestionUnitTypeEnum;
}
