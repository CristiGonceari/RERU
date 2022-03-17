import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ArticlesService } from '../../../utils/services/articles.service';
import { ArticleModel } from '../../../utils/models/article.model';
import { ConfirmModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  article: ArticleModel;
  title: string;
  description: string;
  no: string;
  yes: string;

  public Editor = DecoupledEditor;

  constructor(private articleService: ArticlesService,
    private route: ActivatedRoute,
    private router: Router,
    private ngZone: NgZone,
    private modalService: NgbModal,
    public translate: I18nService,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id && !isNaN(response.id)) {
        this.retrieveDepartment(response.id);
      } else {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
      }
    });
  }

  retrieveDepartment(id: number): void {
    this.articleService.get(id).subscribe(response => {
      if (!response) {
        this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        return;
      }
      this.article = response.data;
      this.isLoading = false;
    });
  }

  deleteArticle(id): void{
		this.articleService.delete(id).subscribe(() => 
		{
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('faq.succes-remove-msg'),
			  ]).subscribe(([title1, description]) => {
				this.title = title1;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
      this.router.navigate(['/faq']);
		});
	}

	openConfirmationDeleteModal(id): void {
    forkJoin([
			this.translate.get('faq.remove'),
			this.translate.get('faq.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title1, description, no, yes]) => {
			this.title = title1;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title1 = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteArticle(id), () => { });
	}
}
