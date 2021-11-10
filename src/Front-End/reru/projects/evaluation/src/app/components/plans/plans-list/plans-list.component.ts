import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PaginationModel } from '../../../utils/models/pagination.model';
import { PlanService } from '../../../utils/services/plan/plan.service';

@Component({
  selector: 'app-plans-list',
  templateUrl: './plans-list.component.html',
  styleUrls: ['./plans-list.component.scss']
})
export class PlansListComponent implements OnInit {

  isLoading: boolean = true;
  plans: any[] = [];
  pagination: PaginationModel = new PaginationModel();

  constructor(private planService: PlanService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.list();
  }

  list(data :any = {}): void {
    this.isLoading = true;
    const request = {
      page: data.page || this.pagination.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagination.pageSize
    }

    this.planService.list(request).subscribe(response => {
      if (response.success) {
        this.plans = response.data.items || [];
        this.pagination = response.data.pagedSummary;
        this.isLoading = false;
      }
    });
  }

  navigate(id) {
		this.router.navigate(['plan/', id, 'overview'], { relativeTo: this.route });
	}

}
