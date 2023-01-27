import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { SelectItem } from '../../models/select-item.model';

@Component({
  selector: 'erp-shared-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.scss']
})
export class SelectComponent {
  /* Simple Mode (filter mode) */
  /* Simple placeholder for non-value option */
  @Input() placeholder: string;
  /* Field that is selected for ngModel/FormGroup and keeps the selected value from items */
  @Input() field: string;
  /* Array of items */
  @Input() items: SelectItem[] = [];
  /* In case you want to add a format display for items for e.g. enums, translations etc... */
  @Input() isCustomDisplay: boolean = false;
  @Input() formatDisplay: string;
  @Input() isDisabled: boolean = false;

  /* Form Mode (create/edit) */
  @Input() formGroup: FormGroup;

  /* Including .form-control, defaults to .form-control-solid and .h-35-px */
  @Input() classes: string[] = ['form-control-solid', 'h-35px'];

  @Output() filter: EventEmitter<string> = new EventEmitter<string>();

  selectModel: string = '';
  constructor() { }
}
