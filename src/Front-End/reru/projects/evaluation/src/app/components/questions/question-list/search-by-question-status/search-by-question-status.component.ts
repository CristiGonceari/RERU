import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-search-by-question-status',
  templateUrl: './search-by-question-status.component.html',
  styleUrls: ['./search-by-question-status.component.scss']
})
export class SearchByQuestionStatusComponent {

  @Output() filter: EventEmitter<string> = new EventEmitter<string>();
  questionStatusList: any;

  constructor(private referenceService: ReferenceService) { this.getQuestionStatus(); }

  getQuestionStatus() {
    this.referenceService.getQuestionStatus().subscribe(res => this.questionStatusList = res.data);
  }

}
