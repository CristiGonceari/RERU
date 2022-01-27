import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { forkJoin } from 'rxjs';
import { Contractor } from '../../models/contractor.model';
import { SelectItem } from '../../models/select-item.model';
import { PositionService } from '../../services/position.service';
import { ReferenceService } from '../../services/reference.service';
import { EnterSubmitListener } from '../../util/submit.util';

interface ChangeCurrentPosition {
  contractorId: number;
  fromDate: string;
  departmentId: number;
  organizationRoleId: number;
}

@Component({
  selector: 'app-change-current-position-modal',
  templateUrl: './change-current-position-modal.component.html',
  styleUrls: ['./change-current-position-modal.component.scss']
})
export class ChangeCurrentPositionModalComponent extends EnterSubmitListener implements OnInit {
  @Input() contractor: Contractor;
  currentPositionForm: FormGroup;
  departments: SelectItem[] = [];
  roles: SelectItem[] = [];
  isLoading: boolean = true;
  constructor(private activeModal: NgbActiveModal,
    private reference: ReferenceService,
    private positionService: PositionService,
    private fb: FormBuilder) {
      super();
      this.callback = this.close;
     }

  ngOnInit(): void {
    this.initDropdownData();
    this.retrieveUserData();
  }

  initForm(data: ChangeCurrentPosition): void {
    this.currentPositionForm = this.fb.group({
      contractorId: this.fb.control(this.contractor.id, [Validators.required]),
      fromDate: this.fb.control(data.fromDate, [Validators.required]),
      departmentId: this.fb.control(data.departmentId, [Validators.required]),
      organizationRoleId: this.fb.control(data.organizationRoleId, [Validators.required])
    });
    this.isLoading = false;
  }

  retrieveUserData(): void {
    this.positionService.retrieveCurrentPosition(this.contractor.id).subscribe(response => {
      if (response.success) {
        this.initForm(response.data);
      }
    });
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

  parseData(data): ChangeCurrentPosition {
    return {
      contractorId: data.contractorId,
      fromDate: data.fromDate,
      departmentId: !data.departmentId || data.departmentId === 'null' ? null : +data.departmentId,
      organizationRoleId: !data.organizationRoleId || data.organizationRoleId === 'null' ? null : +data.organizationRoleId
    }
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
