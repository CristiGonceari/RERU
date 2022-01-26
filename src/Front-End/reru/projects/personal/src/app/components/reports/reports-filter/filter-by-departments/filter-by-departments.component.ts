import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { forkJoin, merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-filter-by-departments',
  templateUrl: './filter-by-departments.component.html',
  styleUrls: ['./filter-by-departments.component.scss']
})
export class FilterByDepartmentsComponent implements OnInit {

  @Output() handleDepartment: EventEmitter<string> = new EventEmitter<string>();

  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  
  departments: SelectItem[];
  selectedDepartment: SelectItem;
  departmentId: number = null;

  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  
  constructor(private reference: ReferenceService) { }

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
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.departments
        : this.departments.filter(v =>v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

  selectDepartment(event: SelectItem): void {
    this.handleDepartment.emit(event && event.value);
  }

} 
