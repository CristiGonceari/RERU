import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LocationService } from 'projects/evaluation/src/app/utils/services/location/location.service';

@Component({
  selector: 'app-responsible-person',
  templateUrl: './responsible-person.component.html',
  styleUrls: ['./responsible-person.component.scss']
})
export class ResponsiblePersonComponent implements OnInit {

  locationId;

  constructor(
    private route: ActivatedRoute, 
    private locationService: LocationService
    ) { }

  ngOnInit(): void {
		this.subsribeForParams();
  }

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {
      this.locationId = params.id;
    });
	}
}
