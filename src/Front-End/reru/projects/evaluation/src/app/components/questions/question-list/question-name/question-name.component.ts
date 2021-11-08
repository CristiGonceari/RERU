import { Component, Input, OnInit } from '@angular/core';
import { PermissionCheckerService } from 'dist/erp-shared/erp-shared';
import { QuestionUnit } from 'projects/evaluation/src/app/utils/models/question-units/question-unit.model';

@Component({
  selector: 'app-question-name',
  templateUrl: './question-name.component.html',
  styleUrls: ['./question-name.component.scss']
})
export class QuestionNameComponent implements OnInit {
  @Input() question: QuestionUnit;
  permission:boolean = false;

   constructor() { }

  ngOnInit(): void {
    // if(this.permissionService.isGranted('P03010201')) 
      this.permission = true;
  }
}
