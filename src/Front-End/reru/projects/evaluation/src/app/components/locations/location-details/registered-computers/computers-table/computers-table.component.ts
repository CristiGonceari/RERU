import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { AttachToLocationService } from '../../../../../utils/services/attach-to-location/attach-to-location.service';

@Component({
  selector: 'app-computers-table',
  templateUrl: './computers-table.component.html',
  styleUrls: ['./computers-table.component.scss']
})
export class ComputersTableComponent implements OnInit {

  clients = [];
	pagination: PaginationModel = new PaginationModel();
	locationId: number;
	isLoading: boolean = true;
  
  constructor(
    private attachToLocationService: AttachToLocationService, 
    private router: Router, 
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
		this.subsribeForParams();
  }

  list(data: any = {}) {
		let params = {
			locationId: this.locationId,
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}

		this.attachToLocationService.getComputers(params).subscribe(
			res => {
				if (res && res.data) {
					this.clients = res.data.items;
					this.pagination = res.data.pagedSummary;
					this.isLoading = false;
				}
			}
		)
	}

  subsribeForParams(): void {
		this.route.parent.params.subscribe(params => {
			this.locationId = params.id;
			if (this.locationId) {
				this.list();
			}
		});
	}
}
