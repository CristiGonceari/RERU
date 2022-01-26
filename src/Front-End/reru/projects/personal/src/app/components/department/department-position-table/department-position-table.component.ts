import { Component, Input, OnInit } from '@angular/core';
import { DepartmentRoleService } from '../../../utils/services/department-role.service';

@Component({
  selector: 'app-department-position-table',
  templateUrl: './department-position-table.component.html',
  styleUrls: ['./department-position-table.component.scss']
})
export class DepartmentPositionTableComponent implements OnInit {
  @Input() id: string;
  departments: any[] = [];
  constructor(private departmenRoleService: DepartmentRoleService) {}

  ngOnInit(): void {
    this.list(this.id);
  }

  list(id): void {
    this.departmenRoleService.list({ departmentId: id }).subscribe(response => {
      const deps = response.data.departments ? [...response.data.departments] : [];
      const roles = response.data.roles ? [...response.data.roles] : [];
      this.departments = [...deps, ...roles];
    });
  }
}
