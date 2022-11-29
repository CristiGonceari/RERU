import { Component, Input } from '@angular/core';
import { EvaluationService } from '../../../utils/services/survey.service';

@Component({
  selector: 'app-evaluations-dropdown-details',
  templateUrl: './evaluations-dropdown-details.component.html',
  styleUrls: ['./evaluations-dropdown-details.component.scss']
})
export class EvaluationsDropdownDetailsComponent {
  @Input() index: number;
  @Input() survey: any;
  constructor(private evaluationService: EvaluationService) { }

  download(): void {
    this.evaluationService.download(this.survey.id);
  }

  isDisabled(): boolean {
    return this.survey &&
           !this.survey.canEvaluate &&
           !this.survey.canCounterSign &&
           !this.survey.canAutoEvaluate;
  }

}
