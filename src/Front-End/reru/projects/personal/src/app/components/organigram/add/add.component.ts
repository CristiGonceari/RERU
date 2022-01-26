import { Component, NgZone, OnInit , ViewChild} from '@angular/core';
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

import {Observable, OperatorFunction, Subject, merge} from 'rxjs';
import {debounceTime, map, distinctUntilChanged, filter} from 'rxjs/operators';
import {NgbTypeahead} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends EnterSubmitListener implements OnInit {
  organigramForm: FormGroup;
  isLoading: boolean;
  heads: any[] = [];
  departments: SelectItem[] = [];
  roles: SelectItem[] = [];
  selectedItem:SelectItem;
  @ViewChild('instance', {static: true}) instance: NgbTypeahead;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  constructor(private fb: FormBuilder,
              private organigramService: OrganigramService,
              private route: ActivatedRoute,
              private router: Router,
              private ngZone: NgZone,
              private notificationService: NotificationsService,
              private referenceService: ReferenceService) {
    super();
    this.callback = this.submit;
  }

  ngOnInit(): void {
    this.retrieveDropdowns();
    this.initForm();
    this.subscribeForTypeChanges();
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

  submit(): void {
    this.organigramService.add(this.parseOrganizationalChart(this.organigramForm.value)).subscribe((response: ApiResponse<any>) => {
      if (response.success) {
        this.notificationService.success('Success', 'Organigram updated!', NotificationUtil.getDefaultConfig());
        this.organigramForm.get('organizationalChartId').patchValue(response.data);
        this.head(this.organigramForm.value);
        return;
      }
      this.notificationService.warn('Error', 'An error occured!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Error', 'Validation service error!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
    });
  }

  parseOrganizationalChart(data) {
    return {
      name: data.name,
      fromDate: data.fromDate,
    }
  }

  head(data): void {
    this.organigramService.head(this.parseHead(data)).subscribe(response => {
      this.ngZone.run(() => this.router.navigate(['../', this.organigramForm.get('organizationalChartId').value], { relativeTo: this.route }));
      this.notificationService.success('Success', 'You\'ve added organigram successfully!', NotificationUtil.getDefaultMidConfig());
    }, (error) => {
      if (error.status === 400) {
        this.notificationService.warn('Error', 'Validation service error!', NotificationUtil.getDefaultMidConfig());
        return;
      }
      this.notificationService.error('Server error occured!', null, NotificationUtil.getDefaultMidConfig());
    });
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
      headId: this.fb.control(null, [Validators.required]),
      fromDate: this.fb.control(null, [Validators.required]),
      organizationalChartId: this.fb.control(null, []),
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
  formatter = (x:SelectItem)=>x.label;

  selectHead (event:SelectItem ){
    if (event)
      this.organigramForm.get("headId").patchValue(event.value);
  }
  
}
