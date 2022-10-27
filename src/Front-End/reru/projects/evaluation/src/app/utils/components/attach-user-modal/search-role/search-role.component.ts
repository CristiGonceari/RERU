import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from '../../../services/reference/reference.service';

@Component({
  selector: 'app-search-role',
  templateUrl: './search-role.component.html',
  styleUrls: ['./search-role.component.scss']
})
export class SearchRoleComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  role: string = '';
  constructor(private referenceService: ReferenceService) { this.getStatuses(); }

  getStatuses() {
    this.referenceService.getRoles().subscribe(res => this.list = res.data);
  }
}
