import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper-arrow-hint',
  templateUrl: './stepper-arrow-hint.component.html',
  styleUrls: ['./stepper-arrow-hint.component.scss']
})
export class StepperArrowHintComponent implements OnInit {
  @Input() description: string;

  constructor() { }

  ngOnInit(): void {
  }

}
