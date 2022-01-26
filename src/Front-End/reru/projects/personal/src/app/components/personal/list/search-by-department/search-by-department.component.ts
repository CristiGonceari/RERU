import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { TimesheetdatesService } from 'projects/personal/src/app/utils/services/timesheetdates.service';
import { Subject, forkJoin, OperatorFunction, Observable, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-search-by-department',
  templateUrl: './search-by-department.component.html',
  styleUrls: ['./search-by-department.component.scss']
})
export class SearchByDepartmentComponent implements OnInit {
  @Output() handleDepartment: EventEmitter<string> = new EventEmitter<string>();
  @ViewChild('instance1', {static: true}) instance1: NgbTypeahead;

  departments: SelectItem[];
  selectedDepartment: SelectItem;
  departmentId: number = null;

  focus1$ = new Subject<string>();
  click1$ = new Subject<string>();

  constructor(private reference: ReferenceService
    ) { }

  ngOnInit(): void {
    this.retrieveDropdownData();
  }

  formatter = (x:SelectItem)=>x.label;

  retrieveDropdownData(): void {
    forkJoin([
      this.reference.listDepartments(),
    ]).subscribe(([departments]) => {
      this.departments = departments.data;
    });
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

  selectDepartment(event: SelectItem): void {
    this.handleDepartment.emit(event && event.value);
  }

}
