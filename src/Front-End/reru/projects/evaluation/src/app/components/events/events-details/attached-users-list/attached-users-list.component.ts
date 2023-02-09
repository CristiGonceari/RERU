import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-attached-users-list',
  templateUrl: './attached-users-list.component.html',
  styleUrls: ['./attached-users-list.component.scss']
})
export class AttachedUsersListComponent implements OnInit {
  id;
  url: string;
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
