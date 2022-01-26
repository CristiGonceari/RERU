import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { ReferenceService } from '../../../utils/services/reference.service';
import { VacationService } from '../../../utils/services/vacation.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';
import { VacationModel } from '../../../utils/models/vacation.model';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends EnterSubmitListener implements OnInit  {
  vacationForm: FormGroup;
  isLoading: boolean = true;
  types: any[] = [];
  originalFileName: string;
  fileName: string;
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
    this.retrieveTypes();
    this.subscribeForParams();
  }

  subscribeForParams(): void {
    this.route.params.subscribe(response => {
      if (response.id) {
        this.retrieveFileById(+response.id)
        this.retrieveVacation(+response.id);
      }
    })
  }

  retrieveFileById(id: number): void {
    this.vacationService.downloadRequest(id).subscribe(response => {
      this.fileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
      this.originalFileName = response.headers.get('Content-Disposition').split("filename=")[1].split(';')[0];
    });
  }

  retrieveVacation(id: number): void {
    this.vacationService.get(id).subscribe(response => {
      this.initForm(response.data);
      this.isLoading = false;
    });
  }

  retrieveTypes(): void {
    this.referenceService.listNomenclatureType(6).subscribe(response => {
      this.types = response.data;
    });
  }

  submit(): void {
    this.edit();
    // if (this.fileName && this.originalFileName !== this.fileName) {
    //   this.vacationService.updateFile(this.parseFile()).subscribe(() => this.edit());
    // } else {
    //   this.edit();
    // }
  }

  edit(): void {
    this.vacationService.edit(this.parseData(this.vacationForm.value)).subscribe((response: ApiResponse<any>) => {
      this.ngZone.run(() => this.router.navigate(['../../', response.data], { relativeTo: this.route }));
      this.notificationService.success('Success', 'Vacation updated!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Validation error', 'Please fill in vacation name!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
    });
  }

  parseFile(): FormData {
    const formData = new FormData();
    formData.append('vacationId', this.vacationForm.get('id').value);
    formData.append('newFile', this.vacationForm.get('vacationFile').value);

    return formData;
  }

  parseData(data): FormData {
    const formData = new FormData();
    formData.append('id', data.id);
    formData.append('fromDate', data.fromDate);
    formData.append('toDate', data.toDate);
    formData.append('vacationTypeId', data.vacationTypeId && !isNaN(data.vacationTypeId) ? data.vacationTypeId : '1');
    return formData;
  }

  initForm(vacation: VacationModel): void {
    this.vacationForm = this.fb.group({
      id: this.fb.control(vacation.id, [Validators.required]),
      vacationTypeId: this.fb.control(vacation.vacationTypeId, [Validators.required]),
      fromDate: this.fb.control(vacation.fromDate, [Validators.required]),
      toDate: this.fb.control(vacation.toDate, [Validators.required]),
      vacationFile: this.fb.control(vacation.vacationFile, [])
    });
  }

  resetFile(): void {
    this.fileName = '';
  }

  uploadDocument(event): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.onload = () => {
        this.vacationForm.get('vacationFile').setValue(event.target.files[0]);
        this.fileName = event.target.files[0].name;
      };
      reader.readAsDataURL(event.target.files[0]);
    }
  }
}
