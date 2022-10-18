import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticlesService } from 'projects/evaluation/src/app/utils/services/articles/articles.service';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';

@Component({
  selector: 'app-faq-overview',
  templateUrl: './faq-overview.component.html',
  styleUrls: ['./faq-overview.component.scss']
})
export class FaqOverviewComponent implements OnInit {
  articleId;
  articleName;
  content: string ='';
  isDisabled = false;
  isLoading: boolean = true;
  fileId: string;
  roles = [];

  public Editor = DecoupledEditor;

  constructor(
		private faqService: ArticlesService,
		private activatedRoute: ActivatedRoute
  ) {  }
  
  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.articleId = params.id;
			if (this.articleId) {
        this.getArticle();
    }});
	}

  getArticle(){
    this.faqService.get(this.articleId).subscribe(res => {
      if (res && res.data) {
        this.articleName = res.data.name;
        this.content = res.data.content;
        this.fileId = res.data.mediaFileId;
        if(res.data.mediaFileId == "null") this.fileId = null;
        if(res.data.roles.length > 0)
          this.roles = res.data.roles.map(e => e.label.replace(/\s/g, "")).join(", ");
        this.isLoading = false;
      }
    })
  }
}
