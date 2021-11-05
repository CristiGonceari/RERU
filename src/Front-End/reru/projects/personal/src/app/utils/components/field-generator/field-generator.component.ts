import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FieldTypeEnum } from '../../../utils/models/field-type.model';

@Component({
  selector: 'app-field-generator',
  templateUrl: './field-generator.component.html',
  styleUrls: ['./field-generator.component.scss']
})
export class FieldGeneratorComponent{
  @Input() type: number;
  @Input() field: string;
  @Input() form: FormGroup;
  fieldType = FieldTypeEnum;
  constructor() { }
}
