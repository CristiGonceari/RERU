import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { SearchDepartmentComponent } from '../../../utils/components/search-department/search-department.component';
import { SearchEmployeeFunctionComponent } from '../../../utils/components/search-employee-function/search-employee-function.component';
import { SearchRoleComponent } from '../../../utils/components/search-role/search-role.component';

@Component({
  selector: 'app-evaluations-list-table',
  templateUrl: './evaluations-list-table.component.html',
  styleUrls: ['./evaluations-list-table.component.scss']
})
export class EvaluationsListTableComponent implements OnInit {
  title: string;

  showFilter: boolean = true;

  constructor(private cd: ChangeDetectorRef,) { }

  ngOnInit(): void {
  }

  getTitle(): string {
    this.title = document.getElementById('title').innerHTML;
    return this.title
  }

  clearFields() {
    this.showFilter=false;
    this.cd.detectChanges();
    this.showFilter=true;
  }
}
