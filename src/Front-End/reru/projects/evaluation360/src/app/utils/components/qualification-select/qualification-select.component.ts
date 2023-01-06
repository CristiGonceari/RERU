import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { isInvalidPattern, isInvalidRequired, isValid } from '../../util/forms.util';

@Component({
  selector: 'app-qualification-select',
  templateUrl: './qualification-select.component.html',
  styleUrls: ['./qualification-select.component.scss']
})
export class QualificationSelectComponent {
  @Input() formGroup: FormGroup;
  @Input() field: string;
  @Input() showNumbers: boolean;
  @Input() hasErrorSpace: boolean;

  @Output() handleChange: EventEmitter<string> = new EventEmitter<string>();
  isInvalidPattern: Function;
  isInvalidRequired
  isValid: Function;
  constructor() {
    this.isInvalidPattern = isInvalidPattern.bind(this);
    this.isInvalidRequired = isInvalidRequired.bind(this);
    this.isValid = isValid.bind(this);
   }
}
