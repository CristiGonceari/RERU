import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLinkWithHref } from '@angular/router';

@Component({
  selector: 'app-locations-list',
  templateUrl: './locations-list.component.html',
  styleUrls: ['./locations-list.component.scss']
})
export class LocationsListComponent implements OnInit {
  id;
  url;
  
	constructor(private route: ActivatedRoute, private router: Router) { }

	ngOnInit(): void {
		this.subsribeForParams();
    this.url = this.router.url.split("/").pop();
	}

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {
      this.id = params.id;
    });
	}

  getTitle(): string {
		return document.getElementById('title').innerHTML;
	}
}
