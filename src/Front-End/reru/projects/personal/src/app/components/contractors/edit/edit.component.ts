import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { Contractor } from '../../../utils/models/contractor.model';
import { SelectItem } from '../../../utils/models/select-item.model';
import { ContractorService } from '../../../utils/services/contractor.service';
import { ReferenceService } from '../../../utils/services/reference.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends EnterSubmitListener implements OnInit {
  contractorForm: FormGroup;
  departments: SelectItem[] = [];
  roles: SelectItem[] = [];
  isLoading: boolean = true;
  contractor: Contractor;
  constructor(private fb: FormBuilder,
    private reference: ReferenceService,
    private route: ActivatedRoute,
    private contractorService: ContractorService,
    private notificationService: NotificationsService,
    private router: Router,
    private ngZone: NgZone) {
      super();
      this.callback = this.editContractor;
     }

  ngOnInit(): void {
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.getDropdownData();
        this.getUser(+response.id);
      }
    })
  }

  initForm(data: Contractor): void {
    this.contractorForm = this.fb.group({
      id: this.fb.control(data && data.id, [Validators.required]),
      firstName: this.fb.control(data && data.firstName, [Validators.required]),
      lastName: this.fb.control(data && data.lastName, [Validators.required]),
      currentPosition: this.fb.group({
        id: this.fb.control(data && data.positionId, []),
        fromDate: this.fb.control(data && data.fromDate, []),
        departmentId: this.fb.control(data && data.departmentId + '', []),
        organizationRoleId: this.fb.control(data && data.organizationRoleId + '', [])
      })
    });
  }

  getUser(id: number): void {
    this.contractorService.get(id).subscribe((response: ApiResponse<Contractor>) => {
      this.isLoading = false;
      this.contractor = { ...response.data };
      this.initForm(response.data);
    });
  }

  renderText(user): string {
    if (!user) {
      return '';
    }

    return `${user.lastName[0].toUpperCase()}${user.firstName[0].toUpperCase()}`;
  }

  getDropdownData() {
    forkJoin([
      this.reference.listDepartments(),
      this.reference.listOrganizationRoles()
    ]).subscribe(([departments, roles]) => {
      this.departments = departments.data;
      this.roles = roles.data;
    });
  }

  editContractor(): void {
    const contractor = this.parseRequest(this.contractorForm.value);

    this.contractorService.update(contractor).subscribe(response => {
      if (response.success) {
        this.ngZone.run(() => this.router.navigate(['../../', contractor.id], { relativeTo: this.route }));
        this.notificationService.success('Success', 'Contractor updated!', NotificationUtil.getDefaultMidConfig());
      }
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation error', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured', NotificationUtil.getDefaultMidConfig());
    });
  }

  parseRequest(data: Contractor): Contractor {
    return {
      ...data,
      currentPosition: {
        id: data.currentPosition.id ? +data.currentPosition.id : null ,
        fromDate: data.currentPosition.fromDate,
        departmentId: data.currentPosition.departmentId ? +data.currentPosition.departmentId :  null ,
        organizationRoleId: data.currentPosition.organizationRoleId ? +data.currentPosition.organizationRoleId : null
      }
    };
  }

}
