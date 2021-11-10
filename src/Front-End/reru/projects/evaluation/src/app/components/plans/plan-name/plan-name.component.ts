import { Component, Input, OnInit } from '@angular/core';
import { PermissionCheckerService } from '@erp/shared';
import { Plan } from '../../../utils/models/plans/plan.model';

@Component({
  selector: 'app-plan-name',
  templateUrl: './plan-name.component.html',
  styleUrls: ['./plan-name.component.scss']
})
export class PlanNameComponent implements OnInit {

  @Input() plan: Plan;
  permission: boolean = false;

  constructor(public permissionService: PermissionCheckerService) { }

  ngOnInit(): void {
    if (this.permissionService.isGranted('P03001101'))
      this.permission = true;
  }
}
