import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './../../utils/services/user-profile/user-profile.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  profile;
  isLoading: boolean = true;
  constructor(private userService: UserProfileService) { }

  ngOnInit(): void {
    this.retrieveProfile();
  }

  retrieveProfile(): void {
    this.userService.getCurrentUser().subscribe(response => {
      this.profile = response;
      this.isLoading = false;
    })
  }
}
