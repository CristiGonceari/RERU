import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DepartmentContentTypeEnum, DepartmentTemplateModel, DepartmentActualModel, DepartmentSummaryModel } from '../../../utils/models/department-content.model';

@Component({
  selector: 'app-department-content-card',
  templateUrl: './department-content-card.component.html',
  styleUrls: ['./department-content-card.component.scss']
})
export class DepartmentContentCardComponent {
  @Input() template: DepartmentTemplateModel;
  @Input() entry: DepartmentTemplateModel | DepartmentActualModel | DepartmentSummaryModel;
  @Input() type: DepartmentContentTypeEnum;
  @Output() openModal: EventEmitter<void> = new EventEmitter<void>();
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();
  contentType: typeof DepartmentContentTypeEnum = DepartmentContentTypeEnum;
  constructor() { }
}