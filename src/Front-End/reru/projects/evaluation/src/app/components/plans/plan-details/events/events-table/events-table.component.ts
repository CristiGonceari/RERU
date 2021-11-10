import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';

@Component({
  selector: 'app-events-table',
  templateUrl: './events-table.component.html',
  styleUrls: ['./events-table.component.scss']
})
export class EventsTableComponent implements OnInit {
  events: [] = [];
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	planId: number;
	isLoading: boolean = true;

	constructor(private service: PlanService, private router: Router, private route: ActivatedRoute) { }

	ngOnInit(): void {
		this.route.parent.params.subscribe(params => {
			this.planId = params.id;
		});
		this.list();
	}

	list(data: any = {}) {
		let params = {
		  planId: this.planId,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}

		this.service.events(params).subscribe(
			res => {
				if (res && res.data) {
					this.events = res.data.items;
					this.pagination = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

}
