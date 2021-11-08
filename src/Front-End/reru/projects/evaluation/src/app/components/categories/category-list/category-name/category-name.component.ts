import { Component, Input, OnInit } from '@angular/core';
import { QuestionCategory } from 'projects/evaluation/src/app/utils/models/question-category/question-category.model';

@Component({
  selector: 'app-category-name',
  templateUrl: './category-name.component.html',
  styleUrls: ['./category-name.component.scss']
})
export class CategoryNameComponent implements OnInit {

  @Input() category: QuestionCategory;
  
  constructor() { }

  ngOnInit(): void {
  }

}
