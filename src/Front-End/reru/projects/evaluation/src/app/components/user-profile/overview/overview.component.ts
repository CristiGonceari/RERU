import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserProfileService } from '../../../utils/services/user-profile/user-profile.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {
  isLoading = true;
  user;

  constructor(private activatedRoute: ActivatedRoute, private userService: UserProfileService) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
        this.getUser(params.id);
			}
		});
	}

  getUser(id): void {
    this.userService.getUser(id).subscribe(res => {
      this.user = res.data;
      this.isLoading = false;
    });
  }
}
