export class ArticleModel {
	id?: number;
	name: string;
	content: string;
	fileDto: {
		file: File;
		type: string;
	}
	roles: [] = [];
}
