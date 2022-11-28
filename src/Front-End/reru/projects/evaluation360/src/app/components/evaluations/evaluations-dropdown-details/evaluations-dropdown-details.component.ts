import { Component, Input } from '@angular/core';
import { SurveyService } from '../../../utils/services/survey.service';

@Component({
  selector: 'app-evaluations-dropdown-details',
  templateUrl: './evaluations-dropdown-details.component.html',
  styleUrls: ['./evaluations-dropdown-details.component.scss']
})
export class EvaluationsDropdownDetailsComponent {
  @Input() index: number;
  @Input() survey: any;
  constructor(private surveyService: SurveyService) { }

  download(): void {
    this.surveyService.download(this.survey.id);
  }

  isDisabled(): boolean {
    return this.survey &&
           !this.survey.canEvaluate &&
           !this.survey.canCounterSign &&
           !this.survey.canAutoEvaluate;
  }

}
