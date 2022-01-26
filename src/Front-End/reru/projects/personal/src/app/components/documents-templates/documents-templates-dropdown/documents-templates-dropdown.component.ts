import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-documents-templates-dropdown',
  templateUrl: './documents-templates-dropdown.component.html',
  styleUrls: ['./documents-templates-dropdown.component.scss']
})
export class DocumentsTemplatesDropdownComponent  {
  @Input() editDocument: string[];
  @Input() viewDocument: string[];
  
  constructor() { }
 
  @Output() delete: EventEmitter<void> = new EventEmitter<void>();
}
