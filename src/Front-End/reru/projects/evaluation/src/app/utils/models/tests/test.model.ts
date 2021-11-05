import { TestPassStatusEnum } from "../../enums/test-pass-status.enum";
import { TestResultStatusEnum } from "../../enums/test-result-status.enum";
import { TestStatusEnum } from "../../enums/test-status.enum";
import { TestTypeModeEnum } from "../../enums/test-type-mode.enum";

export class Test {
    id: number;
    userId: number;
    evaluatorId?: number;
    testTypeId: number;
    eventName: string;
    eventId: number;
    locationName: string;
    testPassStatus: TestPassStatusEnum;
    maxErrors?: number;
    duration: number;
    minPercent: number;
    questionCount: number;
    accumulatedPercentage: number;
    userName: string;
    testTypeName: string;
    rules: string;
    verificationProgress: string;
    showUserName: boolean;
    testStatus: TestStatusEnum;
    modeStatus: TestTypeModeEnum;
    result: TestResultStatusEnum;
    programmedTime: string;
    endTime?: string;
}
