import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ModuleRolesService } from '../../../utils/services/module-roles.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.scss']
})
export class RoleDetailsComponent implements OnInit {
  id: number;
  name: string;
  isLoading = false

  constructor(
    private location: Location,
    private roleServise: ModuleRolesService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.id = params.id;
				this.getRoleName(this.id);
			}
		});
  }
  
  getRoleName(id: number): void {
    this.isLoading = true;
    this.roleServise.getById(id).subscribe(res => {
      if(res && res.data) {
        this.isLoading = false;
        this.name = res.data.name
      }
    });
  }

  back(): void {
		this.location.back();
	}

}
