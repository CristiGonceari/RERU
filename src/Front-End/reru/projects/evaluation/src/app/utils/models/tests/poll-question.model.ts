import { PollOption } from "./poll-option.model";

export class PollQuestion {
    testTemplateId: number;
    index: number;
    question: string;
    votedCount?: number;
    votedPercent: number;
    options: PollOption[];
}
