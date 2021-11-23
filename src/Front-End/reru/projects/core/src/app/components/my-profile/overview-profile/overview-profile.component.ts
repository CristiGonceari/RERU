import { Component, OnInit } from '@angular/core';
import { MyProfile } from '../../../utils/models/user-profile.model';
import { ProfileService } from '../../../utils/services/profile.service';

@Component({
	selector: 'app-overview-profile',
	templateUrl: './overview-profile.component.html',
	styleUrls: ['./overview-profile.component.scss'],
})
export class OverviewProfileComponent implements OnInit {
	isLoading = true;
	profileData = new MyProfile();

	constructor(private profileService: ProfileService) {}

	ngOnInit(): void {
		this.initData();
	}

	initData(): void {
		this.profileService.getUserProfile().subscribe(res => {
			if (res) {
				this.profileData = res.data;
				this.isLoading = false;
			}
		});
	}

	navigate(url: string): void {
		window.open(url, '_blank');
	}
}
