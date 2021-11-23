import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UserProfileService } from 'projects/core/src/app/utils/services/user-profile.service';

@Component({
  selector: 'app-remove-module-access',
  templateUrl: './remove-module-access.component.html',
  styleUrls: ['./remove-module-access.component.scss']
})
export class RemoveModuleAccessComponent implements OnInit {
  isLoading = true;
  userId: number;
  moduleId: number;
  userData: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private userProfileService: UserProfileService
  ) { }

  ngOnInit(): void {
    this.getUserId();
    this.getModuleId();
  }

  getUserId(): void {
		this.activatedRoute.parent.parent.parent.params.subscribe(params => {
			if (params.id) {
				this.userId = params.id;
			}
		});
	}

  getModuleId(): void {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.moduleId = params.id;
        this.getModuleAccessData(this.moduleId, this.userId);
			}
		});
	}

  getModuleAccessData(moduleId, userId): void {
    let data = {
      moduleId,
      userId
    }
    this.userProfileService.getModuleAccessRole(data).subscribe((res) => {
      this.userData = res.data;
      this.isLoading = false;
    });
  }

  removeAccess(): void {
    let data = {
      moduleId: this.moduleId,
      userId: this.userId
    }
    this.userProfileService.removeModuleAccessRole(data).subscribe(() => this.back())
  }

  submit(): void {
    this.removeAccess();
  }
  
	back(): void {
		this.location.back();
	}



}
