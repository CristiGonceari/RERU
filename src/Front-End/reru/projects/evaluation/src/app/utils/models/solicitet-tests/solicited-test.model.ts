import { SolicitedTestStatusEnum } from "../../enums/solicited-test-status.model";

export class SolicitedTest {
    id?: number;
    userProfileId?: number;
    userProfileName: string;
    testTemplateId: number;
    testTemplateName: string;
    eventId?: number;
    eventName: string;
    solicitedTime: string;
    solicitedTestStatus: SolicitedTestStatusEnum;
}