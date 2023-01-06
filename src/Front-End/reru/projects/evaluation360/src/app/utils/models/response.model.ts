import { PaginationModel } from './pagination.model';

export interface Response<T> {
    data: T;
    messages: any[];
    success: boolean;
}

export interface ResponseArray<T> {
    data: {
        items: T[];
        pagedSummary: PaginationModel;
    }
    messages: any[];
    success: boolean;
}