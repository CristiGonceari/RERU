import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from '../../../../utils/services/location/location.service';
import { TestingLocationTypeEnum } from '../../../../utils/enums/testing-location-type.enum';

@Component({
  selector: 'app-location-overview',
  templateUrl: './location-overview.component.html',
  styleUrls: ['./location-overview.component.scss']
})
export class LocationOverviewComponent implements OnInit {
  
  locationId: number;
  name: string;
  address: string;
  places: number;
  type: string;
  description: string;
  isLoading: boolean = true;

  constructor(
    private locationService: LocationService,
		private activatedRoute: ActivatedRoute,
    public router: Router
  ) { }

  ngOnInit(): void {
    this.subsribeForParams();
  }

  getLocation(){
    this.locationService.getLocation(this.locationId).subscribe(res => {
      if (res && res.data) {
        this.name = res.data.name;
        this.address = res.data.address;
        this.places = res.data.places;
        this.type = TestingLocationTypeEnum[res.data.type];
        this.description = res.data.description;
        this.isLoading = false;
      }
    });
  }
  
  subsribeForParams(): void {
    this.activatedRoute.parent.params.subscribe(params => {
      this.locationId = params.id;
			if (this.locationId) {
        this.getLocation();
    }});
	}
}
