import { Component, OnInit } from '@angular/core';
import { SequenceTypeEnum } from 'projects/evaluation/src/app/utils/enums/sequence-type.enum';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { SelectionTypeEnum } from 'projects/evaluation/src/app/utils/enums/selection-type.enum';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { ActivatedRoute } from '@angular/router';
import { TestTemplateService } from 'projects/evaluation/src/app/utils/services/test-template/test-template.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {
  testTemplateId: number;
  isGridTest: boolean = false;
  questionCategoryId: number;
  questionType: number;
  questionCount: number;
  questionCategoryValueList: SelectItem[] = [{ label: '', value: '' }];
  questionTypeEnumList: SelectItem[] = [{ label: '', value: '' }];

  selectQuestions = SelectionTypeEnum.All;
  orderQuestions = SequenceTypeEnum.Random;
  tableData: any = {
    type: null,
    questionCategoryId: null
  };
  setLimit: boolean;
  timeLimit: number;

  isButtonDisabled: boolean = true;

  constructor(private route: ActivatedRoute,
    private referenceService: ReferenceService,
    private testTemplateService: TestTemplateService) { }

  ngOnInit(): void {
    this.subsribeForParams();
    this.getQuestionCategoryValueForDropdown();
    this.getQuestionTypeValueForDropdown();
  }

  subsribeForParams(): void {
    this.testTemplateId = +this.route.snapshot.paramMap.get('id');

    this.testTemplateService.getTestTemplate(this.testTemplateId).subscribe(res => {
      this.isGridTest = res.data.isGridTest;
    });
  }

  getQuestionCategoryValueForDropdown() {
    this.referenceService.getQuestionCategory().subscribe(res => {
      this.questionCategoryValueList = res.data;
    });
  }

  getQuestionTypeValueForDropdown() {
    this.referenceService.getQuestionType().subscribe(res => this.questionTypeEnumList = res.data);
  }

  onSelect() {
    if (this.isGridTest) {
      this.questionTypeEnumList = this.questionTypeEnumList.filter(
        question => question.value === '2' || question.value === '3'
      );
    }

    this.tableData.type = this.questionType;
    this.tableData.questionCategoryId = this.questionCategoryId;

    if (this.questionCategoryId && this.questionType) {
      this.isButtonDisabled = false;
    }
    
    if (!this.isGridTest) this.isButtonDisabled = false;
  }
}
