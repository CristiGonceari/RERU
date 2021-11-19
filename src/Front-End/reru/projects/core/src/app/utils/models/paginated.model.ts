import { PaginationSummary } from './pagination-summary.model';

export class Paginated<T> {
  items: T[];
  pagedSummary: PaginationSummary;
}
