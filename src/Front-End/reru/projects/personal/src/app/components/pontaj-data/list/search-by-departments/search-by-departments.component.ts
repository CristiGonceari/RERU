
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { TimesheetdatesService } from 'projects/personal/src/app/utils/services/timesheetdates.service';
import { forkJoin, merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-search-by-departments',
  templateUrl: './search-by-departments.component.html',
  styleUrls: ['./search-by-departments.component.scss']
})
export class SearchByDepartmentsComponent implements OnInit {
  @ViewChild('instance1', {static: true}) instance1: NgbTypeahead;

  departments: SelectItem[];
  selectedDepartment: SelectItem;
  departmentId: number = null;

  focus1$ = new Subject<string>();
  click1$ = new Subject<string>();

  constructor(private reference: ReferenceService,
    private timesheetdatesService: TimesheetdatesService) { }

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

  nextData(){
    if(this.selectedDepartment){
      this.departmentId = parseInt(this.selectedDepartment.value)
      this.timesheetdatesService.setData(this.departmentId);
    }
  }

  clearSearch(value: string): void {
    if (!value) {
      this.departmentId = null;
    }
   
  }

}
