import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RequiredDocumentService } from '../../../utils/services/required-document/required-document.service';
import { Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { I18nService } from '../../../utils/services/i18n/i18n.service';
import { NotificationsService } from 'angular2-notifications';
import { NotificationUtil } from '../../../utils/util/notification.util';
import { ReferenceService } from '../../../utils/services/reference/reference.service';


@Component({
  selector: 'app-add-edit-required-document',
  templateUrl: './add-edit-required-document.component.html',
  styleUrls: ['./add-edit-required-document.component.scss']
})
export class AddEditRequiredDocumentComponent implements OnInit {
  isLoading: boolean = true;

  documentForm: FormGroup;

  documentId: number;
  disableBtn: boolean;

  title: string;
  description: string;
  mobileButtonLength: string = "100%";

  constructor(private requiredDocumentService: RequiredDocumentService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private location: Location,
    public translate: I18nService,
    private notificationService: NotificationsService,

  ) { }

  ngOnInit(): void {
    this.documentForm = new FormGroup({
      id: new FormControl(),
      name: new FormControl(),
      mandatory: new FormControl(),
    });

    this.initData();

  }

  initData(): void {
    this.activatedRoute.params.subscribe(response => {
      if (!(response && Object.keys(response).length === 0 && response.constructor === Object)) {
        this.documentId = response.id;
        this.requiredDocumentService.getRequiredDocument(this.documentId).subscribe(res => {
          this.initForm(res.data);
          this.isLoading = false;
        })
      }else{
        this.initForm();
        this.isLoading = false;
      }
    })
  }

  initForm(data?: any): void {
    if (data) {
      this.documentForm = this.formBuilder.group({
        id: this.documentId,
        name: this.formBuilder.control((data.name), [Validators.required]),
        mandatory: this.formBuilder.control((data.mandatory) || null),

      });
    } else {
      this.documentForm = this.formBuilder.group({
        name: this.formBuilder.control("", [Validators.required]),
        mandatory: this.formBuilder.control( null),
      });
    }
    this.isLoading = false;
  }

  addRequiredDocuent() {
    this.disableBtn = true;
    const request = new FormData();

    request.append('Data.Name', this.documentForm.value.name);
    request.append('Data.Mandatory', this.documentForm.value.mandatory);

    this.requiredDocumentService.add(request).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('require-documents.document-added-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => {
      this.disableBtn = false;
    });
  }

  editRequiredDocuent() {
    this.disableBtn = true;
    const request = new FormData();

    request.append('Data.Id', this.documentForm.value.id);
    request.append('Data.Name', this.documentForm.value.name);
    request.append('Data.Mandatory', this.documentForm.value.mandatory);

    this.requiredDocumentService.add(request).subscribe(() => {
      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('require-documents.document-edited-msg'),
      ]).subscribe(([title, description]) => {
        this.title = title;
        this.description = description;
      });
      this.disableBtn = false;
      this.backClicked();
      this.notificationService.success(this.title, this.description, NotificationUtil.getDefaultMidConfig());
    }, () => {
      this.disableBtn = false;
    });
  }

  onSave(): void {
    if (this.documentId) {
      this.editRequiredDocuent();
    } else {
      this.addRequiredDocuent();
    }
  }

  backClicked() {
    this.location.back();
  }

  hasErrors(): boolean {
    return this.documentForm.touched;
  }
}
