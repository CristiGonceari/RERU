import { TestPassStatusEnum } from "../../enums/test-pass-status.enum";
import { TestResultStatusEnum } from "../../enums/test-result-status.enum";
import { TestStatusEnum } from "../../enums/test-status.enum";
import { TestTemplateModeEnum } from "../../enums/test-template-mode.enum";

export class Test {
    id: number;
    userId: number;
    evaluatorId?: number;
    testTemplateId: number;
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
    testTemplateName: string;
    rules: string;
    verificationProgress: string;
    showUserName: boolean;
    isEvaluator: boolean;
    testStatus: TestStatusEnum;
    modeStatus: TestTemplateModeEnum;
    result: TestResultStatusEnum;
    programmedTime: string;
    endTime?: string;
    viewTestResult?: boolean;
    idnp: string;
    candidatePositionNames: any[];
    canStartWithoutConfirmation: boolean; 
}

export class CreateTestModel {
    id?: number;
    userProfileId: number;
    testTemplateId: number;
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
            this.testTemplateId = testModel.testTemplateId;
            this.evaluatorId = testModel.evaluatorId;
            this.showUserName = testModel.showUserName;
        } else {
            this.id = null;
            this.userProfileId = null;
            this.programmedTime = null;
            this.testStatus = null;
            this.testTemplateId = null;
            this.eventId = null;
            this.evaluatorId = null;
            this.showUserName = null;
        }
    }
}
