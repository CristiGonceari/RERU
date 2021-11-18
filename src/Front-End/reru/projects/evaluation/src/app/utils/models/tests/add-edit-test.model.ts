import { TestStatusEnum } from "../../enums/test-status.enum";

export class AddEditTest {
    id?: number;
    userProfileId: number;
    evaluatorId?: number;
    showUserName?: boolean;
    testTypeId: number;
    eventId?: number;
    locationId?: number;
    testStatus: TestStatusEnum;
    programmedTime: string;

    constructor(testModel?: AddEditTest) {
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
