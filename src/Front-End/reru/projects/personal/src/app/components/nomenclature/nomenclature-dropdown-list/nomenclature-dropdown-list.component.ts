import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NomenclatureTypeModel } from '../../../utils/models/nomenclature-type.model';

@Component({
  selector: 'app-nomenclature-dropdown-list',
  templateUrl: './nomenclature-dropdown-list.component.html',
  styleUrls: ['./nomenclature-dropdown-list.component.scss']
})
export class NomenclatureDropdownListComponent {
  @Input() index: number;
  @Input() nomenclature: NomenclatureTypeModel;
  @Output() disable: EventEmitter<void> = new EventEmitter<void>();
  constructor() { }
}
