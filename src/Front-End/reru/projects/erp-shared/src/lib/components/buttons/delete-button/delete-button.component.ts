import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-delete-button',
  templateUrl: './delete-button.component.html',
  styleUrls: ['./delete-button.component.scss']
})
export class DeleteButtonComponent {
  @Input() type: string = 'button';
  @Input() value: string;
  @Input() icon: string;
  @Input() disabled: boolean;
  @Input() width: string;
  @Input() classes: string;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]
}
