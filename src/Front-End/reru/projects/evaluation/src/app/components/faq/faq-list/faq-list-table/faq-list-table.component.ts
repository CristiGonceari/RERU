import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleModel } from '../../../../utils/models/article.model';
import { PaginationModel } from '../../../../utils/models/pagination.model';
import { ArticlesService } from '../../../../utils/services/articles/articles.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';

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

  constructor(private articleService: ArticlesService,
	private route: ActivatedRoute,
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
			this.notificationService.success('Success', 'Article was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this article?';
		modalRef.result.then(() => this.deleteArticle(id), () => { });
	}
}
