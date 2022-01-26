import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { ValidatorUtil } from 'projects/personal/src/app/utils/util/validator.util';
import { forkJoin, merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-add-position',
  templateUrl: './add-position.component.html',
  styleUrls: ['./add-position.component.scss']
})
export class AddPositionComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  @ViewChild('instance1', {static: true}) instance1: NgbTypeahead;
  isLoading: boolean;
  positionForm: FormGroup;
  roles: SelectItem[];
  departments: SelectItem[];
  selectedRole: SelectItem;
  selectedDepartment: SelectItem;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  focus1$ = new Subject<string>();
  click1$ = new Subject<string>();
  constructor(private fb: FormBuilder,
              private reference: ReferenceService) { }

  ngOnInit(): void {
    this.retrieveDropdownData();
    this.initForm();
  }

  initForm(): void {
    this.positionForm = this.fb.group({
      fromDate: this.fb.control(null, [Validators.required]),
      generatedDate: this.fb.control(null, [Validators.required]),
      probationDayPeriod: this.fb.control(null, [Validators.required, Validators.pattern(/^[0-9]+$/)]),
      workHours: this.fb.control(null, [Validators.required, ValidatorUtil.isNotNullString.bind(this)]),
      organizationRoleId: this.fb.control(null, [Validators.required]),
      departmentId: this.fb.control(null, [Validators.required]),
      contractorId: this.fb.control(null, []),
      workPlace: this.fb.control(null, [Validators.required, Validators.pattern(/^[0-9a-zA-Z-,.\"\" ]+$/)])
    });
  }

  isInvalidPattern(field: string): boolean {
    return ValidatorUtil.isInvalidPattern(this.positionForm, field);
  }

  isTouched(field: string): boolean {
    return ValidatorUtil.isTouched(this.positionForm, field);
  }

  retrieveDropdownData(): void {
    forkJoin([
      this.reference.listDepartments(),
      this.reference.listOrganizationRoles()
    ]).subscribe(([departments, roles]) => {
      this.departments = departments.data;
      this.roles = roles.data;
    });
  }

  searchRole: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;
  
    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.roles
        : this.roles.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

  formatter = (x:SelectItem)=>x.label;

  selectRole (event:SelectItem ){
    if (event) {
     this.positionForm.get('organizationRoleId').patchValue(event.value);
    }
  }

  selectDepartment (event:SelectItem ){
    if (event) {
     this.positionForm.get('departmentId').patchValue(event.value);
    }
  }

  searchDepartment: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click1$.pipe(filter(() => this.instance1 && !this.instance1.isPopupOpen()));
    const inputFocus$ = this.focus1$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.departments
        : this.departments.filter(v =>v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

}
