import { Component, Input } from '@angular/core';
import { NomenclatureTypeModel } from '../../../utils/models/nomenclature-type.model';

@Component({
  selector: 'app-nomenclature-status-label',
  templateUrl: './nomenclature-status-label.component.html',
  styleUrls: ['./nomenclature-status-label.component.scss']
})
export class NomenclatureStatusLabelComponent {
  @Input() nomenclature: NomenclatureTypeModel;
  constructor() { }
}
