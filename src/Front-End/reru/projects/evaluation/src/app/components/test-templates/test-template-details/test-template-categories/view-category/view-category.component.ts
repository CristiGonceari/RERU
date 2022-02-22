import { Component, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestCategoryQuestionService } from 'projects/evaluation/src/app/utils/services/test-category-questions/test-category-question.service';
import { QuestionUnitStatusEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-status.enum';
import { QuestionUnitTypeEnum } from 'projects/evaluation/src/app/utils/enums/question-unit-type.enum';
import { SequenceTypeEnum } from 'projects/evaluation/src/app/utils/enums/sequence-type.enum';

@Component({
  selector: 'app-view-category',
  templateUrl: './view-category.component.html',
  styleUrls: ['./view-category.component.scss']
})
export class ViewCategoryComponent implements OnInit {
  url
  categories:[] = [];
  @Input() testTemplateId;
  @Input() questionType;
  categoriesQuestions = [];
  questionEnum = QuestionUnitStatusEnum;
  type = QuestionUnitTypeEnum;
  sequence = SequenceTypeEnum;
  categoriesName
  usedQuestionCount
  sequenceType

  constructor(private router: Router,
    private service: TestCategoryQuestionService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.url =this.router.url.split("/").pop();
    this.List(this.url);
  }

  close(){
    this.router.navigate(['../../categories'], {relativeTo:this.route});
  }

  List(id?: number) {
    let categoryId = this.url ? this.url : id;
    this.service.listOfQuestion({ TestTemplateQuestionCategoryId: categoryId }).subscribe(
      res => {
        if (res && res.data) {
          this.categoriesName = res.data.questionCategoryName;
          this.usedQuestionCount = res.data.usedQuestionCount;
          this.sequenceType = res.data.sequenceType;
          this.categoriesQuestions =res.data.questions;
        }
      }
    )
  }
  


}
