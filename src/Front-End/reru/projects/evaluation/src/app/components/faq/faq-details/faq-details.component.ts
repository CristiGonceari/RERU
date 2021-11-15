import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticlesService } from '../../../utils/services/articles/articles.service';
import { Location } from '@angular/common';


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

}
