import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Plan } from 'projects/evaluation/src/app/utils/models/plans/plan.model';
import { PlanService } from 'projects/evaluation/src/app/utils/services/plan/plan.service';

@Component({
  selector: 'app-plan-overview',
  templateUrl: './plan-overview.component.html',
  styleUrls: ['./plan-overview.component.scss']
})
export class PlanOverviewComponent implements OnInit {
  isLoading: boolean = true;
  plan: Plan = new Plan();

  constructor(private planService: PlanService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.parent.params.subscribe(response => {
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
