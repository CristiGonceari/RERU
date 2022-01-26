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

  initForm(data: StudyModel = <StudyModel>{}): void {
    this.studyForm = this.fb.group({
      id: this.fb.control(data.id),
      institution: this.fb.control(data.institution, [Validators.required]),
      studyFrequency: this.fb.control(data.studyFrequency, [Validators.required]),
      studyTypeId: this.fb.control(data.studyTypeId, [Validators.required]),
      faculty: this.fb.control(data.faculty, [Validators.required]),
      qualification: this.fb.control(data.qualification, [Validators.required]),
      specialty: this.fb.control(data.specialty, [Validators.required]),
      diplomaNumber: this.fb.control(data.diplomaNumber, [Validators.required]),
      diplomaReleaseDay: this.fb.control(data.diplomaReleaseDay, [Validators.required]),
      isActiveStudy: this.fb.control(!!data.isActiveStudy, [Validators.required]),
      contractorId: this.fb.control(data.contractorId, [])
    });

    this.selectedItem2 = this.studyTypes2 && this.studyTypes2.find(el => +el.value === data.studyTypeId);

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
