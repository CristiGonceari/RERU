import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-confirm-button',
  templateUrl: './confirm-button.component.html',
  styleUrls: ['./confirm-button.component.scss']
})
export class ConfirmButtonComponent {
  @Input() type: string = 'button';
  @Input() value: string;
  @Input() icon: string;
  @Input() disabled: boolean;
  @Input() width: string;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]
}
