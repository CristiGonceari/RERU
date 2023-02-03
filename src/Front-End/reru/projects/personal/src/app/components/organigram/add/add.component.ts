import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { ApiResponse } from '../../../utils/models/api-response.model';
import { SelectItem } from '../../../utils/models/select-item.model';
import { OrganigramService } from '../../../utils/services/organigram.service';
import { ReferenceService } from '../../../utils/services/reference.service';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { EnterSubmitListener } from '../../../utils/util/submit.util';

import { Observable, OperatorFunction, Subject, merge } from 'rxjs';
import { debounceTime, map, distinctUntilChanged, filter } from 'rxjs/operators';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { saveAs } from 'file-saver';
import { I18nService } from '../../../utils/services/i18n.service';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends EnterSubmitListener implements OnInit {

  @ViewChild('instance', { static: false }) instance: NgbTypeahead;

  uploadForm: FormGroup;
  organigramForm: FormGroup;

  isLoading: boolean;
  heads: any[] = [];
  departments: SelectItem[] = [];
  roles: SelectItem[] = [];
  selectedItem: SelectItem;

  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  notification = {
    success: 'Success',
    error: 'Error',
    successAdd: 'Organigram has been successfully added',
    errorAdd: 'Organigram was not added successfully',
    successAddChart: 'Organigram item has been successfully added',
    errorAddChart: 'Organigram item was not been added',
    successEdit: 'Organigram has been edited successfully',
    errorEdit: 'Organigram was not edited successfully',
    successDelete: 'Organigram was deleted',
    errorDelete: 'Organigram was not deleted',
    successDeleteChart: 'Organigram item was deleted successfully',
    errorDeleteChart: 'This organizational item cannot be deleted because it has sub-item/s',
  };

  title: string;
  description: string;

  constructor(private fb: FormBuilder,
    private organigramService: OrganigramService,
    private route: ActivatedRoute,
    private router: Router,
    private ngZone: NgZone,
    private notificationService: NotificationsService,
    private referenceService: ReferenceService,
    public translate: I18nService) {
    super();
  }

  ngOnInit(): void {
    this.retrieveDropdowns();
    this.initForm();
    this.subscribeForTypeChanges();
    this.translateData();

    this.subscribeForTranslateChanges();
  }

  retrieveDropdowns(): void {
    forkJoin([
      this.referenceService.listDepartments(),
      this.referenceService.listOrganizationRoles()
    ]).subscribe(([departments, roles]) => {
      this.departments = departments.data;
      this.roles = roles.data;
      this.heads = [...departments.data];
    });
  }

  translateData(): void {
    forkJoin([
      this.translate.get('notification.success'),
      this.translate.get('notification.error'),
      this.translate.get('organigram.succes-add-organigram'),
      this.translate.get('organigram.error-add-organigram'),
      this.translate.get('organigram.succes-add-chart-organigram'),
      this.translate.get('organigram.error-add-chart-organigram'),
      this.translate.get('organigram.succes-edit-organigram'),
      this.translate.get('organigram.error-edit-organigram'),
      this.translate.get('organigram.succes-delete-organigram'),
      this.translate.get('organigram.error-delete-organigram'),
      this.translate.get('organigram.succes-delete-chart-organigram'),
      this.translate.get('organigram.error-delete-chart-organigram')

    ]).subscribe(([success, error, successAdd, errorAdd, successAddChart, errorAddChart, successEdit, errorEdit, successDelete, errorDelete, successDeleteChart, errorDeleteChart]) => {
      this.notification.success = success;
      this.notification.error = error;
      this.notification.successAdd = successAdd;
      this.notification.errorAdd = errorAdd;
      this.notification.successAddChart = successAddChart;
      this.notification.errorAddChart = errorAddChart;
      this.notification.successEdit = successEdit;
      this.notification.errorEdit = errorEdit;
      this.notification.successDelete = successDelete;
      this.notification.errorDelete = errorDelete;
      this.notification.successDeleteChart = successDeleteChart;
      this.notification.errorDeleteChart = errorDeleteChart;
    });
  }

  subscribeForTranslateChanges(): void {
    this.translate.change.subscribe(() => this.translateData());
  }

  submit(): void {
    this.organigramService.add(this.parseOrganizationalChart(this.organigramForm.value)).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.organigramForm.get('organizationalChartId').patchValue(response.data);
        this.head(this.organigramForm.value);
        return;
      }
      this.notificationService.success(this.notification.success, this.notification.successAdd, NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      forkJoin([
        this.translate.get('modal.error'),
        this.translate.get('organigram.error-add-organigram'),
      ]).subscribe(([title, description1]) => {
        this.title = title;
        this.description = description1;
      });
      if (error.status === 400) {
        this.notificationService.warn(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());
    });
  }

  parseOrganizationalChart(data) {
    return {
      name: data.name,
      fromDate: data.fromDate,
    }
  }

  head(data): void {
    if (data.createType == 1) {
      this.organigramService.head(this.parseHead(data)).subscribe(response => {
        this.ngZone.run(() => this.router.navigate(['../', this.organigramForm.get('organizationalChartId').value], { relativeTo: this.route }));
        this.notificationService.success(this.notification.success, this.notification.successAdd, NotificationUtil.getDefaultMidConfig());
      }, (error) => {
        forkJoin([
          this.translate.get('modal.error'),
          this.translate.get('organigram.error-add-organigram'),
        ]).subscribe(([title, description1]) => {
          this.title = title;
          this.description = description1;
        });
        if (error.status === 400) {
          this.notificationService.success(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());
          return;
        }
          this.notificationService.success(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());
      });
    } else if (data.createType == 2) {
      const form = new FormData();
      form.append('File', this.uploadForm.value.file);

      this.organigramService.excelImport(form, this.organigramForm.value.organizationalChartId).subscribe(res => {

        const fileName = res.headers.get('Content-Disposition').split("filename=")[1].split(';')[0]
        const blob = new Blob([res.body], { type: res.body.type });
        const file = new File([blob], fileName, { type: res.body.type });
        saveAs(file);

        if (fileName.slice(-6).includes("Succes")) {
         
          this.notificationService.success(this.notification.success, this.notification.successAdd, NotificationUtil.getDefaultMidConfig());
          this.ngZone.run(() => this.router.navigate(['../', this.organigramForm.get('organizationalChartId').value], { relativeTo: this.route }));

        } else if (fileName.slice(-5).includes("Error")) {
          
          this.notificationService.error(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());
          this.ngZone.run(() => this.router.navigate(['../'], { relativeTo: this.route }));
        }
      }, error => {
        forkJoin([
          this.translate.get('modal.error'),
          this.translate.get('organigram.error-add-organigram'),
        ]).subscribe(([title, description1]) => {
          this.title = title;
          this.description = description1;
        });
        if (error.status === 400) {
          this.notificationService.success(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());

          return;
        }
        this.notificationService.success(this.notification.error, this.notification.errorAdd, NotificationUtil.getDefaultMidConfig());
      });
    }
  }

  parseHeadFile(file, id) {
    return {
      file: file,
      organizationalChartId: +id
    }
  }
  parseHead(data) {
    return {
      headId: +data.headId,
      type: +data.type,
      organizationalChartId: +data.organizationalChartId
    }
  }

  initForm(): void {
    this.organigramForm = this.fb.group({
      name: this.fb.control(null, [Validators.required]),
      type: this.fb.control('1', [Validators.required]),
      createType: this.fb.control(null, [Validators.required]),
      headId: this.fb.control(null, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      organizationalChartId: this.fb.control(null, []),
    });

    this.uploadForm = this.fb.group({
      file: this.fb.control(null, [Validators.required])
    });
  }

  subscribeForTypeChanges(): void {
    this.organigramForm.get('type').valueChanges.subscribe(value => {
      this.organigramForm.get('headId').patchValue(null);
      if (value === '1') {
        this.heads = [...this.departments];
      }

      if (value === '2') {
        this.heads = [...this.roles];
      }
    });
  }

  searchHead = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(200), distinctUntilChanged());
    const clicksWithClosedPopup$ = this.click$.pipe(filter(() => !this.instance.isPopupOpen()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map(term => (term === '' ? this.heads
        : this.heads.filter(v => v.label.toLowerCase().indexOf(term.toLowerCase()) > -1)))
    );
  }
  formatter = (x: SelectItem) => x.label;

  selectHead(event: SelectItem) {
    if (event)
      this.organigramForm.get("headId").patchValue(event.value);
  }

  setFile(event): void {
    const file = event;
    if (file.size === 0) {
      this.uploadForm.get('file').setErrors({ fileEmpty: true });
      return;
    }
    this.uploadForm.get('file').setErrors(null);
    this.uploadForm.get('file').patchValue(file);
  }

  validateButton(value) {
    if (value == 1) {

      return !this.organigramForm.valid;
    }
    else if (value == 2) {

      return (this.organigramForm.value.name == null ||
        this.organigramForm.value.fromDate == null ||
        this.uploadForm.value.file == null) ? true : false
    }

    return true;
  }
}
