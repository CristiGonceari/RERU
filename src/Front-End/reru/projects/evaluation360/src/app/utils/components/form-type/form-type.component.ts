import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Evaluation } from '../../models/evaluation';
import { SurveyService } from '../../services/survey.service';

@Component({
  selector: 'app-form-type',
  templateUrl: './form-type.component.html',
  styleUrls: ['./form-type.component.scss']
})
export class FormTypeComponent implements OnInit {
  @Input() id: number;
  @Input() action: number;
  evaluation: Evaluation;
  type: number;
  constructor(private surveyService: SurveyService,
              private router: Router) { }

  ngOnInit(): void {
    this.getEvaluation(this.id);
  }

  getEvaluation(id: number): void {
    this.surveyService.get(id).subscribe(response => {
			if (!response) {
				this.router.navigate(['/']);
				return;
      }
      this.type = response.type;
			this.evaluation = response;
		});
  }
}