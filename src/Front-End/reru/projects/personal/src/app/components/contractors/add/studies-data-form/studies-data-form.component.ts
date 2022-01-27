import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-studies-data-form',
  templateUrl: './studies-data-form.component.html',
  styleUrls: ['./studies-data-form.component.scss']
})
export class StudiesDataFormComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  isLoading: boolean;
  studyForm: FormGroup;
  studyTypes: SelectItem[];
  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [{label:'',value:''}];
  
  constructor(private referenceService: ReferenceService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.retrieveDropdowns();
  }

  initForm(): void {
    this.studyForm = this.fb.group({
      studies: this.fb.array([this.generateStudy()])
    });
  }

  handleStudyChange(event, form) {
    ValidatorUtil.handleStudyChange(event, form);
  }

  isInvalidPattern(form, field: string): boolean {
    return ValidatorUtil.isInvalidPattern(form, field);
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 5 }).subscribe(response => {
      this.studyTypes = response.data;

      this.initForm();

      this.isLoading = false;
    });
  }

  searchStudyType(index, text$) {
    return (text$: Observable<string>) => {
      const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
      const clicksWithClosedPopup$ = this.click$[index].pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
      const inputFocus$ = this.focus$[index];

      return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
        map(term => (term === '' ? this.studyTypes
          : this.studyTypes.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
    }
  }

  selectStudyType(studyType: SelectItem, index:number): void {
    (<FormArray>this.studyForm.controls.studies).controls[index].get('studyTypeId').patchValue(studyType ? +studyType.value : null);
  }

  generateStudy(): FormGroup {
    return this.fb.group({
      institution: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      studyFrequency: this.fb.control(null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyTypeId: this.fb.control(null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      faculty: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      qualification: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      specialty: this.fb.control(null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      diplomaNumber: this.fb.control(null, [Validators.required]),
      diplomaReleaseDay: this.fb.control(null, [Validators.required]),
      isActiveStudy: this.fb.control(false, [Validators.required]),
      contractorId: this.fb.control(null, [])
    });
  }

  addStudy(): void {
    this.focus$.push(new Subject<string>());
    this.click$.push(new Subject<string>());
    //this.selectedItems.push({label:'',value:''}); ????????
    (<FormArray>this.studyForm.controls.studies).controls.push(this.generateStudy());
  }

  removeStudy(index: number): void {
    this.focus$.splice(index, 1);
    this.click$.splice(index, 1);
    //this.selectedItems.splice(index, 1); ???????
    (<FormArray>this.studyForm.controls.studies).controls.splice(index, 1);
  }

  formatter = (x:SelectItem) => x.label;
}
