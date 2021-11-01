import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-delete-button',
  templateUrl: './delete-button.component.html',
  styleUrls: ['./delete-button.component.scss']
})
export class DeleteButtonComponent implements OnInit {
  @Input() value: string; 
  @Input() icon: string;
  @Input() disabled: boolean;
  @Output() handle: EventEmitter<void> = new EventEmitter<void>();

  providers: [SafeHtml]

  constructor() { }

  ngOnInit() {
  }

}
