import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { TestingLocationTypeEnum } from 'projects/evaluation/src/app/utils/enums/testing-location-type.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { LocationService } from '../../../../utils/services/location/location.service';

@Component({
  selector: 'app-location-list-table',
  templateUrl: './location-list-table.component.html',
  styleUrls: ['./location-list-table.component.scss']
})
export class LocationListTableComponent implements OnInit {

  	locations = [];
	keyword: string;
	pagination: PaginationModel = new PaginationModel();
	enum = TestingLocationTypeEnum;
	isLoading: boolean = true;
  
  	constructor(
    	private locationService: LocationService,
    	private router: Router, 
    	private route: ActivatedRoute,
		private notificationService: NotificationsService,
		private modalService: NgbModal,
  	) { }

 	 ngOnInit(): void {
		this.list();
  	}

  	list(data: any = {}) {
		this.keyword = data.keyword;
		let params = {
			name: this.keyword || '',
			page: data.page || this.pagination.currentPage,
			itemsPerPage: Number(this.pagination?.pageSize || 10)
		}
		this.locationService.getLocations(params).subscribe( res => {
			if(res && res.data) {
				this.locations = res.data.items;
				this.pagination = res.data.pagedSummary;
				this.isLoading = false;
			}
		});
	}

	navigate(id){
		this.router.navigate(['location/', id, 'overview'], {relativeTo: this.route});
	}

	deleteLocation(id): void{
		this.locationService.deleteLocation(id).subscribe(() => 
		{
			this.notificationService.success('Success', 'Location was successfully deleted', NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this location?';
		modalRef.result.then(() => this.deleteLocation(id), () => { });
	}
}
