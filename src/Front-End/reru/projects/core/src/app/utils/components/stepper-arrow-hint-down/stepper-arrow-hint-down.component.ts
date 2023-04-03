import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper-arrow-hint-down',
  templateUrl: './stepper-arrow-hint-down.component.html',
  styleUrls: ['./stepper-arrow-hint-down.component.scss']
})
export class StepperArrowHintDownComponent implements OnInit {
  @Input() description: string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
