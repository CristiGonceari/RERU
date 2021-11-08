import { PollOption } from "./poll-option.model";

export class PollQuestion {
    questionId: number;
    index: number;
    question: string;
    votedCount?: number;
    votedPercent: number;
    options: PollOption[];
}
