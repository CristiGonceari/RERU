import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-search-test-mode',
  templateUrl: './search-test-mode.component.html',
  styleUrls: ['./search-test-mode.component.scss']
})
export class SearchTestModeComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  statusesList;
  modeStatus: string = '';
  constructor(private referenceService: ReferenceService) { this.getStatuses(); }

  getStatuses() {
    this.referenceService.getMode().subscribe(res => this.statusesList = res.data);
  }
}
