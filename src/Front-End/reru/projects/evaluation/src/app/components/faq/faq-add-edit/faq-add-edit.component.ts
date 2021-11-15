import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { Location } from '@angular/common';
import { ArticlesService } from '../../../utils/services/articles/articles.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';

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

  constructor(
    private articleService: ArticlesService,
    private activatedRoute: ActivatedRoute,
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
        this.backClicked();
			  this.notificationService.success('Success', 'Article was successfully updated', NotificationUtil.getDefaultMidConfig());
      });
    } else {
      this.articleService.create(createData).subscribe(() => {
        this.backClicked();
			  this.notificationService.success('Success', 'Article was successfully added', NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  backClicked() {
		this.location.back();
	}
}
