import { TestStatusEnum } from "../../enums/test-status.enum";

export class AddEditTest {
    id: number;
    userProfileId: number;
    evaluatorId?: number;
    showUserName?: boolean;
    testTypeId: number;
    eventId?: number;
    locationId?: number;
    testStatus: TestStatusEnum;
    programmedTime: string;
}
