import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Plan } from '../../../utils/models/plans/plan.model';
import { PlanService } from '../../../utils/services/plan/plan.service';

@Component({
  selector: 'app-plan-details',
  templateUrl: './plan-details.component.html',
  styleUrls: ['./plan-details.component.scss']
})
export class PlanDetailsComponent implements OnInit {
  isLoading: boolean = true;
  plan: Plan = new Plan();

  constructor(private planService: PlanService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrievePlan(response.id);
      }
    })
  }

  retrievePlan(id: number): void {
    this.planService.get(id).subscribe(response => {
      this.plan = response.data;
      this.isLoading = false;
    });
  } 
}

