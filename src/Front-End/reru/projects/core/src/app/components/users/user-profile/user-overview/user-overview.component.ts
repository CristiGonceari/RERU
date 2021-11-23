import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'projects/core/src/app/utils/services/user.service';

@Component({
  selector: 'app-user-overview',
  templateUrl: './user-overview.component.html',
  styleUrls: ['./user-overview.component.scss']
})
export class UserOverviewComponent implements OnInit {
  isLoading = true;
  id: string;
  email: string;

  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService
    ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams(): void {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.id = params.id;
        this.getUserInfo();
			}
		});
	}

  getUserInfo(): void {
    this.userService.getUser(this.id).subscribe(res => {
      this.email = res.data.email;
      this.isLoading = false;
    });
  }
}
