import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NomenclatureTypeModel } from '../../../utils/models/nomenclature-type.model';

@Component({
  selector: 'app-nomenclature-dropdown-details',
  templateUrl: './nomenclature-dropdown-details.component.html',
  styleUrls: ['./nomenclature-dropdown-details.component.scss']
})
export class NomenclatureDropdownDetailsComponent {
  @Input() index: number;
  @Input() nomenclature: NomenclatureTypeModel;
  @Output() disable: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
