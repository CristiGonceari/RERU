import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserProfileService } from 'projects/core/src/app/utils/services/user-profile.service';

@Component({
  selector: 'app-search-status',
  templateUrl: './search-status.component.html',
  styleUrls: ['./search-status.component.scss']
})
export class SearchStatusComponent {

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  statusesList;
  constructor(private userService: UserProfileService) { this.getTestStatuses(); }

  getTestStatuses() {
    this.userService.getUserStatus().subscribe(res => this.statusesList = res.data);
  }
}
