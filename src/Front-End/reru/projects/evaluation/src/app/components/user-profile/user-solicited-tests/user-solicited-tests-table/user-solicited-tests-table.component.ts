import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { SolicitedTestService } from 'projects/evaluation/src/app/utils/services/solicited-test/solicited-test.service';
import { SolicitedTestStatusEnum } from 'projects/evaluation/src/app/utils/enums/solicited-test-status.model';

@Component({
	selector: 'app-user-solicited-tests-table',
	templateUrl: './user-solicited-tests-table.component.html',
	styleUrls: ['./user-solicited-tests-table.component.scss']
})
export class UserSolicitedTestsTableComponent implements OnInit {
	userId: number;
	solicitedTests: [] = [];
	pagedSummary: PaginationModel = new PaginationModel();
	isLoading: boolean = true;
	enum = SolicitedTestStatusEnum;

	constructor(private solicitedTestService: SolicitedTestService,
		private activatedRoute: ActivatedRoute,
	) { }

	ngOnInit() {
		this.subsribeForParams();
	}

	subsribeForParams(): void {
		this.activatedRoute.parent.params.subscribe(params => {
			if (params.id) {
				this.userId = params.id
				this.getUserSolicitedTests(this.userId);
			}
		});
	}

	getUserSolicitedTests(data: any = {}) {
		const params: any = {
			userId: this.userId,
			page: data.page || this.pagedSummary.currentPage,
			itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
		}

		this.solicitedTestService.getUserSolicitedTests(params).subscribe(
			res => {
				if (res && res.data) {
					this.solicitedTests = res.data.items;
					this.pagedSummary = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}
}
