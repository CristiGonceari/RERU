import { PagedSummary } from './paged-summary.model';

export interface ApiResponse<T> {
    data?: T;
    success: boolean;
    items?: T[];
    pagedSummary?: PagedSummary;
    messages: ErrorMessage[];
}

export interface ListResponse<T> {
    items?: T[];
    pagedSummary?: PagedSummary;
}

interface ErrorMessage {
    code: string;
    messageText: string;
    type: number;
    data: ErrorData[];
}

interface ErrorData {
    [key: string]: string;
}
