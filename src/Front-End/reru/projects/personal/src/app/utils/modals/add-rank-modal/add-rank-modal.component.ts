import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { OperatorFunction, merge, Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-rank-modal',
  templateUrl: './add-rank-modal.component.html',
  styleUrls: ['./add-rank-modal.component.scss']
})
export class AddRankModalComponent extends EnterSubmitListener implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractorId: number;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  rankForm: FormGroup;
  isLoading: boolean = true;
  ranks: SelectItem[];
  selectedRank: SelectItem;
  constructor(private fb: FormBuilder,
              private activeModal: NgbActiveModal,
              private referenceService: ReferenceService) {
    super();
    this.callback = this.close;
   }

  ngOnInit(): void {
    this.retrieveRanks();
  }

  retrieveRanks(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 3 }).subscribe(response => {
      this.ranks = response.data;
      this.initForm();
    });
  }

  initForm(): void {
    this.rankForm = this.fb.group({
      contractorId: this.fb.control(this.contractorId),
      rankRecordId: this.fb.control(null, [Validators.required]),
      from: this.fb.control(null, [Validators.required]),
      mentions: this.fb.control(null, [Validators.required])
    });
    this.isLoading = false;
  }

  close(): void {
    this.activeModal.close(this.rankForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  searchRank: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.ranks
        : this.ranks.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1))));
  }

  selectRank(rank: SelectItem): void {
    if (rank){
      this.rankForm.get('rankRecordId').patchValue(rank.value);
    }
  }

  formatter = (x: SelectItem)=> x.label;
}
