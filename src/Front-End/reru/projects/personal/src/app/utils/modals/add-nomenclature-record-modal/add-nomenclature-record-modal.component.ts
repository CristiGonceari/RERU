import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NomenclatureColumnsModel } from '../../models/nomenclature-columns.model';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-nomenclature-record-modal',
  templateUrl: './add-nomenclature-record-modal.component.html',
  styleUrls: ['./add-nomenclature-record-modal.component.scss']
})
export class AddNomenclatureRecordModalComponent extends EnterSubmitListener {
  @Input() form: FormGroup;
  @Input() original: FormGroup;
  @Input() columns: NomenclatureColumnsModel[];
  constructor(private activeModal: NgbActiveModal) {
    super();
    this.callback = this.close;
   }

  close(): void {
    this.activeModal.close(this.form);
  }

  dismiss(): void {
    this.activeModal.dismiss(this.original);
  }

  getColumnType(id: number): number {
    if (!id) return;
    const column = this.columns.find(el => el.id === id);
    return column ? column.type : null;
  }

  getColumnName(id: number): string {
    if (!id) return;
    const column = this.columns.find(el => el.id === id);
    return column ? column.name : null;
  }
}
