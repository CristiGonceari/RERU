import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-evaluation-type-title',
  templateUrl: './evaluation-type-title.component.html'
})
export class EvaluationTypeTitleComponent {
  @Input() evaluationType: number;
  constructor() { }
}
