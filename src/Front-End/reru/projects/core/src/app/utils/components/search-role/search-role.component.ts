import { Component, EventEmitter, Output } from '@angular/core';
import { ReferenceService } from '../../services/reference.service';
import { UserRoleService } from '../../services/user-role.service';

@Component({
  selector: 'app-search-role',
  templateUrl: './search-role.component.html',
  styleUrls: ['./search-role.component.scss']
})
export class SearchRoleComponent {
  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  list;
  role: string = '';
  constructor(private service: UserRoleService) { this.getStatuses(); }

  getStatuses() {
    this.service.getValues().subscribe(res => this.list = res.data);
  }
}
