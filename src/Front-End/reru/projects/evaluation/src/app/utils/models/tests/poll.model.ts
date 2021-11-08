import { MyPollStatusEnum } from "../../enums/my-poll-status.enum";
import { TestStatusEnum } from "../../enums/test-status.enum";
import { TestTypeStatusEnum } from "../../enums/test-type-status.enum";

export class Poll {
    id: number;
    testTypeName: string;
    status: MyPollStatusEnum;
    testStatus?: TestStatusEnum;
    testTypeStatus: TestTypeStatusEnum;
    settings: boolean;
    votedTime?: string;
    startTime: string;
    endTime: string;
}
