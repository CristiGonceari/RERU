import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent implements OnInit {
  @Input() value: string; 
  @Input() icon: string;
  @Input() width: string;
  @Input() disabled: boolean;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]

  constructor() { }

  ngOnInit() {
  }

}
