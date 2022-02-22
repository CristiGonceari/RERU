import { MyPollStatusEnum } from "../../enums/my-poll-status.enum";
import { TestStatusEnum } from "../../enums/test-status.enum";
import { TestTemplateStatusEnum } from "../../enums/test-Template-status.enum";

export class Poll {
    id: number;
    testTemplateName: string;
    status: MyPollStatusEnum;
    testStatus?: TestStatusEnum;
    testTemplateStatus: TestTemplateStatusEnum;
    settings: boolean;
    votedTime?: string;
    startTime: string;
    endTime: string;
}
