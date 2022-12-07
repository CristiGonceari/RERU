import { TestStatusEnum } from "../../enums/test-status.enum";

export class AddEditTest {
    id?: number;
    userProfileIds: number[];
    evaluatorIds?: any;
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
            this.userProfileIds = testModel.userProfileIds;
            this.eventId = testModel.eventId;
            this.programmedTime = testModel.programmedTime;
            this.testStatus = testModel.testStatus;
            this.testTemplateId = testModel.testTemplateId;
            this.evaluatorIds = testModel.evaluatorIds;
            this.showUserName = testModel.showUserName;
            this.processId = testModel.processId;
        } else {
            this.id = null;
            this.userProfileIds = null;
            this.programmedTime = null;
            this.testStatus = null;
            this.testTemplateId = null;
            this.eventId = null;
            this.evaluatorIds = null;
            this.showUserName = null;
        }
    }
}
