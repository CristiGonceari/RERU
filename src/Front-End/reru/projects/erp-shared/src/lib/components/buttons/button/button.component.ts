import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent {
  @Input() type: string = 'button';
  @Input() value: string;
  @Input() icon: string;
  @Input() width: string;
  @Input() disabled: boolean;
  @Input() classes: string;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]
}
