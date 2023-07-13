import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { StudyModel } from '../../models/study.model';
import { EnterSubmitListener } from '../../util/submit.util';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { ValidatorUtil } from '../../util/validator.util';


@Component({
  selector: 'app-edit-stydy-modal',
  templateUrl: './edit-stydy-modal.component.html',
  styleUrls: ['./edit-stydy-modal.component.scss']
})
export class EditStydyModalComponent extends EnterSubmitListener implements OnInit {
  @ViewChild('instance', {static: false}) instance2: NgbTypeahead;
  @Input() study: StudyModel;
  studyForm: FormGroup;

  selectedItem2: SelectItem = {label:'',value:''};

  studyTypes2: SelectItem[];
  focus2$ = new Subject<string>();
  click2$ = new Subject<string>();

  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder, private referenceService: ReferenceService) {
    super();
    this.callback = this.close;
   }

  ngOnInit(): void {
    this.retrieveDropdowns();
  }

  retrieveDropdowns(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 5 }).subscribe(response => {
      this.studyTypes2 = response.data;
      this.initForm(this.study);
    });
  }

  initForm(study: StudyModel = <StudyModel>{}): void {
    this.studyForm = this.fb.group({
      id: this.fb.control((study && study.id) || null, []),
      institution: this.fb.control((study && study.institution) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      institutionAddress: this.fb.control((study && study.institutionAddress) || null),
      studyTypeId: this.fb.control((study && study.studyTypeId) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyFrequency: this.fb.control((study && study.studyFrequency) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyProfile: this.fb.control((study && study.studyProfile) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      studyCourse: this.fb.control((study && study.studyCourse) || null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      faculty: this.fb.control((study && study.faculty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\- ]+$/)]),
      specialty: this.fb.control((study && study.specialty) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      yearOfAdmission: this.fb.control((study && study.yearOfAdmission) || null, [Validators.required, Validators.maxLength(4), Validators.minLength(4), Validators.pattern(/^[0-9]*$/)]),
      graduationYear: this.fb.control((study && study.graduationYear) || null, [Validators.required, Validators.maxLength(4), Validators.minLength(4), Validators.pattern(/^[0-9]*$/)]),
      startStudyPeriod: this.fb.control((study && study.startStudyPeriod) || null, [Validators.required]),
      endStudyPeriod: this.fb.control((study && study.endStudyPeriod) || null, [Validators.required]),
      title: this.fb.control((study && study.title) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      qualification: this.fb.control((study && study.qualification) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-ZĂăÎîȘŞșşȚŢțţÂâ\-,. ]+$/)]),
      creditCount: this.fb.control((study && study.creditCount) || null, [Validators.required]),
      studyActSeries: this.fb.control((study && study.studyActSeries) || null, [Validators.required, Validators.pattern(/^[a-zA-Z-,. ]+$/)]),
      studyActNumber: this.fb.control((study && study.studyActNumber) || null, [Validators.required, Validators.pattern(/^[0-9]+(\.?[0-9]+)?$/)]),
      studyActRelaseDay: this.fb.control((study && study.studyActRelaseDay) || null, [Validators.required]),
      contractorId: this.fb.control(study.contractorId || null, [])
    });

    this.selectedItem2 = this.studyTypes2 && this.studyTypes2.find(el => +el.value === study.studyTypeId);

  }

  searchStudyType: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click2$.pipe(filter(() => this.instance2 && !this.instance2.isPopupOpen()));
    const inputFocus$ = this.focus2$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.studyTypes2
        : this.studyTypes2.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectStudyType2(studyType: SelectItem): void {
    this.studyForm.get('studyTypeId').patchValue(studyType ? +studyType.value : null);
  }

  close(): void {
    this.activeModal.close(this.studyForm.value);
  } 

  dismiss(): void {
    this.activeModal.dismiss();
  }

  formatter = (x:SelectItem) => x.label;
}
