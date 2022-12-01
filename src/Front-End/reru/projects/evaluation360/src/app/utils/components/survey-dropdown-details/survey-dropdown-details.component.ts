import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SurveyService } from '../../services/survey.service';

@Component({
  selector: 'app-survey-dropdown-details',
  templateUrl: './survey-dropdown-details.component.html',
  styleUrls: ['./survey-dropdown-details.component.scss']
})
export class SurveyDropdownDetailsComponent {
  @Input() index: number;
  @Input() survey: any;
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor(private surveyService: SurveyService) { }

  download(): void {
    this.surveyService.download(this.survey.id);
  }

  isDisabled(): boolean {
    return this.survey &&
           !this.survey.canEvaluate &&
           !this.survey.canCounterSign &&
           !this.survey.canAutoEvaluate &&
           !this.survey.canDownload;
  }
}
