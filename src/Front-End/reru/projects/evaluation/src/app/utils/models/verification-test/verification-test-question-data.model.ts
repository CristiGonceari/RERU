import { VerificationTestQuestionSummary } from "./verification-test-question-summary.model";

export class VerificationTestQuestionData {
    testQuestions: VerificationTestQuestionSummary[];
    correctAnswers: number;
    totalQuestions: number;
    points: number;
    result: number;
}
