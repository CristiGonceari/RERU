import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { AddNomenclatureHeaderModalComponent } from '../../../utils/modals/add-nomenclature-header-modal/add-nomenclature-header-modal.component';
import { AddNomenclatureRecordModalComponent } from '../../../utils/modals/add-nomenclature-record-modal/add-nomenclature-record-modal.component';
import { DeleteColumnModalComponent } from '../../../utils/modals/delete-column-modal/delete-column-modal.component';
import { NomenclatureColumnModel, NomenclatureColumnsModel } from '../../../utils/models/nomenclature-columns.model';
import { NomenclatureRecordModel, RecordValuesModel } from '../../../utils/models/nomenclature-record.model';
import { I18nService } from '../../../utils/services/i18n.service';
import { NomenclatureColumnService } from '../../../utils/services/nomenclature-column.service';
import { NomenclatureRecordService } from '../../../utils/services/nomenclature-record.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { ObjectUtil } from '../../../utils/util/object.util';


@Component({
  selector: 'app-nomenclature-record',
  templateUrl: './nomenclature-record.component.html',
  styleUrls: ['./nomenclature-record.component.scss']
})
export class NomenclatureRecordComponent implements OnInit {
  isLoading: boolean = true;
  columns: NomenclatureColumnsModel[];
  nomenclatureForm: FormGroup;
  nomenclatureRecordForm: FormArray;
  nomenclatureTypeId: number;
  pagedSummary: PagedSummary = {
    totalCount: 0,
    pageSize: 0,
    currentPage: 1,
    totalPages: 1
  };
  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private nomenclatureColumnService: NomenclatureColumnService,
              private nomenclatureRecordService: NomenclatureRecordService,
              private modalService: NgbModal,
              private notificationService: NotificationsService,
              private translate: I18nService) { }

  ngOnInit(): void {
    this.susbcribeFormParams();
  }

  susbcribeFormParams(): void {
    this.route.params.subscribe(response => {
      this.retrieveNomenclatureForm(response.id);
    });
  }

  retrieveNomenclatureForm(id: number): void {
    forkJoin([
      this.nomenclatureColumnService.get(id),
      this.nomenclatureRecordService.list({ nomenclatureTypeId: id, page: this.pagedSummary.currentPage })
    ]).subscribe(([response, list]) => {
      this.nomenclatureTypeId = id;
      this.columns = response.data.nomenclatureColumns;
      this.initForm(response.data, (list.data as any).items);
      this.pagedSummary = (list.data as any).pagedSummary;
      this.isLoading = false;
    });
  }

  retrieveRecordList(data:any): void {
    const request = ObjectUtil.preParseObject({
      page: data.page,
      nomenclatureTypeId: this.nomenclatureTypeId,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    });
    this.nomenclatureRecordService.list(request).subscribe((response:any) => {
      this.initForm({nomenclatureTypeId:this.nomenclatureTypeId,nomenclatureColumns:this.columns}, response.data.items);
      this.pagedSummary=response.data.pagedSummary;
      this.isLoading = false;
    });
  }

  initForm(nomenclatureColumn: NomenclatureColumnModel = <NomenclatureColumnModel>{}, nomenclatureRecords): void {
    this.nomenclatureForm = this.fb.group({
      nomenclatureTypeId: this.fb.control(nomenclatureColumn.nomenclatureTypeId),
      nomenclatureColumns: this.fb.array(this.buildNomenclatureColumns(nomenclatureColumn && nomenclatureColumn.nomenclatureColumns)),
      columns: this.fb.array(this.buildNomenclatureRecords(nomenclatureRecords))      
    })
  }

  buildNomenclatureRecords(records): FormGroup[] {
    if (!records || !records.length) return [];

    return records.map(el => this.addRecordForm(el));
  }

  buildNomenclatureColumns(columns: NomenclatureColumnsModel[]): FormGroup[] {
    if (!columns || !columns.length) {
      return [];
    }

    return columns.map((el: NomenclatureColumnsModel) => this.addColumnForm(el, true));
  }

  addRecord(): void {
    this.openRecordModal(this.addRecordForm(<NomenclatureRecordModel>{ recordValues: this.buildRecordValues() }), false);
  }

  buildRecordValues(): RecordValuesModel[] {
    return this.columns.map(el => {
      return { type: el.type, nomenclatureColumnId: el.id, stringValue: '' }
    });
  }

  addRecordForm(record: NomenclatureRecordModel = <NomenclatureRecordModel>{}): FormGroup {
    return this.fb.group({
      id: this.fb.control(record.id),
      name: this.fb.control(record.name),
      isActive: this.fb.control(record.isActive || true),
      nomenclatureTypeId: this.fb.control(record.nomenclatureTypeId || this.nomenclatureTypeId),
      recordValues: this.fb.array(this.addRecordValues(record.recordValues))
    })
  }

  addRecordValues(values: RecordValuesModel[]): FormGroup[] {
    if (!values || !values.length) return [];

    return values.map(el => this.addRecordValue(el));
  }

  addRecordValue(value: RecordValuesModel): FormGroup {
    return this.fb.group({
      id: this.fb.control(value.id),
      nomenclatureRecordId: this.fb.control(value.nomenclatureRecordId),
      nomenclatureColumnId: this.fb.control(value.nomenclatureColumnId),
      stringValue: this.fb.control(this.parseFromStringValue(value.nomenclatureColumnId, value.stringValue)),
      type: this.fb.control(value.type)
    });
  }

  parseFromStringValue(id: number, value:string): string|boolean {
    const column = this.columns.find(el => el.id === id);
    const type = column.type;
    if (type === 5) {
      return value && (value+'').toLowerCase() === 'true' ? true : false;
    }

    return value;
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

  addColumn(isEdit: boolean): void {
    const order = !(<FormArray>this.nomenclatureForm.controls.nomenclatureColumns).controls.length ? 1 : 
                   (<FormArray>this.nomenclatureForm.controls.nomenclatureColumns).controls.length + 1;
    const column = this.addColumnForm({
      name: '',
      type: 1,
      isMandatory: false,
      order
    });
    // (<FormArray>this.nomenclatureForm.controls.nomenclatureColumns).controls.push(column);
    this.openCreateHeaderModal(column, (<FormArray>this.nomenclatureForm.controls.nomenclatureColumns).controls.length, isEdit);
  }

  getColumnType(id: number): number {
    if (!id) return;
    const column = this.columns.find(el => el.id === id);
    return column ? column.type : null;
  }

  openCreateHeaderModal(form: FormGroup, index: number, isEdit: boolean = false): void {
    const modalRef = this.modalService.open(AddNomenclatureHeaderModalComponent, { centered: true, backdrop: 'static'});
    modalRef.componentInstance.form = this.addColumnForm(form.value, isEdit);
    modalRef.componentInstance.original = this.addColumnForm(form.value, isEdit);
    modalRef.result.then((response) => this.updateNomenclatureColumn(response, index), 
                         (declined) => {});
  }

  updateNomenclatureColumn(form: FormGroup, index: number): void {
    (<FormArray>this.nomenclatureForm.controls.nomenclatureColumns).controls[index] = form;
    this.updateColumns();
  }

  updateColumns(): void {
    const request = this.parseColumns(this.nomenclatureForm.getRawValue());
    this.nomenclatureColumnService.update(request).subscribe(() => {
      this.retrieveNomenclatureForm(this.nomenclatureForm.get('nomenclatureTypeId').value);
      this.notificationService.success('Success', 'Tabelul a fost actualizat!', NotificationUtil.getDefaultConfig());
    });
  }

  parseColumns(data) {
    return {
      nomenclatureTypeId: data.nomenclatureTypeId,
      nomenclatureColumns: data.nomenclatureColumns
    }
  }

  openDeleteColumnModal(index: number): void {
    const modalRef = this.modalService.open(DeleteColumnModalComponent, { centered: true, backdrop: 'static' });
    modalRef.result.then(() => this.removeHeader(index), () => {});
  }

  removeHeader(i: number): void {
    (<FormArray>this.nomenclatureForm.controls.nomenclatureColumns).removeAt(i);
    this.updateColumns();
  }

  openRecordModal(form: FormGroup, isEdit: boolean): void {
    const modalRef = this.modalService.open(AddNomenclatureRecordModalComponent, { centered: true, backdrop: 'static'});
    modalRef.componentInstance.form = this.addRecordForm(form.value);
    modalRef.componentInstance.original = this.addRecordForm(form.value);
    modalRef.componentInstance.columns = [...this.columns];
    modalRef.result.then((response) => isEdit ? this.updateRecord(response.getRawValue()) : this.createRecord(response.getRawValue()), () => {})
  }

  createRecord(data) {
    this.isLoading = true;
    this.nomenclatureRecordService.create(this.parseCreate(data)).subscribe(() => {
      this.notificationService.success('Success', 'Camp creat!', NotificationUtil.getDefaultConfig());
      this.retrieveNomenclatureForm(this.nomenclatureTypeId);
    });
  }

  updateRecord(data) {
    this.isLoading = true;
    this.nomenclatureRecordService.update(this.parseEdit(data)).subscribe(() => {
      this.notificationService.success('Success', 'Camp actualizat!', NotificationUtil.getDefaultConfig());
      this.retrieveNomenclatureForm(this.nomenclatureTypeId);
    });
  }

  parseCreate(data: NomenclatureRecordModel): NomenclatureRecordModel {
    if (!data.id) data.id = 0;

    data.recordValues = data.recordValues.map(el => {
      if (!el.id) el.id = 0;
      if (!el.nomenclatureRecordId) el.nomenclatureRecordId = 0;
      el.stringValue = el.stringValue+'';
      delete el.type;
      return el;
    })

    return data;
  }

  parseEdit(data: NomenclatureRecordModel): NomenclatureRecordModel {
    if (!data.id) data.id = 0;

    data.recordValues = data.recordValues.map(el => {
      if (!el.id) el.id = 0;
      if (!el.nomenclatureRecordId) el.nomenclatureRecordId = 0;
      el.stringValue = this.parseToStringValue(el);
      delete el.type;
      return el;
    })

    return data;
  }

  parseToStringValue(el: RecordValuesModel): string {
    if (el.type === 5) {
      return (!!el.stringValue)+'';
    }

    return el.stringValue+'';
  }
}
