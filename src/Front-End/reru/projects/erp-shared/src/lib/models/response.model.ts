import { Message } from './message.model';
import { PaginationModel } from './pagination.model';

export interface Response<T> {
    data: T;
    messages: Message[];
    success: boolean;
}

export interface ResponseArray<T> {
    data: {
        items: T[];
        pagedSummary: PaginationModel;
    }
    messages: Message[];
    success: boolean;
}