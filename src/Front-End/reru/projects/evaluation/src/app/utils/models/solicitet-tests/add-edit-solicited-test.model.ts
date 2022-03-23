import { SolicitedTestStatusEnum } from "../../enums/solicited-test-status.model";

export class AddEditSolicitedTest {
    id?: number;
    userProfileId?: number;
    testTemplateId: number;
    eventId?: number;
    solicitedTime: string;
    solicitedTestStatus: SolicitedTestStatusEnum;
}