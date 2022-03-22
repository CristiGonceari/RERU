import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { NavigationService } from '../../services/navigation/navigation.service';

@Component({
  selector: 'app-location-back-button',
  templateUrl: './location-back-button.component.html',
  styleUrls: ['./location-back-button.component.scss']
})
export class LocationBackButtonComponent implements OnInit {
  
  @Input() value: string; 

  constructor(private router : Router,
    private location: Location,
    public navigation: NavigationService) { }

  ngOnInit(): void {
  }
 
}
