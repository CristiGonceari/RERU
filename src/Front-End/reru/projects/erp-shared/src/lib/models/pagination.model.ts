export interface PaginationModel {
	currentPage: number;
	pageSize: number;
	totalCount: number;
	totalPages: number;
}

export class PaginationClass implements PaginationModel {
	currentPage: number = 1;
	pageSize: number = 10;
	totalCount: number;
	totalPages: number;
}
