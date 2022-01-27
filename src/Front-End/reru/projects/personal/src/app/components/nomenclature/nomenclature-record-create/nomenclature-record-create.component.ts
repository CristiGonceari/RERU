import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { AddNomenclatureRecordModalComponent } from '../../../utils/modals/add-nomenclature-record-modal/add-nomenclature-record-modal.component';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { NomenclatureColumnsModel, NomenclatureColumnModel } from '../../../utils/models/nomenclature-columns.model';
import { NomenclatureRecordModel, RecordValuesModel } from '../../../utils/models/nomenclature-record.model';
import { NomenclatureColumnService } from '../../../utils/services/nomenclature-column.service';
import { NomenclatureRecordService } from '../../../utils/services/nomenclature-record.service';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-nomenclature-record-create',
  templateUrl: './nomenclature-record-create.component.html',
  styleUrls: ['./nomenclature-record-create.component.scss']
})
export class NomenclatureRecordCreateComponent implements OnInit {
  columns: NomenclatureColumnsModel[] = [];
  nomenclatureRecordForm: FormGroup;
  isLoading: boolean = true;
  nomenclatureTypeId: number;
  constructor(private route: ActivatedRoute,
              private nomenclatureRecordService: NomenclatureRecordService,
              private nomenclatureColumnService: NomenclatureColumnService,
              private fb: FormBuilder,
              private notificationService: NotificationsService,
              private modalService: NgbModal) { }

  ngOnInit(): void {
    this.susbcribeFormParams();
  }

  susbcribeFormParams(): void {
    this.route.params.subscribe(response => {
      this.retrieveNomenclatureColumns(response.id);
      this.retrieveNomenclatureRecords(response.id);
    });
  }

  retrieveNomenclatureColumns(id: number): void {
    this.nomenclatureColumnService.get(id).subscribe((response: ApiResponse<NomenclatureColumnModel>) => {
      this.nomenclatureTypeId = response.data.nomenclatureTypeId;
      this.columns = response.data.nomenclatureColumns;
    });
  }

  retrieveNomenclatureRecords(nomenclatureTypeId: number): void {
    this.nomenclatureRecordService.list({ nomenclatureTypeId }).subscribe((response: ApiResponse<any>) => {
      this.initRecordForm(response.data.items);
      this.isLoading = false;
    })
  }

  submit(): void {
    this.isLoading = true;
    const body = this.nomenclatureRecordForm.getRawValue();
    const requests = this.parseRequests(body.columns);
    forkJoin(requests).subscribe(response => {
      this.notificationService.success('Success', 'Nomenclator actualizat!', NotificationUtil.getDefaultConfig());
      this.retrieveNomenclatureColumns(this.nomenclatureTypeId);
      this.retrieveNomenclatureRecords(this.nomenclatureTypeId);
    });
  }

  parseRequests(columns) {
    const requests = [];
    columns.map(el => {
      if (el.id) {
        requests.push(this.nomenclatureRecordService.update(this.parseEdit(el)));
      } else {
        requests.push(this.nomenclatureRecordService.create(this.parseCreate(el)));
      }
    })
    return requests;
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

  initRecordForm(nomenclatureRecords): void {
    this.nomenclatureRecordForm = this.fb.group({
      columns: this.fb.array(this.buildNomenclatureRecords(nomenclatureRecords))
    });
  }

  buildNomenclatureRecords(records): FormGroup[] {
    if (!records || !records.length) return [];

    return records.map(el => this.addRecordForm(el));
  }

  getColumnType(id: number): number {
    if (!id) return;
    const column = this.columns.find(el => el.id === id);
    return column ? column.type : null;
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

  addRecord(): void {
    (<FormArray>this.nomenclatureRecordForm.controls.columns).controls.push(
      this.addRecordForm(<NomenclatureRecordModel>{recordValues: this.buildRecordValues()}
    ));
  }

  buildRecordValues(): RecordValuesModel[] {
    return this.columns.map(el => {
      return {type: el.type, nomenclatureColumnId: el.id, stringValue: ''}
    });
  }

  openEditRecordModal(form: FormGroup, index: number): void {
    const modalRef = this.modalService.open(AddNomenclatureRecordModalComponent, { centered: true, backdrop: 'static'});
    modalRef.componentInstance.form = this.addRecordForm(form.value);
    modalRef.componentInstance.original = this.addRecordForm(form.value);
    modalRef.componentInstance.columns = [...this.columns];
    modalRef.result.then((response) => this.updateRecordValueForm(response, index), 
                         (declined) => this.updateRecordValueForm(declined, index))
  }

  updateRecordValueForm(form: FormGroup, index: number): void {
    (<FormArray>this.nomenclatureRecordForm.controls.columns).controls[index] = form;
  }
}
