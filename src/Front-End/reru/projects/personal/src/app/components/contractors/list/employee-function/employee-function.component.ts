import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { Subject, OperatorFunction, Observable, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-employee-function',
  templateUrl: './employee-function.component.html',
  styleUrls: ['./employee-function.component.scss']
})
export class EmployeeFunctionComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  selectedFunction: SelectItem;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  functions: SelectItem[];
  @Output() handleChangeFunction: EventEmitter<string> = new EventEmitter<string>();
  constructor(private reference: ReferenceService) { }

  ngOnInit(): void {
    this.retrieveDropdownData();
  }

  retrieveDropdownData(): void {
    this.reference.getEmployeeFunctions().subscribe(res => {
      this.functions = res.data;
    });
  }

  searchFunction: OperatorFunction<string, readonly SelectItem[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => this.instance && !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;
  
    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.functions
        : this.functions.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }

  formatter = (x: SelectItem)=> x.label;

  selectFunction (event:SelectItem ) {
    this.handleChangeFunction.emit(event && event.value);
  }

}
