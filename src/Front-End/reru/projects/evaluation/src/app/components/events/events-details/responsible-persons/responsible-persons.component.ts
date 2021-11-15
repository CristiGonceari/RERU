import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-responsible-persons',
  templateUrl: './responsible-persons.component.html',
  styleUrls: ['./responsible-persons.component.scss']
})
export class ResponsiblePersonsComponent implements OnInit {
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
}
