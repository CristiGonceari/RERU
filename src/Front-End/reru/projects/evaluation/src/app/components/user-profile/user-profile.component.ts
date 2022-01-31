import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from '../../utils/services/user-profile/user-profile.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  acronym: string;
  isLoading = false;
  user;

  constructor(private activatedRoute: ActivatedRoute, private userService: UserProfileService) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.isLoading = true;
    this.activatedRoute.params.subscribe(params => {
      if (params.id) {
        this.getUserById(params.id);
      }
    });
  }

  getUserById(id: number) {
		this.userService.getUser(id).subscribe(response => {
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
