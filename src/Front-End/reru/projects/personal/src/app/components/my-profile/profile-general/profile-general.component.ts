import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { Contractor } from '../../../utils/models/contractor.model';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ContractorService } from '../../../utils/services/contractor.service';
import { ReferenceService } from '../../../utils/services/reference.service';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';

@Component({
  selector: 'app-profile-general',
  templateUrl: './profile-general.component.html',
  styleUrls: ['./profile-general.component.scss']
})
export class ProfileGeneralComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractor: Contractor;
  originalContractor: Contractor;
  generalForm: FormGroup;
  bloodTypes: SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  isLoading: boolean = true;
  selectedItem: SelectItem;
  constructor(private fb: FormBuilder,
              private contractorService: ContractorService,
              private referenceService: ReferenceService) {}

  ngOnInit(): void {
    this.originalContractor = {...this.contractor};
    this.retrieveDropdowns();
    this.subscribeForContractorChanges();
  }

  subscribeForContractorChanges(): void {
    if (!this.contractorService.contractor.closed) {
      return;
    }

    this.contractorService.contractor.subscribe(response => {
      this.contractor = response || this.contractor;
      if (response) this.initForm(response);
    });
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 1 }).subscribe(response => {
      this.bloodTypes = response.data;
      this.initForm(this.contractor);
    });
  }

  initForm(contractor: Contractor = <Contractor>{}): void {
    this.generalForm = this.fb.group({
      id: this.fb.control(2),
      firstName: this.fb.control({value: contractor.firstName, disabled: true }, [Validators.required]),
      lastName: this.fb.control({value: contractor.lastName, disabled: true }, [Validators.required]),
      fatherName: this.fb.control({value: contractor.fatherName, disabled: true }, [Validators.required]),
      birthDate: this.fb.control({value: contractor.birthDate, disabled: true }, [Validators.required]),
      bloodTypeId: this.fb.control({value: contractor.bloodTypeId, disabled: true }, [Validators.required]),
      sex: this.fb.control({value: contractor.sex, disabled: true }, [Validators.required])
    });
    const bloodType = this.bloodTypes && this.bloodTypes.find(el => +el.value === contractor.bloodTypeId);
    this.selectedItem = bloodType;
    this.isLoading = false;
  }

  searchBloodType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.bloodTypes
        : this.bloodTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectBloodType(bloodType: SelectItem): void {
    this.generalForm.get('bloodTypeId').patchValue(bloodType ? +bloodType.value : null);
  }

  formatter = (x:SelectItem) => x.label;
}
