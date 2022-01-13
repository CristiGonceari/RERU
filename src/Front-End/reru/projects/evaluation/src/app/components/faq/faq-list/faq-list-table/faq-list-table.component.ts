import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleModel } from '../../../../utils/models/article.model';
import { PaginationModel } from '../../../../utils/models/pagination.model';
import { ArticlesService } from '../../../../utils/services/articles/articles.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-faq-list-table',
  templateUrl: './faq-list-table.component.html',
  styleUrls: ['./faq-list-table.component.scss']
})
export class FaqListTableComponent implements OnInit {
  articles: ArticleModel;
  keyword: string;
  pagedSummary: PaginationModel = new PaginationModel();
  isLoading: boolean = true;
  title: string;
  description: string;
  no: string;
  yes: string;

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

		this.articleService.getList(params).subscribe( res => {
			if (res && res.data) {
				this.articles = res.data.items;
				this.pagedSummary = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	navigate(id){
		this.router.navigate(['faq-details/', id, 'overview'], {relativeTo: this.route});
	}
	
	deleteArticle(id): void{
		this.articleService.delete(id).subscribe(() => 
		{
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
