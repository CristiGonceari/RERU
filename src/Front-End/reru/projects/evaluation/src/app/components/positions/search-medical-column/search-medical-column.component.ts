import { Component, Output, EventEmitter } from '@angular/core';
import { ReferenceService } from '../../../utils/services/reference/reference.service';

@Component({
  selector: 'app-search-medical-column',
  templateUrl: './search-medical-column.component.html',
  styleUrls: ['./search-medical-column.component.scss']
})
export class SearchMedicalColumnComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  column: string = '';
  constructor(private referenceService: ReferenceService) { this.get(); }

  get() {
    this.referenceService.getMedicalColumnEnum().subscribe(res => this.list = res.data);
  }
}
