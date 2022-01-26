import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { RankModel } from '../../models/rank.model';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-edit-rank-modal',
  templateUrl: './edit-rank-modal.component.html',
  styleUrls: ['./edit-rank-modal.component.scss']
})
export class EditRankModalComponent extends EnterSubmitListener implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @Input() contractorId: number;
  @Input() rank: RankModel;
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
   }

  ngOnInit(): void {
    this.retrieveRanks();
  }

  retrieveRanks(): void {
    this.referenceService.listNomenclatureRecords({ nomenclaturebaseType: 3 }).subscribe(response => {
      this.ranks = response.data;
      this.initRankDropdown(this.rank);
      this.initForm(this.rank);
    });
  }

  initRankDropdown(rank: RankModel): void {
    const rankItem = this.ranks.find(el => +el.value === rank.rankRecordId);
    this.selectedRank = rankItem;
  }

  initForm(data: RankModel): void {
    this.rankForm = this.fb.group({
      id: this.fb.control(data.id),
      contractorId: this.fb.control(this.contractorId),
      rankRecordId: this.fb.control(data.rankRecordId, [Validators.required]),
      from: this.fb.control(data.from, [Validators.required]),
      mentions: this.fb.control(data.mentions, [Validators.required])
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
