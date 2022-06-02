import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReferenceService } from '../../../utils/services/reference/reference.service';

@Component({
  selector: 'app-search-by-type',
  templateUrl: './search-by-type.component.html',
  styleUrls: ['./search-by-type.component.scss']
})
export class SearchByTypeComponent{

  @Output() filter: EventEmitter<void> = new EventEmitter<void>();
  documentTemplatesType;

  constructor(private referenceService: ReferenceService) { this.getDocumentTemplatesType(); }

  getDocumentTemplatesType() {
    this.referenceService.getDocumentTemplateType().subscribe(res => this.documentTemplatesType = res.data);
  }

}
