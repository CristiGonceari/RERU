import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';

@Component({
  selector: 'app-individual-contract-request',
  templateUrl: './individual-contract-request.component.html',
  styleUrls: ['./individual-contract-request.component.scss']
})
export class IndividualContractRequestComponent implements OnInit {
  @Input() contractorId: number;
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @ViewChild('instance1', {static: true}) instance1: NgbTypeahead;
  isLoading: boolean = false;
  individualContractForm: FormGroup;
  selectedSuperior: SelectItem;
  selectedCurrency: SelectItem;
  superiors: SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  currencies: SelectItem[];
  focus1$ = new Subject<string>();
  click1$ = new Subject<string>();

  constructor(private fb: FormBuilder,
              private reference: ReferenceService) {}

  ngOnInit(): void {
    this.initForm();
    this.retrieveDropdowns();
    this.retrieveContractors();
    this.retrieveCurrencies();
  }

  retrieveDropdowns(): void {
    this.reference.listNomenclatureRecords({ nomenclaturebaseType: 2 }).subscribe(response => {
      this.currencies = response.data;
      this.isLoading = false;
    });
  }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.individualContractForm, field);
  }
  
  isInvalidInstructionPattern(field: string): boolean{
    return ValidatorUtil.isInvalidPattern(<FormGroup>this.individualContractForm.get('instruction'), field);
  }

  isTouched(field: string): boolean {
    return ValidatorUtil.isTouched(this.individualContractForm, field);
  }

  initForm(): void {
    this.individualContractForm = this.fb.group({
      superiorId: this.fb.control(null,[]),
      netSalary: this.fb.control(null,[Validators.required, Validators.pattern(/^[0-9., ]+$/)]),
      brutSalary: this.fb.control(null,[Validators.required, Validators.pattern(/^[0-9., ]+$/)]),
      vacationDays: this.fb.control(28, [Validators.required, Validators.pattern(/^[0-9]+$/)]),
      contractorId: this.fb.control(null,[]),
      currencyTypeId: this.fb.control(null, [Validators.required]),
      instruction: this.buildInstruction()
    })
    this.individualContractForm.get('instruction').updateValueAndValidity();
  }

  buildInstruction(): FormGroup {
    return this.fb.group({
      thematic: this.fb.control(null, [Validators.pattern(/^[0-9a-zA-Z-., ]+$/)]),
      instructorName: this.fb.control(null, [Validators.pattern(/^[a-zA-Z- ]+$/)]),
      instructorLastName: this.fb.control(null, [Validators.pattern(/^[a-zA-Z- ]+$/)]),
      duration: this.fb.control(null, [Validators.pattern(/^[0-9]+$/)]),
      date: this.fb.control(null, []),
      contractorId: this.fb.control(null, [])
    });
  }

  retrieveContractors(): void {
    this.reference.listContractors(this.contractorId).subscribe(response => {
      this.superiors = response.data;
      this.initForm();
      this.isLoading = false;
    })
  }

  retrieveCurrencies(): void {
    this.reference.listNomenclatureRecords({ nomenclaturebaseType: 2 }).subscribe(response => {
      this.currencies = response.data;
      this.isLoading = false;
    });
  }

  formatter = (x:SelectItem)=> x.label;

  selectSuperior (event:SelectItem ){
    if (event) {
     this.individualContractForm.get('superiorId').patchValue(event.value);
    }
  }

  searchSuperior: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.superiors
        : this.superiors.filter(v =>v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

  searchCurrency: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click1$.pipe(filter(() => this.instance1 && !this.instance1.isPopupOpen()));
    const inputFocus$ = this.focus1$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.currencies
        : this.currencies.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectCurrency(currency: SelectItem): void {
    this.individualContractForm.get('currencyTypeId').patchValue(currency ? +currency.value : null);
  }
}
