import { Component, EventEmitter, Output } from '@angular/core';
import { DepartmentService } from '../../services/department.service';
import { ReferenceService } from '../../services/reference.service';

@Component({
  selector: 'app-search-department',
  templateUrl: './search-department.component.html',
  styleUrls: ['./search-department.component.scss']
})
export class SearchDepartmentComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  department: string = '';
  constructor(private service: DepartmentService) { this.getStatuses(); }

  getStatuses() {
    this.service.getValues().subscribe(res => this.list = res.data);
  }
}
