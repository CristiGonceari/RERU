import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';

import {Observable, OperatorFunction, Subject, merge} from 'rxjs';
import {debounceTime, map, distinctUntilChanged, filter} from 'rxjs/operators';
import {NgbTypeahead} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-department-content-modal',
  templateUrl: './department-content-modal.component.html',
  styleUrls: ['./department-content-modal.component.scss']
})
export class DepartmentContentModalComponent implements OnInit {
  departmentForm: FormGroup;
  roles: SelectItem[] = [];
  isLoading: boolean = true;
  selectedItem:SelectItem;
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  constructor(private fb: FormBuilder,
              private referenceService: ReferenceService,
              private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.retrieveOrganizationRoles();
  }

  retrieveOrganizationRoles(): void {
    this.referenceService.listOrganizationRoles().subscribe(response => {
      this.roles = response.data;
      this.initForm();
      this.isLoading = false;
    });
  }

  initForm(): void {
    this.departmentForm = this.fb.group({
      organizationRoleId: this.fb.control(null, [Validators.required]),
      organizationRoleCount: this.fb.control(null, [Validators.required])
    });
  }

  close(): void {
    this.activeModal.close(this.departmentForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

  clickEvents($event, typeaheadInstance) {
    if (typeaheadInstance.isPopupOpen()) {
      this.click$.next($event.target.value);
    }
  }

  searchRole: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
  
    return merge(debouncedText$, this.focus$, this.click$).pipe(
      map(term => (term === '' ? this.roles
        : this.roles.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }
  formatter = (x:SelectItem)=>x.label;

  selectRole (event:SelectItem ){
    if (event)
      this.departmentForm.get("organizationRoleId").patchValue(event.value);
  }
}
