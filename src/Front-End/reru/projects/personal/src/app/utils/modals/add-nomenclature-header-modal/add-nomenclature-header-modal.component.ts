import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-nomenclature-header-modal',
  templateUrl: './add-nomenclature-header-modal.component.html',
  styleUrls: ['./add-nomenclature-header-modal.component.scss']
})
export class AddNomenclatureHeaderModalComponent extends EnterSubmitListener {
  @Input() form: FormGroup;
  @Input() original: FormGroup;
  constructor(private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  close(): void {
    this.form.get('type').patchValue(+this.form.get('type').value);
    this.activeModal.close(this.form);
  }

  dismiss(): void {
    this.activeModal.dismiss(this.original);
  }
}
