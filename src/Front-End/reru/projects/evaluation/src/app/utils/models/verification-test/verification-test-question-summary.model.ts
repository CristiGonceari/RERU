import { VerificationStatusEnum } from "../../enums/verification-status.enum";

export class VerificationTestQuestionSummary {
    index: number;
    verificationStatus: VerificationStatusEnum;
    isCorrect: boolean;
}
