import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleModel } from '../../../../utils/models/article.model';
import { PaginationSummary } from '../../../../utils/models/pagination-summary.model';
import { ArticlesService } from '../../../../utils/services/articles.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { PrintModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';
import { saveAs } from 'file-saver';

@Component({
	selector: 'app-faq-list-table',
	templateUrl: './faq-list-table.component.html',
	styleUrls: ['./faq-list-table.component.scss']
})
export class FaqListTableComponent implements OnInit {
	articles: ArticleModel;
	keyword: string;
	pagedSummary: PaginationSummary = new PaginationSummary();
	isLoading: boolean = true;
	title: string;
	description: string;
	no: string;
	yes: string;
	downloadFile: boolean = false;
	headersToPrint = [];
	printTranslates: any[];

	constructor(private articleService: ArticlesService,
		private route: ActivatedRoute,
		public translate: I18nService,
		private router: Router,
		private notificationService: NotificationsService,
		private modalService: NgbModal) { }

	ngOnInit(): void {
		this.list();
	}

	list(data: any = {}) {
		this.keyword = data.keyword;
		let params = {
			name: this.keyword || '',
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		}

		this.articleService.getList(params).subscribe(res => {
			if (res && res.data) {
				this.articles = res.data.items;
				this.pagedSummary = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	getHeaders(name: string): void {
		this.translateData();
		let headersHtml = document.getElementsByTagName('th');
		let headersDto = ['name', 'content'];
		for (let i=0; i<headersHtml.length-1; i++) {
			this.headersToPrint.push({ value: headersDto[i], label: headersHtml[i].innerHTML })
		}
		let printData = {
			tableName: name,
			fields: this.headersToPrint,
			orientation: 2,
			name: this.keyword || ''
		};
		const modalRef: any = this.modalService.open(PrintModalComponent, { centered: true, size: 'lg' });
		modalRef.componentInstance.tableData = printData;
		modalRef.componentInstance.translateData = this.printTranslates;
		modalRef.result.then(() => this.printTable(modalRef.result.__zone_symbol__value), () => { });
		this.headersToPrint = [];
	}

	translateData(): void {
		this.printTranslates = ['print-table', 'print-msg', 'sorted-by', 'cancel']
		forkJoin([
			this.translate.get('print.print-table'),
			this.translate.get('print.print-msg'),
			this.translate.get('print.sorted-by'),
			this.translate.get('button.cancel')
		]).subscribe(
			(items) => {
				for (let i=0; i<this.printTranslates.length; i++) {
					this.printTranslates[i] = items[i];
				}
			}
		);
	}

	printTable(data): void {
		this.downloadFile = true;
		this.articleService.print(data).subscribe(response => {
			if (response) {
				const fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
				const blob = new Blob([response.body], { type: response.body.type });
				const file = new File([blob], fileName, { type: response.body.type });
				saveAs(file);
				this.downloadFile = false;
			}
		}, () => this.downloadFile = false);
	}

	navigate(id) {
		this.router.navigate(['faq-details/', id, 'overview'], { relativeTo: this.route });
	}

	deleteArticle(id): void {
		this.articleService.delete(id).subscribe(() => {
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('faq.succes-remove-msg'),
			]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
			});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		forkJoin([
			this.translate.get('faq.remove'),
			this.translate.get('faq.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
		});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteArticle(id), () => { });
	}
}