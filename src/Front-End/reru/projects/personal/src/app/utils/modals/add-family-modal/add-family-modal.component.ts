import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { Observable, OperatorFunction, Subject, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-family-modal',
  templateUrl: './add-family-modal.component.html',
  styleUrls: ['./add-family-modal.component.scss']
})
export class AddFamilyModalComponent extends EnterSubmitListener implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractorId: number;
  familyForm: FormGroup;
  selectedRelation: SelectItem;
  relations: SelectItem[];
  isLoading: boolean = true;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  constructor(private activeModal: NgbActiveModal,
              private referenceService: ReferenceService,
              private fb: FormBuilder) {
    super();
    this.callback = this.close;
   }

  ngOnInit(): void {
    this.retrieveDropdownData();
  }

  retrieveDropdownData(): void {
    this.referenceService.listNomenclatureRecords({ nomenclatureBaseType: 4 }).subscribe(response => {
      this.relations = response.data;
      this.initForm();
      this.isLoading = false;
    });
  }

  initForm(): void {
    this.familyForm = this.fb.group({
      firstName: this.fb.control(null, [Validators.required]),
      lastName: this.fb.control(null, [Validators.required]),
      birthday: this.fb.control(null, [Validators.required]),
      relationId: this.fb.control(null, [Validators.required]),
      contractorId: this.fb.control(this.contractorId, []),
    });
  }

  close(): void {
    this.activeModal.close(this.familyForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  searchRelation: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.relations
        : this.relations.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectRelation(relation: SelectItem): void {
    if (relation) {
      this.familyForm.get('relationId').patchValue(relation.value);
    }
  }

  formatter = (x: SelectItem)=> x.label;

}
