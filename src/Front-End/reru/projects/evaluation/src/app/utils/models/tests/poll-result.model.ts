import { PollQuestion } from "./poll-question.model";

export class PollResult {
    id: number;
    testTypeName: string;
    eventName: string;
    totalInvited?: number;
    totalVotedCount: number;
    totalVotedPercent: number;
    itemsCount: number;
    startDate: string;
    endDate: string;
    questions: PollQuestion[];
}
