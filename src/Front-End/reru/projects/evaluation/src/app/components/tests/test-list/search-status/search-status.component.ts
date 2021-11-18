import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-search-status',
  templateUrl: './search-status.component.html',
  styleUrls: ['./search-status.component.scss']
})
export class SearchStatusComponent {

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  testStatusesList;
  constructor(private referenceService: ReferenceService) { this.getTestStatuses(); }

  getTestStatuses() {
    this.referenceService.getTestStatuses().subscribe(res => this.testStatusesList = res.data);
  }

}
