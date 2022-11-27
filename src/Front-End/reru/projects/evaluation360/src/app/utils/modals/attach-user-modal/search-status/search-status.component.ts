import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from '../../../services/reference.service';

@Component({
  selector: 'app-search-status',
  templateUrl: './search-status.component.html',
  styleUrls: ['./search-status.component.scss']
})
export class SearchStatusComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  statusesList;
  userStatus: string = '';
  constructor(private referenceService: ReferenceService) { this.getStatuses(); }

  getStatuses(): void {
    this.referenceService.listUserStatuses().subscribe(res => this.statusesList = res.data);
  }
}
