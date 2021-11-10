import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Plan } from '../../../utils/models/plans/plan.model';
import { PlanService } from '../../../utils/services/plan/plan.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.scss']
})
export class DeleteComponent implements OnInit {

  isLoading: boolean = true;
  plan: Plan = new Plan();

  constructor(private planService: PlanService,
    private route: ActivatedRoute,
    private router: Router,
    public location: Location) {
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

  onConfirm() {
    this.planService.delete(this.plan.id).subscribe(() => this.router.navigate(['plans']));
  }

  back() {
    this.location.back();
  }
}
