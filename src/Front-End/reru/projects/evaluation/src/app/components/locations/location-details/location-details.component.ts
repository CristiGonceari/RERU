import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from '../../../utils/services/location/location.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { NotificationsService } from 'angular2-notifications';
import { ConfirmModalComponent } from '@erp/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-location-details',
  templateUrl: './location-details.component.html',
  styleUrls: ['./location-details.component.scss']
})
export class LocationDetailsComponent implements OnInit {

  locationId: number;
  locationName: string;
  isLoading: boolean = true; 
	title: string;
	description: string;
	no: string;
	yes: string;

  constructor(
    private locationService: LocationService,
		private activatedRoute: ActivatedRoute,
		public translate: I18nService,
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
  }
  
  subsribeForParams(): void {
    this.activatedRoute.params.subscribe(params => {
      this.locationId = params.id;
			if (this.locationId) {
        this.getLocation();
      }
    });
	}

  deleteCategory(): void{
		this.locationService.deleteLocation(this.locationId).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('locations.succes-remove-location-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
			this.router.navigate(['/locations']);
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
		});
	}

	openConfirmationDeleteModal(): void {
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
		modalRef.componentInstance.buttonYes = this.yes;;
		modalRef.result.then(() => this.deleteCategory(), () => { });
	}
}
