import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-evaluators',
  templateUrl: './evaluators.component.html',
  styleUrls: ['./evaluators.component.scss']
})
export class EvaluatorsComponent implements OnInit {
  id: number;
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
