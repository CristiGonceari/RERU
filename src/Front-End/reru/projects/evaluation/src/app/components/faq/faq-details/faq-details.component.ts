import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticlesService } from '../../../utils/services/articles/articles.service';
import { Location } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';


@Component({
  selector: 'app-faq-details',
  templateUrl: './faq-details.component.html',
  styleUrls: ['./faq-details.component.scss']
})
export class FaqDetailsComponent implements OnInit {editorData: string;
  title: string;
  articleId: number;
  isLoading: boolean = true;

  constructor(
    private articleService: ArticlesService,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private notificationService: NotificationsService,
    public router: Router,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.articleId = params.id;
      this.getArticle();
		});
  }

  getArticle(){
    this.articleService.get(this.articleId).subscribe(
      (res) => {
        if(res && res.data) {
          this.title = res.data.name;
          this.editorData = res.data.content;
          this.isLoading = false;
        }
    })
  }

  backClicked() {
		this.location.back();
	}

  deleteArticle(id): void{
		this.articleService.delete(id).subscribe(() => 
		{
			this.notificationService.success('Success', 'Article was successfully deleted', NotificationUtil.getDefaultMidConfig());
      this.router.navigate(['/faq']);
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this article?';
		modalRef.result.then(() => this.deleteArticle(id), () => { });
	}

}
