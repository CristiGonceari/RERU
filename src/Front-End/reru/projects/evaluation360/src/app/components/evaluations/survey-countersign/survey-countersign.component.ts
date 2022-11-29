import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-survey-countersign',
  templateUrl: './survey-countersign.component.html',
  styleUrls: ['./survey-countersign.component.scss']
})
export class SurveyCountersignComponent implements OnInit {
  id: number;
  isLoading: boolean = true;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.id = +response.id;
        this.isLoading = false;
      }
    })
  }
}
