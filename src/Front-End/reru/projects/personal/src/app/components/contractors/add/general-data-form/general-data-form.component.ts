import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-general-data-form',
  templateUrl: './general-data-form.component.html',
  styleUrls: ['./general-data-form.component.scss']
})
export class GeneralDataFormComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  generalForm: FormGroup;
  generalForm1:FormGroup;
  bloodTypes: SelectItem[];
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  isLoading: boolean = true;
  isRequired: boolean = true;
  contractorId: number;
  constructor(private referenceService: ReferenceService,
              private fb: FormBuilder,
              private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.subscribeForParams();
    this.initForm();
   
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if(response.id)
      {
        this.isLoading=false;
        this.contractorId=response.id;
        return;
      }
      this.retrieveDropdowns();
    })
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 1 }).subscribe(response => {
      this.bloodTypes = response.data;
      this.isLoading = false;
    });
  }
  
   initForm(): void {
     if(this.contractorId)
     {
      this.generalForm = this.fb.group({
        file: this.fb.control(null, [Validators.required])
      });
     }
     else {
     this.generalForm = this.fb.group({
       firstName: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z- ]+$/) ]),
       lastName: this.fb.control(null, [Validators.required , Validators.pattern(/^[a-zA-Z- ]+$/)]),
       fatherName: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z- ]+$/)]),
       birthDate: this.fb.control(null, [Validators.required]),
       bloodTypeId: this.fb.control(null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
       sex: this.fb.control(null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
     });}
   }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.generalForm, field);
  }

  isTouched(field: string): boolean {
    return ValidatorUtil.isTouched(this.generalForm, field);
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
