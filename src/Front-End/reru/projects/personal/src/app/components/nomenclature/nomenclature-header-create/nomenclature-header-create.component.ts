import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NomenclatureColumnModel, NomenclatureColumnsModel } from '../../../utils/models/nomenclature-columns.model';
import { NomenclatureColumnService } from '../../../utils/services/nomenclature-column.service';
import { AddNomenclatureHeaderModalComponent } from '../../../utils/modals/add-nomenclature-header-modal/add-nomenclature-header-modal.component';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ApiResponse } from '../../../utils/models/api-response.model';

@Component({
  selector: 'app-nomenclature-header-create',
  templateUrl: './nomenclature-header-create.component.html',
  styleUrls: ['./nomenclature-header-create.component.scss']
})
export class NomenclatureHeaderCreateComponent implements OnInit {
  @Output() created: EventEmitter<void> = new EventEmitter<void>();
  isLoading: boolean = true;
  columns: NomenclatureColumnModel[];
  nomenclatureColumnForm: FormGroup;
  nomenclatureRecordForm: FormArray;
  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private nomenclatureColumnService: NomenclatureColumnService,
              private modalService: NgbModal,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.susbcribeFormParams();
  }

  susbcribeFormParams(): void {
    this.route.params.subscribe(response => {
      this.retrieveNomenclatureForm(response.id);
    });
  }

  retrieveNomenclatureForm(id: number): void {
    this.nomenclatureColumnService.get(id).subscribe((response: ApiResponse<NomenclatureColumnModel>) => {
      this.initForm(response.data);
      this.isLoading = false;
    });
  }

  initForm(nomenclatureColumn: NomenclatureColumnModel = <NomenclatureColumnModel>{}): void {
    this.nomenclatureColumnForm = this.fb.group({
      nomenclatureTypeId: this.fb.control(nomenclatureColumn.nomenclatureTypeId),
      nomenclatureColumns: this.fb.array(this.buildNomenclatureColumns(nomenclatureColumn && nomenclatureColumn.nomenclatureColumns))
    })
  }

  buildNomenclatureColumns(columns: NomenclatureColumnsModel[]): FormGroup[] {
    if (!columns || !columns.length) {
      return [];
    }

    return columns.map((el: NomenclatureColumnsModel) => this.addColumnForm(el, true));
  }

  addColumnForm(data: NomenclatureColumnsModel, isEdit: boolean = false): FormGroup {
    if (isEdit) {
      return this.fb.group({
        id: this.fb.control(data.id),
        name: this.fb.control(data.name, [Validators.required]),
        type: this.fb.control(data.type, [Validators.required]),
        isMandatory: this.fb.control(data.isMandatory, [Validators.required]),
        order: this.fb.control(data.order, [Validators.required])
      });
    }

    return this.fb.group({
      name: this.fb.control(data.name, [Validators.required]),
      type: this.fb.control(data.type, [Validators.required]),
      isMandatory: this.fb.control(data.isMandatory, [Validators.required]),
      order: this.fb.control(data.order, [Validators.required])
    });
  }

  addColumn(): void {
    const order = !(<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls.length ? 1 : 
                   (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls.length + 1;
    const column = this.addColumnForm({
      name: 'Column',
      type: 1,
      isMandatory: false,
      order
    });
    (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls.push(column);
    this.openCreateHeaderModal(column, (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls.length - 1);
  }

  openCreateHeaderModal(form: FormGroup, index: number): void {
    const modalRef = this.modalService.open(AddNomenclatureHeaderModalComponent, { centered: true, backdrop: 'static'});
    modalRef.componentInstance.form = this.addColumnForm(form.value);
    modalRef.componentInstance.original = this.addColumnForm(form.value);
    modalRef.result.then((response) => this.updateNomenclatureColumn(response, index), 
                         (declined) => this.updateNomenclatureColumn(declined, index));
  }

  updateNomenclatureColumn(form: FormGroup, index: number): void {
    (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls[index] = form;
  }

  submit(): void {
    this.nomenclatureColumnService.update(this.nomenclatureColumnForm.getRawValue()).subscribe(() => {
      this.retrieveNomenclatureForm(this.nomenclatureColumnForm.get('nomenclatureTypeId').value);
      this.notificationService.success('Success', 'Tabelul a fost actualizat!', NotificationUtil.getDefaultConfig());
      this.created.emit();
    });
  }

  removeHeader(i: number): void {
    (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).removeAt(i);
  }

  handle(event) {
    // const length = +(<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls.length;
    // for(let i = 0; i < length; i++) {
    //   if (i + 1 < +event.dropIndex) {
    //     (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls[i].get('order').patchValue(i + 1);
    //   }

    //   if (i + 1 > +event.dropIndex) {
    //     (<FormArray>this.nomenclatureColumnForm.controls.nomenclatureColumns).controls[i].get('order').patchValue(i + 2);
    //   }
    // }
  }
}
