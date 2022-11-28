import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss']
})
export class ViewComponent implements OnInit {
  id: number;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscribeForRouteParams();
  }

  subscribeForRouteParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.id = response.id;
      }
    });
  }

}
