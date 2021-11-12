import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { AttachToLocationService } from 'projects/evaluation/src/app/utils/services/attach-to-location/attach-to-location.service';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';
import { NotificationUtil } from 'projects/evaluation/src/app/utils/util/notification.util';
import { Location } from '@angular/common';

@Component({
  selector: 'app-detach-computer',
  templateUrl: './detach-computer.component.html',
  styleUrls: ['./detach-computer.component.scss']
})
export class DetachComputerComponent implements OnInit {

  locationId;
  clientId;
  userName;
  name;
  address;

  constructor(
    private locationService: LocationService,
    private attachToLocationService: AttachToLocationService, 
    private activatedRoute: ActivatedRoute,
    private location: Location,
    private notificationService: NotificationsService
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  subsribeForParams() {
    this.locationId = this.activatedRoute.snapshot.paramMap.get('id');
    this.clientId = this.activatedRoute.snapshot.paramMap.get('id2');
    console.log(this.clientId)
    if (this.locationId)
      this.getLocation();
    console.log('locationIdAAA', this.clientId);
  }

  getLocation(): void {
    this.locationService.getLocation(this.locationId).subscribe((res) => {
      this.name = res.data.name;
      this.address = res.data.address;
    })
    console.log('locationId', this.locationId);
  }

  detach() {
    this.attachToLocationService.detachComputer({locationClientId: this.clientId}).subscribe(() => {
      this.backClicked();
			this.notificationService.success('Success', 'Computer was successfully detached', NotificationUtil.getDefaultMidConfig());
    });
    console.log('clientID', this.clientId);
  }

  backClicked() {
    this.location.back();
  }
}
