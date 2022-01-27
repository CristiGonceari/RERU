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
      id: this.fb.control(study.id),
      institution: this.fb.control({ value: study.institution, disabled: true }, [Validators.required]),
      studyFrequency: this.fb.control({ value: study.studyFrequency, disabled: true }, [Validators.required]),
      studyTypeId: this.fb.control({ value: study.studyTypeId, disabled: true }, [Validators.required]),
      faculty: this.fb.control({ value: study.faculty, disabled: true }, [Validators.required]),
      qualification: this.fb.control({ value: study.qualification, disabled: true }, [Validators.required]),
      specialty: this.fb.control({ value: study.specialty, disabled: true }, [Validators.required]),
      diplomaNumber: this.fb.control({ value: study.diplomaNumber, disabled: true }, [Validators.required]),
      diplomaReleaseDay: this.fb.control({ value: study.diplomaReleaseDay, disabled: true }, [Validators.required]),
      isActiveStudy: this.fb.control({ value: study.isActiveStudy, disabled: true }, [Validators.required]),
      contractorId: this.fb.control({ value: study.contractorId, disabled: true }, [])
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
