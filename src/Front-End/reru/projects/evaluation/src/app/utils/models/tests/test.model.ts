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
    isEvaluator: boolean;
    testStatus: TestStatusEnum;
    modeStatus: TestTypeModeEnum;
    result: TestResultStatusEnum;
    programmedTime: string;
    endTime?: string;
    viewTestResult?: boolean;
}

export class CreateTestModel {
    id?: number;
    userProfileId: number;
    testTypeId: number;
    eventId?: number;
    testStatus: number;
    programmedTime: string;
    evaluatorId?: number;
    showUserName?: string;

    constructor(testModel?: CreateTestModel) {
        if (testModel) {
            this.id = testModel.id;
            this.userProfileId = testModel.userProfileId;
            this.eventId = testModel.eventId;
            this.programmedTime = testModel.programmedTime;
            this.testStatus = testModel.testStatus;
            this.testTypeId = testModel.testTypeId;
            this.evaluatorId = testModel.evaluatorId;
            this.showUserName = testModel.showUserName;
        } else {
            this.id = null;
            this.userProfileId = null;
            this.programmedTime = null;
            this.testStatus = null;
            this.testTypeId = null;
            this.eventId = null;
            this.evaluatorId = null;
            this.showUserName = null;
        }
    }
}
