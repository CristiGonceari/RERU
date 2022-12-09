import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EvaluationListModel } from '@utils';
import { EvaluationService } from '../../services/evaluations.service';

@Component({
  selector: 'app-evaluation-dropdown-details',
  templateUrl: './evaluation-dropdown-details.component.html',
  styleUrls: ['./evaluation-dropdown-details.component.scss']
})
export class EvaluationDropdownDetailsComponent {
  @Input() index: number;
  @Input() evaluation: EvaluationListModel;
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
  constructor(private evaluationService: EvaluationService) { }

  download(): void {
    this.evaluationService.download(this.evaluation.id);
  }

  isDisabled(): boolean {
    return this.evaluation &&
           !this.evaluation?.canEvaluate &&
           !this.evaluation?.canCounterSign &&
           !this.evaluation?.canDownload && 
           !this.evaluation?.canDelete &&
           !this.evaluation?.canFinished;;
  }
}
