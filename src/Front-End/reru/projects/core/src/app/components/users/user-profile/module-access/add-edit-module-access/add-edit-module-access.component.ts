import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../../../utils/util/notification.util';
import { ModuleRolesService } from 'projects/core/src/app/utils/services/module-roles.service';
import { UserProfileService } from 'projects/core/src/app/utils/services/user-profile.service';

@Component({
  selector: 'app-add-edit-module-access',
  templateUrl: './add-edit-module-access.component.html',
  styleUrls: ['./add-edit-module-access.component.scss']
})
export class AddEditModuleAccessComponent implements OnInit {
isLoading = true;
userId: number;
moduleId: number;
roles: any;
role: string;
userData: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private moduleRoleService: ModuleRolesService,
    private location: Location,
    private notificationService: NotificationsService,
    private userProfileService: UserProfileService
  ) {  }

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
        this.getModuleAccessData(this.moduleId, this.userId)
			}
		});
	}

  selectRole(): void {
    this.moduleRoleService.selectModuleRole(this.moduleId).subscribe(res => {
      this.roles = res.data;
    });
  }

  getModuleAccessData(moduleId, userId): void {
    let data = {
      moduleId,
      userId
    }
    this.userProfileService.getModuleAccessRole(data).subscribe((res) => {
      this.userData = res.data;
      this.role = res.data.roleId || null;
      this.isLoading = false;
    });
    this.selectRole();
  }

  giveRole(): void {
    let params = {
      moduleId: +this.moduleId,
      userId: +this.userId,
      roleId: +this.role
    }
    this.userProfileService.giveModuleAccessRole(params).subscribe(() => this.back());
    this.notificationService.success('Success', 'Module access role is updated!', NotificationUtil.getDefaultMidConfig());
  }

  submit(): void {
    if(this.role === 'null') {
      this.notificationService.warn('Warning', 'Please select role!', NotificationUtil.getDefaultMidConfig());
    } else {
      this.giveRole();
    }
  }
  
	back(): void {
		this.location.back();
	}

}
