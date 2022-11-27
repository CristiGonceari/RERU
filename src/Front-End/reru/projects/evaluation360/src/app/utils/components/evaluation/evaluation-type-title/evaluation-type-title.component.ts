import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-evaluation-type-title',
  templateUrl: './evaluation-type-title.component.html',
  styleUrls: ['./evaluation-type-title.component.css']
})
export class EvaluationTypeTitleComponent {
  @Input() evaluationType: number;
  constructor() { }
}
