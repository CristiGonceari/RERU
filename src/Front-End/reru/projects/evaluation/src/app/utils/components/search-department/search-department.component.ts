import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from '../../services/reference/reference.service';

@Component({
  selector: 'app-search-department',
  templateUrl: './search-department.component.html',
  styleUrls: ['./search-department.component.scss']
})
export class SearchDepartmentComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  department: string = '';
  constructor(private referenceService: ReferenceService) { this.getStatuses(); }

  getStatuses() {
    this.referenceService.getDepartments().subscribe(res => this.list = res.data);
  }
}