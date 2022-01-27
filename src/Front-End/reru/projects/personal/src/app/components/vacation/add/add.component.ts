import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { ReferenceService } from '../../../utils/services/reference.service';
import { VacationService } from '../../../utils/services/vacation.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends EnterSubmitListener implements OnInit {
  vacationForm: FormGroup;
  isLoading: boolean;
  types: any[] = [];
  availableDays: number = 0;
  constructor(private fb: FormBuilder,
    private vacationService: VacationService,
    private route: ActivatedRoute,
    private router: Router,
    private ngZone: NgZone,
    private notificationService: NotificationsService,
    private referenceService: ReferenceService) {
      super();
      this.callback = this.submit;
     }

  ngOnInit(): void {
    this.retrieveAvailableDays();
    this.retrieveTypes();
    this.initForm();
  }

  retrieveAvailableDays(): void {
    this.vacationService.retrieveAvailableDays().subscribe(response => {
      this.availableDays = response.data;
    });
  }

  retrieveTypes(): void {
    this.referenceService.listNomenclatureType(6).subscribe(response => {
      this.types = response.data;
    });
  }

  submit(): void {
    this.vacationService.add(this.parseData(this.vacationForm.value)).subscribe((response: ApiResponse<any>) => {
      this.ngZone.run(() => this.router.navigate(['../', response.data], { relativeTo: this.route }));
      this.notificationService.success('Success', 'Vacation created!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Validation error', 'Please fill in vacation name!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Error', 'Server error occured!', NotificationUtil.getDefaultMidConfig());
    });
  }

  uploadDocument(event): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.onload = () => {
        this.vacationForm.get('vacationFile').setValue(event.target.files[0]);
      };
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  parseData(data) {
    const formData = new FormData();
    formData.append('vacationTypeId', data.vacationTypeId);
    formData.append('fromDate', data.fromDate);
    formData.append('toDate', data.toDate);
    formData.append('vacationFile', 'null');
    return formData;
  }

  initForm(): void {
    this.vacationForm = this.fb.group({
      vacationTypeId: this.fb.control(null, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      toDate: this.fb.control(null, [Validators.required]),
      vacationFile: this.fb.control(null, [])
    });
  }

}
