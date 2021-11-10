import { Component, OnInit } from '@angular/core';
import { SequenceTypeEnum } from 'projects/evaluation/src/app/utils/enums/sequence-type.enum';
import { SelectItem } from 'projects/evaluation/src/app/utils/models/select-item.model';
import { SelectionTypeEnum } from 'projects/evaluation/src/app/utils/enums/selection-type.enum';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {
  testTypeId: number;
  questionCategoryId: number;
  questionType: number;
  questionCount: number;
  questionCategoryValueList: SelectItem[] = [{ label: '', value: '' }];
  questionTypeEnumList: SelectItem[] = [{ label: '', value: '' }];

  selectQuestions = SelectionTypeEnum.All;
  orderQuestions = SequenceTypeEnum.Random;
  tableData: any;
  setLimit: boolean;
  timeLimit: number;

  constructor(private route: ActivatedRoute, private referenceService: ReferenceService) { }

  ngOnInit(): void {
    this.subsribeForParams();
    this.getQuestionCategoryValueForDropdown();
    this.getQuestionTypeValueForDropdown();
  }

  subsribeForParams(): void {
    this.testTypeId = +this.route.snapshot.paramMap.get('id');
  }

  getQuestionCategoryValueForDropdown() {
    this.referenceService.getQuestionCategory().subscribe(res => {
      this.questionCategoryValueList = res.data;
    });
  }

  getQuestionTypeValueForDropdown() {
    this.referenceService.getQuestionTypeStatuses().subscribe(res => this.questionTypeEnumList = res.data);
  }

  onSelect() {
    this.tableData = {
      questionCount: this.questionCount,
      questionCategoryId: this.questionCategoryId,
      sequenceType: this.orderQuestions,
      selectQuestions: this.selectQuestions,
      type: this.questionType
    }
  }
}
