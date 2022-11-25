import { Component, OnInit, ViewChild } from '@angular/core';
import { SearchDepartmentComponent } from '../../../utils/components/search-department/search-department.component';
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

  constructor() { }

  ngOnInit(): void {
  }

  getTitle(): string {
    this.title = document.getElementById('title').innerHTML;
    return this.title
  }

  clearFields() {
    this.testName.value = '';
    this.testEvent.value = '';
    this.testLocation.value = '';
    this.userName.value = '';
    this.evaluatorName.value = '';
    this.selectedResult.getTestResults();
    this.departmentId.department = '';
    this.roleId.role = '';
  }
}
