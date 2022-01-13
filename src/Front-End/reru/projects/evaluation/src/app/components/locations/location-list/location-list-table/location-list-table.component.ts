import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { TestingLocationTypeEnum } from 'projects/evaluation/src/app/utils/enums/testing-location-type.enum';
import { PaginationModel } from 'projects/evaluation/src/app/utils/models/pagination.model';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { LocationService } from '../../../../utils/services/location/location.service';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

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
  
	title: string;
	description: string;
	no: string;
	yes: string;

  	constructor(
    	private locationService: LocationService,
		public translate: I18nService,
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
			forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('locations.succes-remove-location-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
			this.list();
		});
	}

	openConfirmationDeleteModal(id): void {
		forkJoin([
			this.translate.get('locations.remove'),
			this.translate.get('locations.remove-msg'),
			this.translate.get('button.no'),
			this.translate.get('button.yes'),
		]).subscribe(([title, description, no, yes]) => {
			this.title = title;
			this.description = description;
			this.no = no;
			this.yes = yes;
			});
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = this.title;
		modalRef.componentInstance.description = this.description;
		modalRef.componentInstance.buttonNo = this.no;
		modalRef.componentInstance.buttonYes = this.yes;
		modalRef.result.then(() => this.deleteLocation(id), () => { });
	}
}
