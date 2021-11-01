import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-add-button',
  templateUrl: './add-button.component.html',
  styleUrls: ['./add-button.component.scss']
})
export class AddButtonComponent implements OnInit {
  @Input() value: string; 
  @Input() icon: string;
  @Input() disabled: boolean;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]

  constructor() { }

  ngOnInit() {
  }

}
