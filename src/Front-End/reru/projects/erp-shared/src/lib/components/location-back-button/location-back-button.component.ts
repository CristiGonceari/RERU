import { Component, Input } from '@angular/core';
import { NavigationService } from '../../services/navigation/navigation.service';

@Component({
  selector: 'app-location-back-button',
  templateUrl: './location-back-button.component.html',
  styleUrls: ['./location-back-button.component.scss']
})
export class LocationBackButtonComponent {
  @Input() value: string; 
  constructor(public navigation: NavigationService) { }
}
