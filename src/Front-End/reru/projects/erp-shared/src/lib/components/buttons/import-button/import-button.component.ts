import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'erp-shared-import-button',
  templateUrl: './import-button.component.html',
  styleUrls: ['./import-button.component.scss']
})
export class ImportButtonComponent {
  @Input() type: string = 'button';
  @Input() value: string;
  @Input() icon: string;
  @Input() disabled: boolean;
  @Input() width: string;
  @Input() classes;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]
}
