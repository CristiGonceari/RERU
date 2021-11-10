import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-test-type-search',
  templateUrl: './test-type-search.component.html',
  styleUrls: ['./test-type-search.component.scss']
})
export class TestTypeSearchComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  testStatusesList;
  constructor(private referenceService: ReferenceService) { this.getTestStatuses(); }

  getTestStatuses() {
    this.referenceService.getTestTypeStatuses().subscribe(res => this.testStatusesList = res.data);
  }

}
