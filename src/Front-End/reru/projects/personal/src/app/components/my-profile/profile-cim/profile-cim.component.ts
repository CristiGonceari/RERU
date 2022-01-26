import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { ContractModel, InstructionModel } from '../../../utils/models/contract.model';
import { Contractor } from '../../../utils/models/contractor.model';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';
import { ReferenceService } from '../../../utils/services/reference.service';

@Component({
  selector: 'app-profile-cim',
  templateUrl: './profile-cim.component.html',
  styleUrls: ['./profile-cim.component.scss']
})
export class ProfileCimComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @ViewChild('instance1', {static: true}) instance1: NgbTypeahead;
  @Input() contractor: Contractor;
  isLoading: boolean = true;
  individualContractForm: FormGroup;
  selectedSuperior: SelectItem;
  selectedCurrency: SelectItem;
  superiors: SelectItem[];
  currencyTypes: SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  focus1$ = new Subject<string>();
  click1$ = new Subject<string>();
  contract: ContractModel;
  constructor(private fb: FormBuilder,
              private reference: ReferenceService,
              private contractorProfileService: ContractorProfileService) {}

    ngOnInit(): void {
      this.retrieveDropdowns();
    }

    retrieveDropdowns(): void {
      this.reference.listNomenclatureRecords({ nomenclaturebaseType: 2 }).subscribe(response => {
        this.currencyTypes = response.data;
        this.retrieveContractors();
      });
    }

    retrieveContract(): void {
      this.contractorProfileService.getContract({}).subscribe(response => {
        this.initForm(response.data);
        this.contract = {...response.data};
        this.selectedCurrency = this.currencyTypes.find(el => +el.value === this.contract.currencyTypeId)
        this.selectedSuperior = this.superiors.find(el => +el.value === this.contract.superiorId);
        this.isLoading = false;
      });
    }

    initForm(data: ContractModel): void {
      this.individualContractForm = this.fb.group({
        id: this.fb.control(data.id),
        no: this.fb.control({ value: data.no || '-', disabled: true }),
        superiorId: this.fb.control({ value: data.superiorId || 'null', disabled: true }),
        netSalary: this.fb.control({ value: data.netSalary, disabled: true }),
        brutSalary: this.fb.control({ value: data.brutSalary, disabled: true }),
        vacationDays: this.fb.control({ value: data.vacationDays, disabled: true }),
        currencyTypeId: this.fb.control({ value: data.currencyTypeId, disabled: true }),
        contractorId: this.fb.control(data.contractorId),
        instructions: this.fb.array(this.buildInstructions(data.instructions))
      });
    }

    buildInstructions(instructions: InstructionModel[]): FormGroup[] {
      if (!instructions || !instructions.length) {
        return [];
      }

      return instructions.map(el => this.buildInstruction(el));
    }

    buildInstruction(data: InstructionModel): FormGroup {
      return this.fb.group({
        id: this.fb.control({ value: data.id, disabled: true }),
        thematic: this.fb.control({ value: data.thematic, disabled: true }, []),
        instructorName: this.fb.control({ value: data.instructorName, disabled: true }, []),
        instructorLastName: this.fb.control({ value: data.instructorLastName, disabled: true }, []),
        duration: this.fb.control({ value: data.duration, disabled: true }, []),
        date: this.fb.control({ value: data.date, disabled: true }, []),
        contractorId: this.fb.control({value: data.contractorId, disabled: true }, [])
      });
    }

    retrieveContractors(): void {
      this.reference.listAllContractors().subscribe(response => {
        this.superiors = response.data;
        this.retrieveContract();
      })
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

  searchCurrencyType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click1$.pipe(filter(() => this.instance1 && !this.instance1.isPopupOpen()));
    const inputFocus$ = this.focus1$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.currencyTypes
        : this.currencyTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectCurrencyType(currencyType: SelectItem): void {
    if(currencyType){
      this.individualContractForm.get('currencyTypeId').patchValue(currencyType.value);
    }

  }
}
