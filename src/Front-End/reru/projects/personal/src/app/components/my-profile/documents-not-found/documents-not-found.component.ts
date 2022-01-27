import { Component, Input } from '@angular/core';
import { DocumentTypeEnum } from '../../../utils/models/document-type.enum';

@Component({
  selector: 'app-documents-not-found',
  templateUrl: './documents-not-found.component.html',
  styleUrls: ['./documents-not-found.component.scss']
})
export class DocumentsNotFoundComponent {
  @Input() selectedType: number;
  documentTypes = DocumentTypeEnum;
}

