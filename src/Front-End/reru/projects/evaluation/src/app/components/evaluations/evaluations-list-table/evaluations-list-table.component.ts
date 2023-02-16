import { Component, OnInit, ViewChild } from '@angular/core';
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

  @ViewChild('testName') testName: any;
  @ViewChild('testEvent') testEvent: any;
  @ViewChild('testLocation') testLocation: any;
  @ViewChild('userName') userName: any;
  @ViewChild('evaluatorName') evaluatorName: any;
  @ViewChild('selectedResult') selectedResult: any;
  @ViewChild(SearchDepartmentComponent) departmentId: SearchDepartmentComponent;
  @ViewChild(SearchRoleComponent) roleId: SearchRoleComponent;
  @ViewChild(SearchEmployeeFunctionComponent) functionId: SearchEmployeeFunctionComponent;

  constructor() { }

  ngOnInit(): void {
  }

  getTitle(): string {
    this.title = document.getElementById('title').innerHTML;
    return this.title
  }

  clearFields() {
    this.testName.clearSearch();
    this.userName.clearSearch();
    this.evaluatorName.clearSearch();
    this.testEvent.clearSearch();
    this.testLocation.clearSearch();
    this.selectedResult.getTestResults();
    this.departmentId.department = '';
    this.roleId.role = '';
    this.functionId.function = '';
  }
}
