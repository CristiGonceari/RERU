import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { Contractor } from 'projects/personal/src/app/utils/models/contractor.model';
import { ContractorService } from 'projects/personal/src/app/utils/services/contractor.service';
import { NotificationUtil } from 'projects/personal/src/app/utils/util/notification.util';
import { PermissionModel } from '../../../../utils/models/permission.model';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.scss']
})
export class PermissionsComponent implements OnInit {
  @Input() contractor: Contractor;
  permissionForm: FormGroup;
  isLoading: boolean = true;
  originalPermissions: PermissionModel;
  constructor(private fb: FormBuilder,
              private notificationService: NotificationsService,
              private contractorService: ContractorService) { }

  ngOnInit(): void {
    this.retrievePermissions();
  }

  retrievePermissions(): void {
    this.contractorService.getPermissions(this.contractor.id).subscribe(response => {
      this.initForm(response.data);
      this.originalPermissions = {...response.data};
    });
  }

  initForm(data: PermissionModel = <PermissionModel>{}): void {
    this.permissionForm = this.fb.group({
      contractorId: this.fb.control(this.contractor.id),
      getGeneralData: this.fb.control(!!data.getGeneralData, [Validators.required]),
      getBulletinData: this.fb.control(!!data.getBulletinData, [Validators.required]),
      getStudiesData: this.fb.control(!!data.getStudiesData, [Validators.required]),
      getCimData: this.fb.control(!!data.getCimData, [Validators.required]),
      getPositionsData: this.fb.control(!!data.getPositionsData, [Validators.required]),
      getRanksData: this.fb.control(!!data.getRanksData, [Validators.required]),
      getFamilyComponentData: this.fb.control(!!data.getFamilyComponentData, [Validators.required]),
      getTimeSheetTableData: this.fb.control(!!data.getTimeSheetTableData, [Validators.required]),
      getDocumentsDataIdentity: this.fb.control(!!data.getDocumentsDataIdentity, [Validators.required]),
      getDocumentsDataOrders: this.fb.control(!!data.getDocumentsDataOrders, [Validators.required]),
      getDocumentsDataCim: this.fb.control(!!data.getDocumentsDataCim, [Validators.required]),
      getDocumentsDataRequest: this.fb.control(!!data.getDocumentsDataRequest, [Validators.required]),
      getDocumentsDataVacation: this.fb.control(!!data.getDocumentsDataVacation, [Validators.required]),
    });
    this.isLoading = false;
  }

  submit(): void {
    this.isLoading = true;
    this.contractorService.updatePermissions(this.permissionForm.value).subscribe(() => {
      this.retrievePermissions();
      this.notificationService.success('Success', 'Saved!', NotificationUtil.getDefaultConfig());
    });
  }

  resetPermissions(): void {
    this.isLoading = true;
    this.initForm(this.originalPermissions);
  }

}
