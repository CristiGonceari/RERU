import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-reports-filter',
  templateUrl: './reports-filter.component.html',
  styleUrls: ['./reports-filter.component.scss']
})
export class ReportsFilterComponent implements OnInit {

  filterForm: FormGroup;

  @Output() applyFilter: EventEmitter<any> = new EventEmitter<any>();
  @Output() reset: EventEmitter<any> = new EventEmitter<any>();

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.filterForm = this.fb.group({
      type: this.fb.control(0, []),
      name: this.fb.control(null, []),
      contractorName: this.fb.control(null, []),
      contractorLastName: this.fb.control(null, []),
      departmentId: this.fb.control(null, []),
      fromDate: this.fb.control(null, []),
      toDate: this.fb.control(null, [])
    })
  }

  updateDepartmentId(id: number): void {
    this.filterForm.get('departmentId').patchValue(id);
  }

  apply(): void {
    this.applyFilter.emit(this.filterForm.value);
  }

  resetForm(): void {
    this.filterForm.reset();
  }
}
