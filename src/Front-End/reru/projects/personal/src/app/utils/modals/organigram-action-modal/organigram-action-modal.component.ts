import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';

import { Observable, OperatorFunction, Subject, merge } from 'rxjs';
import { debounceTime, map, distinctUntilChanged } from 'rxjs/operators';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { ItemComponent } from '@swimlane/ngx-dnd';
import { PagedSummary } from '../../models/paged-summary.model';

@Component({
  selector: 'app-organigram-action-modal',
  templateUrl: './organigram-action-modal.component.html',
  styleUrls: ['./organigram-action-modal.component.scss']
})
export class OrganigramActionModalComponent implements OnInit {
  @Input() id: number;
  @Input() type: number;
  @Input() container: any;
  mode: number;
  organigramForm: FormGroup;
  heads: SelectItem[] = [];
  departments: SelectItem[] = [];
  roles: SelectItem[] = [];
  selectItem: SelectItem;

  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  
  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  @Input() users: any[] = [];
  @Input() pagedSummary: PagedSummary = new PagedSummary();

  constructor(private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private referenceService: ReferenceService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.retrieveDropdowns();
    this.initForm();
    this.subscribeForTypeChanges();
  }

  initForm(): void {
    this.organigramForm = this.fb.group({
      relationType: this.fb.control(null, [Validators.required]),
      childId: this.fb.control(null, [Validators.required])
    });
  }

  openList(): void {
    this.activeModal.close({ mode: this.mode = 3, data: { id: this.id, type: this.type } });
  }

  navigationContainer() {
    this.router.navigate(['../../department', this.container.id], { relativeTo: this.route });
    this.activeModal.dismiss();
  }

  navigateRole(id) {
    this.router.navigate(['../../roles', id], { relativeTo: this.route });
    this.activeModal.dismiss();
  }

  navigateFunction(id) {
    this.router.navigate(['../../employee-functions', id], { relativeTo: this.route });
    this.activeModal.dismiss();
  }

  close(): void {
    if (this.mode === 1 && this.organigramForm.invalid) return;
    this.activeModal.close({ mode: this.mode, data: this.parseRelation(this.organigramForm.value) });
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  retrieveDropdowns(): void {
    forkJoin([
      this.referenceService.listChartDepartments(this.id),
      this.referenceService.listChartOrganizationRoles(this.id)
    ]).subscribe(([departments, roles]) => {
      this.departments = departments.data;
      this.roles = roles.data;
      this.heads = [...departments.data];
    });
  }

  subscribeForTypeChanges(): void {
    this.organigramForm.get('relationType').valueChanges.subscribe(value => {
      this.organigramForm.get('childId').patchValue(null);
      if (value === '1' || value === '3') {
        this.heads = [...this.departments];
      }

      if (value === '2' || value === '4') {
        this.heads = [...this.roles]
      }
    });
  }

  parseRelation(data) {
    return {
      childId: +data.childId,
      relationType: +data.relationType
    }
  }

  clickEvents($event, typeaheadInstance) {
    if (typeaheadInstance.isPopupOpen()) {
      this.click$.next($event.target.value);
    }
  }

  searchHead: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    return merge(debouncedText$, this.focus$, this.click$).pipe(
      map(term => (term === '' ? this.heads
        : this.heads.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

  formatter = (x: SelectItem) => x.label;

  selectHead(event: SelectItem) {
    if (event)
      this.organigramForm.get("childId").patchValue(event.value);
  }
}
