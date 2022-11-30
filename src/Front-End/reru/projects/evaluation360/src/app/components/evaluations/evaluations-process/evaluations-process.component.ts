import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-evaluations-process',
  templateUrl: './evaluations-process.component.html',
  styleUrls: ['./evaluations-process.component.scss']
})
export class EvaluationProcessComponent implements OnInit {
  id: number;
  type: number;
  constructor(private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.subscribeFormParams();
    this.id = 1;
    this.type = 1;
  }

  subscribeFormParams(): void {
    // TODO: combineLatest with queryParams
    // this.route.params.subscribe(response => {
    //   if (response.id) {
    //     this.id = +response.id;
    //   } else {
    //     this.router.navigate(['../../'], { relativeTo: this.route });
    //   }
    // });
  }

}
