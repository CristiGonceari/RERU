import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbActiveModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { ContractorTimesheetDataModel } from '../../models/contractor-timesheet-data.model';
import { TimesheetDataModel } from '../../models/timesheet-data.model';
import { TimeSheetTableValuesService } from '../../services/time-sheet-table-values.service';

@Component({
  selector: 'app-add-edit-timesheet-values',
  templateUrl: './add-edit-timesheet-values.component.html',
  styleUrls: ['./add-edit-timesheet-values.component.scss']
})
export class AddEditTimesheetValuesComponent implements OnInit {
  
  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  @Input() value: TimesheetDataModel;
  @Input() contractor: ContractorTimesheetDataModel;

  timeSheet: SelectItem[];
  timeSheetValue: SelectItem;
  generalForm: FormGroup;

  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  isLoading: boolean = true;

  timeSheetSelectedValue: string;
  isClosed: boolean = false;
  infoBtn: boolean = false;

  constructor(private referenceService: ReferenceService,
    private timeSheetTableService: TimeSheetTableValuesService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.retrieveDropdowns()
    this.initForm()
  }

  selectTimeValuesType(timeSheet: SelectItem): void {
    this.generalForm.get('nomenculatureValueId').patchValue(timeSheet ? + timeSheet.value : null);
    this.timeSheetSelectedValue = timeSheet.value;
  }

  retrieveDropdowns(): void {
    this.referenceService.listTimeSheetValues().subscribe(response => {
      this.timeSheet = response.data;
      this.initForm();
      this.isLoading = false;
    });
  }

  initForm(): void {
    this.generalForm = this.fb.group({
      nomenculatureValueId: this.fb.control(null, [Validators.required]),
    })
  }

  searchTimeValuesType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.timeSheet
        : this.timeSheet.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  formatter = (x: SelectItem) => x.label;

  submit(selectItem:SelectItem, contractorId: number, date:any): void {
    let data = {
      contractorId: contractorId,
      date: date,
      valueId: + selectItem.value
    }
    this.timeSheetTableService.addEditTimeSheetTable({ data: data }).subscribe(() => {
      this.activeModal.close();
    });
    this.isLoading = false;
  }

  close(): void {
    this.isClosed = false;
    this.timeSheetSelectedValue = null;
    this.activeModal.close(this.timeSheetSelectedValue);
    this.dismiss();
  }

  dismiss(): void {
    this.timeSheetSelectedValue = null;
    this.activeModal.dismiss();
  }

  openInfo(){
    this.infoBtn = true;
  }
}
