import { TestStatusEnum } from "../../enums/test-status.enum";

export class AddEditTest {
    id?: number;
    userProfileId: number[];
    evaluatorId?: any;
    showUserName?: boolean;
    testTemplateId: any;
    eventId?: number;
    locationId?: number;
    testStatus: TestStatusEnum;
    programmedTime: string;
    processId?: number;

    constructor(testModel?: AddEditTest) {
        if (testModel) {
            this.id = testModel.id;
            this.userProfileId = testModel.userProfileId;
            this.eventId = testModel.eventId;
            this.programmedTime = testModel.programmedTime;
            this.testStatus = testModel.testStatus;
            this.testTemplateId = testModel.testTemplateId;
            this.evaluatorId = testModel.evaluatorId;
            this.showUserName = testModel.showUserName;
            this.processId = testModel.processId;
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
