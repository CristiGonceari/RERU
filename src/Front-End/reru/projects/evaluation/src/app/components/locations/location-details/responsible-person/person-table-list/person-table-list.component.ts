import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { AttachToLocationService } from 'projects/evaluation/src/app/utils/services/attach-to-location/attach-to-location.service';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';

@Component({
  selector: 'app-person-table-list',
  templateUrl: './person-table-list.component.html',
  styleUrls: ['./person-table-list.component.scss']
})
export class PersonTableListComponent implements OnInit {

  persons;
	pagination: PaginationModel = new PaginationModel();
	locationId: number;
	isLoading: boolean = true;
  
  constructor(
    private locationService: LocationService, 
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

		this.attachToLocationService.getPersons(params).subscribe(
			res => {
				if (res && res.data) {
					this.persons = res.data.items;
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
