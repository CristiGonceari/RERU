import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-search-by-question-type',
  templateUrl: './search-by-question-type.component.html',
  styleUrls: ['./search-by-question-type.component.scss']
})
export class SearchByQuestionTypeComponent {

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  questionTypeList: any;

  constructor(private referenceService: ReferenceService) { this.getQuestionType(); }

  getQuestionType(){
    this.referenceService.getQuestionType().subscribe(res => this.questionTypeList = res.data);
  }
}
