import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticlesService } from '../../../utils/services/articles.service';
import { Location } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { ConfirmModalComponent } from '@erp/shared';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n.service';

@Component({
  selector: 'app-faq-details',
  templateUrl: './faq-details.component.html',
  styleUrls: ['./faq-details.component.scss']
})
export class FaqDetailsComponent implements OnInit {editorData: string;
  title: string;
  articleId: number;
  isLoading: boolean = true;
  title1: string;
  description: string;
  no: string;
  yes: string;

  constructor(
    private articleService: ArticlesService,
    private activatedRoute: ActivatedRoute,
	  public translate: I18nService,
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
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('faq.succes-remove-msg'),
			  ]).subscribe(([title1, description]) => {
				this.title1 = title1;
				this.description = description;
				});
			this.notificationService.success(this.title1, this.description, NotificationUtil.getDefaultMidConfig());
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
			this.title1 = title1;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title1 = this.title1;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteArticle(id), () => { });
	}

}
