import { QuestionUnitTypeEnum } from "../../enums/question-unit-type.enum";
import { VerificationTestOptions } from "./verification-test-options.model";

export class VerificationTestQuestionUnit {
    id: number;
    questionCategoryId: number
    questionUnitId: number
    answersCount: number
    verified: number;
    question: string;
    correctHashedQuestion: string;
    categoryName: string;
    answerText: string;
    comment: string;
    isCorrect?: boolean;
    questionMaxPoints: number;
    evaluatorPoints: number;
    questionType: QuestionUnitTypeEnum;
    options: VerificationTestOptions[];
    hashedOptions: VerificationTestOptions[];
}
