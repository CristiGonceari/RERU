import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-mobile-add-button',
  templateUrl: './mobile-add-button.component.html',
  styleUrls: ['./mobile-add-button.component.scss']
})
export class MobileAddButtonComponent{
  @Input() type: string = 'button';
  @Input() value: string;
  @Input() icon: string;
  @Input() disabled: boolean;
  @Input() width: string;
  @Input() classes: string;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]
}
