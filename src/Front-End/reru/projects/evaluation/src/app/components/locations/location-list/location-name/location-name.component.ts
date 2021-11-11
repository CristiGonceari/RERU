import { Component, Input, OnInit } from '@angular/core';
import { LocationModel } from '../../../../utils/models/locations/location.model'

@Component({
  selector: 'app-location-name',
  templateUrl: './location-name.component.html',
  styleUrls: ['./location-name.component.scss']
})
export class LocationNameComponent implements OnInit {
  @Input() location: LocationModel;

  constructor() { }

  ngOnInit(): void {
  }
  

}
