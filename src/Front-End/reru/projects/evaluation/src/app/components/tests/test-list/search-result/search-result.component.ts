import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.scss']
})
export class SearchResultComponent{

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  testResultsList;
  constructor(private referenceService: ReferenceService) { this.getTestResults(); }

  getTestResults() {
    this.referenceService.getTestResults().subscribe(res => this.testResultsList = res.data);
  }

}
