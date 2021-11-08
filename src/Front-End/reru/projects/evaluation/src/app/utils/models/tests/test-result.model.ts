import { TestResultStatusEnum } from "../../enums/test-result-status.enum";
import { TestStatusEnum } from "../../enums/test-status.enum";

export class TestResult {
    minPercent: number;
    accumulatedPercentage: number;
    status: TestStatusEnum;
    result: TestResultStatusEnum;
}
