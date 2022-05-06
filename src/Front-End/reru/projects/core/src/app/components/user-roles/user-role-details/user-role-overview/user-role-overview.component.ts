import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserRoleService } from 'projects/core/src/app/utils/services/user-role.service';

@Component({
  selector: 'app-user-role-overview',
  templateUrl: './user-role-overview.component.html',
  styleUrls: ['./user-role-overview.component.scss']
})
export class UserRoleOverviewComponent implements OnInit {
  roleId: number;
  roleName: string;
  isLoading: boolean = true;

  constructor(
    private roleService: UserRoleService,
		private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  get(){
    this.roleService.get(this.roleId).subscribe(res => {
      if (res && res.data) {
        this.roleName = res.data.name;
        this.isLoading = false;
      }
    });
  }

  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.roleId = params.id;
			if (this.roleId) {
        this.get();
    }});
	}
}
