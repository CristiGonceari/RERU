import { Component, OnInit } from '@angular/core';
import { I18nService } from '../../utils/services/i18n.service';
import { InitializerUserProfileService } from '../../utils/services/initializer-user-profile.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  profile;
  isLoading: boolean = true;
  constructor(private profileService: InitializerUserProfileService,
              private translate: I18nService) {}

  ngOnInit(): void {
    this.retrieveProfile();
  }

  retrieveProfile(): void {
    this.profileService.profile.subscribe(response => {
      this.profile = response;
      this.isLoading = false;
    })
  }
 }