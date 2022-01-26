import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { Contractor } from '../../models/contractor.model';
import { SelectItem } from '../../models/select-item.model';
import { ReferenceService } from '../../services/reference.service';

@Component({
  selector: 'app-add-old-position-modal',
  templateUrl: './add-old-position-modal.component.html',
  styleUrls: ['./add-old-position-modal.component.scss']
})
export class AddOldPositionModalComponent implements OnInit {
  @Input() contractor: Contractor;
  currentPositionForm: FormGroup;
  departments: SelectItem[] = [];
  roles: SelectItem[] = [];
  isLoading: boolean = true;
  constructor(private activeModal: NgbActiveModal,
              private reference: ReferenceService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initDropdownData();
    this.initForm();
  }

  initForm(): void {
    this.currentPositionForm = this.fb.group({
      contractorId: this.fb.control(this.contractor.id, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      toDate: this.fb.control(null, [Validators.required]),
      departmentId: this.fb.control(null, [Validators.required]),
      organizationRoleId: this.fb.control(null, [Validators.required])
    });
    this.isLoading = false;
  }

  initDropdownData(): void {
    forkJoin([
      this.reference.listDepartments(),
      this.reference.listOrganizationRoles()
    ]).subscribe(([departments, roles]) => {
      this.departments = departments.data;
      this.roles = roles.data;
    });
  }

  close(): void {
    this.activeModal.close(this.parseData(this.currentPositionForm.value));
  }

  parseData(data) {
    return {
      contractorId: data.contractorId,
      fromDate: data.fromDate,
      toDate: data.toDate,
      departmentId: !data.departmentId || data.departmentId === 'null' ? null : +data.departmentId,
      organizationRoleId: !data.organizationRoleId || data.organizationRoleId === 'null' ? null : +data.organizationRoleId
    }
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }
}
