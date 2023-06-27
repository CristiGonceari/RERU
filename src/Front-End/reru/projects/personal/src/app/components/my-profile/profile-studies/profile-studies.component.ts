import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { Contractor } from '../../../utils/models/contractor.model';
import { PagedSummary } from '../../../utils/models/paged-summary.model';
import { StudyModel } from '../../../utils/models/study.model';
import { ContractorProfileService } from '../../../utils/services/contractor-profile.service';
import { SelectItem } from '../../../utils/models/select-item.model';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { ReferenceService } from '../../../utils/services/reference.service';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ValidatorUtil } from '../../../utils/util/validator.util';

@Component({
  selector: 'app-profile-studies',
  templateUrl: './profile-studies.component.html',
  styleUrls: ['./profile-studies.component.scss']
})
export class ProfileStudiesComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractor: Contractor;
  isLoading: boolean = true;
  studyForm: FormGroup;
  studies: StudyModel[];

  studyTypes: SelectItem[];
  focus$: Subject<string>[] = [new Subject<string>()];
  click$: Subject<string>[] = [new Subject<string>()];
  selectedItems: SelectItem[] = [];

  pagedSummary: PagedSummary = {
    totalCount: 1,
    totalPages: 1,
    pageSize: 10,
    currentPage: 1
  }
  constructor(private fb: FormBuilder,
              private contractorProfileService: ContractorProfileService,
              private referenceService: ReferenceService) { }

  ngOnInit(): void {
    this.retrieveDropdowns();
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 5 }).subscribe(response => {
      this.studyTypes = response.data;

      this.retrieveStudies();

      this.isLoading = false;
    });
  }

  retrieveStudies(data: any = {}): void {
    const request = {
      page: data.page || this.pagedSummary.currentPage,
      itemsPerPage: data.itemsPerPage || this.pagedSummary.pageSize
    }
    this.contractorProfileService.getStudies(request).subscribe((response: ApiResponse<any>) => {
      this.studies = [...response.data.items];
      this.pagedSummary = response.data.pagedSummary;
      this.initForm(response.data.items);
    });
  }

  initForm(studies: StudyModel[]): void {
    studies.forEach((element, index) => {
      this.selectedItems[index] = this.studyTypes && this.studyTypes.find(el => +el.value === element.studyTypeId);
    });

    this.studyForm = this.fb.group({
      studies: this.fb.array(this.buildStudies(studies))
    });

    this.isLoading = false;
  }

  buildStudies(studies): StudyModel[] {
    return studies.map((el: StudyModel) => this.generateStudy(el));
  }

  generateStudy(study: StudyModel): FormGroup {
    return this.fb.group({
      id: this.fb.control((study && study.id) || null, []),
      institution: this.fb.control((study && study.institution) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ăâîșțĂÂÎȘȚ\-,. ]+$/)]),
      institutionAddress: this.fb.control((study && study.institutionAddress) || null),
      studyTypeId: this.fb.control((study && study.studyTypeId) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyFrequency: this.fb.control((study && study.studyFrequency) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyProfile: this.fb.control((study && study.studyProfile) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyCourse: this.fb.control((study && study.studyCourse) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      faculty: this.fb.control((study && study.faculty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ăâîșțĂÂÎȘȚ\- ]+$/)]),
      specialty: this.fb.control((study && study.specialty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ăâîșțĂÂÎȘȚ\-,. ]+$/)]),
      yearOfAdmission: this.fb.control((study && study.yearOfAdmission) || null, [Validators.required, Validators.maxLength(4), Validators.minLength(4), Validators.pattern(/^[0-9]*$/)]),
      graduationYear: this.fb.control((study && study.graduationYear) || null, [Validators.required, Validators.maxLength(4), Validators.minLength(4), Validators.pattern(/^[0-9]*$/)]),
      startStudyPeriod: this.fb.control((study && study.startStudyPeriod) || null, [Validators.required]),
      endStudyPeriod: this.fb.control((study && study.endStudyPeriod) || null, [Validators.required]),
      title: this.fb.control((study && study.title) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ăâîșțĂÂÎȘȚ\-,. ]+$/)]),
      qualification: this.fb.control((study && study.qualification) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ăâîșțĂÂÎȘȚ\-,. ]+$/)]),
      creditCount: this.fb.control((study && study.creditCount) || null, [Validators.required]),
      studyActSeries: this.fb.control((study && study.studyActSeries) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      studyActNumber: this.fb.control((study && study.studyActNumber) || null, [Validators.required, Validators.pattern(/^[0-9]+(\.?[0-9]+)?$/)]),
      studyActRelaseDay: this.fb.control((study && study.studyActRelaseDay) || null, [Validators.required]),
      contractorId: this.fb.control(study.contractorId || null, [])
    });
  }

  selectStudyType(studyType: SelectItem, index:number): void {
    (<FormArray>this.studyForm.controls.studies).controls[index].get('studyTypeId').patchValue(studyType ? +studyType.value : null);
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

  formatter = (x:SelectItem) => x.label;
}
