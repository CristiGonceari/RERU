import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from '../../../services/reference.service';

@Component({
  selector: 'app-search-function',
  templateUrl: './search-function.component.html',
  styleUrls: ['./search-function.component.scss']
})
export class SearchFunctionComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  function: string = '';
  constructor(private referenceService: ReferenceService) { this.getStatuses(); }

  getStatuses(): void {
    this.referenceService.listFunctions().subscribe(res => this.list = res.data);
  }

}
