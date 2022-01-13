import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { Location } from '@angular/common';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { LocationResponsiblePerson } from './../../../../../utils/models/locations/location-responsible-person.model';
import { forkJoin } from 'rxjs';
import { I18nService } from 'projects/evaluation/src/app/utils/services/i18n/i18n.service';

@Component({
  selector: 'app-attach-person',
  templateUrl: './attach-person.component.html',
  styleUrls: ['./attach-person.component.scss']
})
export class AttachPersonComponent implements OnInit {

  userId = 0;
  locationId;
  name;
  address;
  isLoading: boolean = true;
  title: string;
	description: string;

  constructor(
    private location: Location, 
    private locationService: LocationService, 
		public translate: I18nService,
    private activatedRoute: ActivatedRoute,
		private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(response => {this.locationId = response.id; this.get()});
    this.locationService.user.subscribe(x => this.userId = x);
  }

  parse(){
    this.userId = this.userId == undefined ? 0 : this.userId;
    
    return {
      data: new LocationResponsiblePerson({
        locationId: +this.locationId,
        userProfileId: +this.userId
      })
    };
  }

  attach(){
    this.locationService.assignPerson(this.parse()).subscribe(() => {
      forkJoin([
				this.translate.get('modal.success'),
				this.translate.get('locations.succes-add-person-msg'),
			  ]).subscribe(([title, description]) => {
				this.title = title;
				this.description = description;
				});
      this.backClicked();
			this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    });
  }

  get(){
    this.locationService.getLocation(this.locationId).subscribe( res => {
      if (res && res.data) {
        this.name = res.data.name;
        this.address = res.data.address;
        this.isLoading = false;
      }
    });
  }

  backClicked() {
		this.location.back();
	}
}
