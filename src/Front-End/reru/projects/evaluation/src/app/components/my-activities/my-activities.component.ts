import { Component, OnInit } from '@angular/core';
import { UserProfileService } from '../../utils/services/user-profile/user-profile.service';
import { UserProfile } from '../../utils/models/user-profiles/user-profile.model';

@Component({
  selector: 'app-my-activities',
  templateUrl: './my-activities.component.html',
  styleUrls: ['./my-activities.component.scss']
})
export class MyActivitiesComponent implements OnInit {
  acronym: string;
  isLoading = false;
  user: UserProfile = new UserProfile();
  constructor(private userService: UserProfileService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser() {
		this.userService.getCurrentUser().subscribe(response => {
      this.user = response.data;
			this.subscribeForUserChanges(response.data);
			this.isLoading = false;
		});
	}

  subscribeForUserChanges(user): void {
    let matches = user && (user.firstName + ' ' + user.lastName).match(/\b(\w)/g);
    if(user.lastName == null){
      matches = user && (user.firstName).match(/\b(\w)/g);
    }
    this.acronym = matches ? matches.join('') : null;
	}
}
