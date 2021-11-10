import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';

@Component({
  selector: 'app-responsable-persons-table',
  templateUrl: './responsable-persons-table.component.html',
  styleUrls: ['./responsable-persons-table.component.scss']
})
export class ResponsablePersonsTableComponent implements OnInit {
    persons;
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	id: number;
	isLoading: boolean = true;

	constructor(private planService: PlanService, private router: Router, private route: ActivatedRoute) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

	subsribeForParams(): void {
		this.route.parent.params.subscribe(params => {
			this.id = params.id;
			if (this.id) {
				this.list();
			}
		});
	}

	list(data: any = {}) {
		let params = {
			planId: this.id,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}

		this.planService.persons(params).subscribe( res => {
			if (res && res.data) {
				this.persons = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
				
		});
	}
}
