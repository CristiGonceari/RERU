import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DepartmentService } from 'projects/core/src/app/utils/services/department.service';

@Component({
  selector: 'app-department-overview',
  templateUrl: './department-overview.component.html',
  styleUrls: ['./department-overview.component.scss']
})
export class DepartmentOverviewComponent implements OnInit {
  departmentId: number;
  departmentName: string;
  isLoading: boolean = true;

  constructor(
    private departmentService: DepartmentService,
		private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  get(){
    this.departmentService.get(this.departmentId).subscribe(res => {
      if (res && res.data) {
        this.departmentName = res.data.name;
        this.isLoading = false;
      }
    });
  }

  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.departmentId = params.id;
			if (this.departmentId) {
        this.get();
    }});
	}
}
