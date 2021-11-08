import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionCategoryService } from 'projects/evaluation/src/app/utils/services/question-category/question-category.service';

@Component({
  selector: 'app-category-overview',
  templateUrl: './category-overview.component.html',
  styleUrls: ['./category-overview.component.scss']
})
export class CategoryOverviewComponent implements OnInit {

  categoryId: number;
  categoryName: string;
  isLoading: boolean = true;

  constructor(
    private categoryService: QuestionCategoryService,
		private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  getCategory(){
    this.categoryService.get(this.categoryId).subscribe(res => {
      if (res && res.data) {
        this.categoryName = res.data.name;
        this.isLoading = false;
      }
    });
  }

  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.categoryId = params.id;
			if (this.categoryId) {
        this.getCategory();
    }});
	}
}
