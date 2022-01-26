import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { VacationConfigurationService } from '../../../utils/services/vacation-configuration.service';
import { VacationConfigurationModel } from '../../../utils/models/vacation-configuration.model';
import { NotificationUtil } from '../../../utils/util/notification.util';

@Component({
  selector: 'app-configure-vacation',
  templateUrl: './configure-vacation.component.html',
  styleUrls: ['./configure-vacation.component.scss']
})
export class ConfigureVacationComponent implements OnInit {

  vacationForm: FormGroup;
  isLoading: boolean = true;
  originalConfiguration: VacationConfigurationModel;
  constructor(private fb: FormBuilder,
              private vacationConfigurationService: VacationConfigurationService,
              private notificationService: NotificationsService) { }

  ngOnInit(): void {
    this.retrieveConfigurations();
  }

  initForm(data: VacationConfigurationModel): void {
    this.vacationForm = this.fb.group({
      paidLeaveDays: this.fb.control(data.paidLeaveDays, [Validators.required]),
      nonPaidLeaveDays: this.fb.control(data.nonPaidLeaveDays, [Validators.required]),
      studyLeaveDays: this.fb.control(data.studyLeaveDays, [Validators.required]),
      deathLeaveDays: this.fb.control(data.deathLeaveDays, [Validators.required]),
      childCareLeaveDays: this.fb.control(data.childCareLeaveDays, [Validators.required]),
      childBirthLeaveDays: this.fb.control(data.childBirthLeaveDays, [Validators.required]),
      marriageLeaveDays: this.fb.control(data.marriageLeaveDays, [Validators.required]),
      paternalistLeaveDays: this.fb.control(data.paternalistLeaveDays, [Validators.required]),
      includeOffDays: this.fb.control(data.includeOffDays, []),
      includeHolidayDays: this.fb.control(data.includeHolidayDays, []),
      mondayIsWorkDay: this.fb.control(data.mondayIsWorkDay, []),
      tuesdayIsWorkDay: this.fb.control(data.tuesdayIsWorkDay, []),
      wednesdayIsWorkDay: this.fb.control(data.wednesdayIsWorkDay, []),
      thursdayIsWorkDay: this.fb.control(data.thursdayIsWorkDay, []),
      fridayIsWorkDay: this.fb.control(data.fridayIsWorkDay, []),
      saturdayIsWorkDay: this.fb.control(data.saturdayIsWorkDay, []),
      sundayIsWorkDay: this.fb.control(data.sundayIsWorkDay, [])
    });
  }

  submit(): void {
    this.isLoading = true;
    this.vacationConfigurationService.add(this.vacationForm.value).subscribe(response => {
      this.notificationService.success('Success', 'Configurations saved!', NotificationUtil.getDefaultConfig());
      this.retrieveConfigurations();
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.error('Error', 'Failed saving configuration!', NotificationUtil.getDefaultConfig());
        return;
      }

      this.notificationService.error('Error', 'Internal server error!', NotificationUtil.getDefaultConfig());
    });
  }

  parseData(data: VacationConfigurationModel): VacationConfigurationModel {
    return {
      ...data,
      paidLeaveDays: +data.paidLeaveDays,
      nonPaidLeaveDays: +data.nonPaidLeaveDays,
      studyLeaveDays: +data.studyLeaveDays,
      deathLeaveDays: +data.deathLeaveDays,
      childCareLeaveDays: +data.childCareLeaveDays,
      childBirthLeaveDays: +data.childBirthLeaveDays,
      marriageLeaveDays: +data.marriageLeaveDays,
      paternalistLeaveDays: +data.paternalistLeaveDays
    }
  }

  retrieveConfigurations(): void {
    this.vacationConfigurationService.get().subscribe(response => {
      this.originalConfiguration = {...response.data};
      this.initForm(response.data);
      this.isLoading = false;
    });
  }

  isSameConfiguration(): boolean {
    const configuration = this.parseData(this.vacationForm.value);

    for(let item in configuration) {
      if (configuration[item] !== this.originalConfiguration[item]) {
        return false;
      }
    }
    return true;
  }

  resetConfiguration(): void {
    this.initForm(this.originalConfiguration);
  }
}
