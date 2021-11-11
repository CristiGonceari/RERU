import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from '../../../utils/services/location/location.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-location-details',
  templateUrl: './location-details.component.html',
  styleUrls: ['./location-details.component.scss']
})
export class LocationDetailsComponent implements OnInit {

  locationId: number;
  locationName: string;
  isLoading: boolean = true; 

  constructor(
    private locationService: LocationService,
		private activatedRoute: ActivatedRoute,
    public router: Router,
    public notificationService: NotificationsService,
    private modalService: NgbModal,
  ) { }

  ngOnInit(): void {
   this.subsribeForParams();
  }

  getLocation(){
    this.locationService.getLocation(this.locationId).subscribe(res => {
      if (res && res.data) {
        this.locationName = res.data.name;
        this.isLoading = false;
      }
    });
    console.log('LOCATION', this.locationId);
  }
  
  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.locationId = params.id;
			if (this.locationId) {
        this.getLocation();
      }
    });
    console.log('locIdFromDetails', this.locationId);
	}

  deleteCategory(categoryId): void{
		this.locationService.deleteLocation(this.locationId).subscribe(() => {
			this.router.navigate(['/locations']);
			this.notificationService.success('Success', 'Location was successfully deleted', NotificationUtil.getDefaultMidConfig());
		});
	}

	openConfirmationDeleteModal(): void {
		const modalRef: any = this.modalService.open(ConfirmModalComponent, { centered: true });
		modalRef.componentInstance.title = 'Delete';
		modalRef.componentInstance.description = 'Are you sure you want to delete this Location?';
		modalRef.result.then(() => this.deleteCategory(this.locationId), () => { });
	}
}
