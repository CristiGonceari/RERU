import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from 'projects/evaluation/src/app/utils/services/reference/reference.service';

@Component({
  selector: 'app-search-qualifying-type',
  templateUrl: './search-qualifying-type.component.html',
  styleUrls: ['./search-qualifying-type.component.scss']
})
export class SearchQualifyingTypeComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  statusesList;
  modeStatus: string = '';
  constructor(private referenceService: ReferenceService) { this.getStatuses(); }

  getStatuses() {
    this.referenceService.getQualifyingType().subscribe(res => this.statusesList = res.data);
  }
}
