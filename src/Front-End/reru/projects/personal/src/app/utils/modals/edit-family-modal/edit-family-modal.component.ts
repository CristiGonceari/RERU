import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { Observable, OperatorFunction, Subject, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { FamilyModel } from '../../models/family.model';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-edit-family-modal',
  templateUrl: './edit-family-modal.component.html',
  styleUrls: ['./edit-family-modal.component.scss']
})
export class EditFamilyModalComponent extends EnterSubmitListener implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractorId: number;
  @Input() family: FamilyModel;
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
      this.initFamilyDropdown(this.family);
      this.initForm(this.family);
      this.isLoading = false;
    });
  }

  initFamilyDropdown(rank: FamilyModel): void {
    const familyItem = this.relations.find(el => +el.value === rank.relationId);
    this.selectedRelation = familyItem;
  }

  initForm(data: FamilyModel): void {
    this.familyForm = this.fb.group({
      id: this.fb.control(data.id),
      firstName: this.fb.control(data.firstName, [Validators.required]),
      lastName: this.fb.control(data.lastName, [Validators.required]),
      birthday: this.fb.control(data.birthday, [Validators.required]),
      relationId: this.fb.control(data.relationId, [Validators.required]),
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
