import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { SelectItem } from 'projects/personal/src/app/utils/models/select-item.model';
import { ReferenceService } from 'projects/personal/src/app/utils/services/reference.service';
import { merge, Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-employee-position',
  templateUrl: './employee-position.component.html',
  styleUrls: ['./employee-position.component.scss']
})
export class EmployeePositionComponent implements OnInit {
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  selectedRole: SelectItem;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();
  roles: SelectItem[];
  @Output() handleChangeRole: EventEmitter<string> = new EventEmitter<string>();
  constructor(private reference: ReferenceService) { }

  ngOnInit(): void {
    this.retrieveDropdownData();
  }

  retrieveDropdownData(): void {
    this.reference.listOrganizationRoles().subscribe(roles => {
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

  formatter = (x: SelectItem)=> x.label;

  selectRole (event:SelectItem ) {
    this.handleChangeRole.emit(event && event.value);
  }

}
