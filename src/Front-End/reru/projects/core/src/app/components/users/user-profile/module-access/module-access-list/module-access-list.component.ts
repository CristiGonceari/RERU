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
	isResponsive: boolean;

	checkScreenWidth(): void {
		const screenWidth = window.innerWidth;
		this.isResponsive = screenWidth <= 375;
	}
	  
	ngOnDestroy(): void {
		window.removeEventListener('resize', this.checkScreenWidth.bind(this));
	}

	constructor(
		private profileService: UserProfileService,
		private activatedRoute: ActivatedRoute,
		private location: Location
	) {}

	ngOnInit(): void {
		this.subsribeForParams();
		this.checkScreenWidth();
		window.addEventListener('resize', this.checkScreenWidth.bind(this));
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
