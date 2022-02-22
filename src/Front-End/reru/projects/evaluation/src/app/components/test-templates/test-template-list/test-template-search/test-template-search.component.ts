import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-test-template-search',
  templateUrl: './test-template-search.component.html',
  styleUrls: ['./test-template-search.component.scss']
})
export class TestTemplateSearchComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  testStatusesList;
  constructor(private referenceService: ReferenceService) { this.getTestStatuses(); }

  getTestStatuses() {
    this.referenceService.getTestTemplateStatuses().subscribe(res => this.testStatusesList = res.data);
  }

}
