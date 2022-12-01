import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-survey-evaluate',
  templateUrl: './survey-evaluate.component.html',
  styleUrls: ['./survey-evaluate.component.scss']
})
export class SurveyEvaluateComponent implements OnInit {
  id: number;
  constructor(private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.subscribeFormParams();
  }

  subscribeFormParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.id = +response.id;
      } else {
        this.router.navigate(['../../'], { relativeTo: this.route });
      }
    });
  }

}
