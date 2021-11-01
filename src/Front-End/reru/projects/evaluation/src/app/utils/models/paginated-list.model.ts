import { PaginationModel } from './pagination.model';

export class PaginatedListModel<T> {
	data: {
		items: T[];
		pagedSummary: PaginationModel;
	};
}
