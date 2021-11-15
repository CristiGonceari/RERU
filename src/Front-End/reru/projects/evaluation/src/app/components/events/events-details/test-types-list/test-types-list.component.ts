import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-test-types-list',
  templateUrl: './test-types-list.component.html',
  styleUrls: ['./test-types-list.component.scss']
})
export class TestTypesListComponent implements OnInit {
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
}
