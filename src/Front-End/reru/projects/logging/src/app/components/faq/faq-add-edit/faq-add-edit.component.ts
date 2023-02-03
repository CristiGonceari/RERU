import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { Location } from '@angular/common';
import { ArticlesService } from '../../../utils/services/articles/articles.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';

@Component({
  selector: 'app-faq-add-edit',
  templateUrl: './faq-add-edit.component.html',
  styleUrls: ['./faq-add-edit.component.scss']
})
export class FaqAddEditComponent implements OnInit {
  editorData: string = '';
  title: string;
  articleId: number;
  isLoading: boolean = true;
  public Editor = DecoupledEditor;
  public onReady(editor) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }

  title1: string;
  description: string;
  no: string;
  yes: string;

  constructor(
    private articleService: ArticlesService,
    private activatedRoute: ActivatedRoute,
	  public translate: I18nService,
    private location: Location,
		private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.articleId = params.id;
    });
    if (this.articleId) {
      this.getArticle();
    } else {
      this.isLoading = false;
    }
  }

  checkConfirmButton(article: string, title: string){
    if (article !== '' && title !== ''){
      return false
    }
    return true;
  }

  getArticle(): void {
    this.articleService.get(this.articleId).subscribe((res) => {
      if (res && res.data) {
        this.title = res.data.name;
        this.editorData = res.data.content;
        this.isLoading = false;
      }
    })
  }

  saveArticle(): void {
    const createData = {
      name: this.title,
      content: this.editorData
    }

    const editData = {
      id: this.articleId,
      name: this.title,
      content: this.editorData
    }

    if (this.articleId) {
      this.articleService.create(editData).subscribe(() => {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('faq.succes-edit-msg'),
          ]).subscribe(([title1, description]) => {
          this.title1 = title1;
          this.description = description;
          });
        this.backClicked();
			  this.notificationService.success(this.title1, this.description, NotificationUtil.getDefaultMidConfig());
      });
    } else {
      this.articleService.create(createData).subscribe(() => {
        forkJoin([
          this.translate.get('modal.success'),
          this.translate.get('faq.succes-add-msg'),
          ]).subscribe(([title1, description]) => {
          this.title1 = title1;
          this.description = description;
          });
        this.backClicked();
			  this.notificationService.success(this.title1, this.description, NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  backClicked() {
		this.location.back();
	}
}
