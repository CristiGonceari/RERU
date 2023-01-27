import { Component } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';
import { UserProfileService } from '../../utils/services/user-profile.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  isLoading: boolean = true;
  profile: any;

  constructor(private readonly translate: I18nService,
    private userProfileService: UserProfileService) {
      this.getUserProfile();
  }

  getUserProfile(){
    this.userProfileService.getCurrentUserProfile().subscribe(res => {this.profile = res.data, this.isLoading = false; })
  }
 }