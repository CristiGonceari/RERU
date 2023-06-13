import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserModuleAccessModel } from 'projects/core/src/app/utils/models/user-module-access.model';
import { Location } from '@angular/common';
import { UserProfileService } from 'projects/core/src/app/utils/services/user-profile.service';

@Component({
	selector: 'app-module-access-list',
	templateUrl: './module-access-list.component.html',
	styleUrls: ['./module-access-list.component.scss'],
})
export class ModuleAccessListComponent implements OnInit {
	isLoading = false;
	userId: number;
	modulesAccess: UserModuleAccessModel[];

	constructor(
		private profileService: UserProfileService,
		private activatedRoute: ActivatedRoute,
		private location: Location
	) {}

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams() {
		this.isLoading = true;
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.userId = params.id;
				this.getPermissionsForUpdate();
			}
		});
	}

	getPermissionsForUpdate(): void {
		this.profileService.getUserModuleAccess(this.userId).subscribe(res => {
			if (res) {
				this.modulesAccess = res.data;
				this.isLoading = false;
			}
		});
	}

	back(): void {
		this.location.back();
	}
}
