import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-responsable-persons',
  templateUrl: './responsable-persons.component.html',
  styleUrls: ['./responsable-persons.component.scss']
})
export class ResponsablePersonsComponent implements OnInit {
  id: number;

	constructor(private route: ActivatedRoute) { }

	ngOnInit(): void {
		this.subsribeForParams();
	}

  subsribeForParams(): void {
    this.route.parent.params.subscribe(params => {
      this.id = params.id;
    });
	}
}

