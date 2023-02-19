import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { ContactModel } from '../../models/contact.model';
import { SelectItem } from '../../models/select-item.model';
import { ContactService } from '../../services/contact.service';
import { I18nService } from '../../services/i18n.service';
import { ReferenceService } from '../../services/reference.service';
import { NotificationUtil } from '../../util/notification.util';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-edit-contact-modal',
  templateUrl: './edit-contact-modal.component.html',
  styleUrls: ['./edit-contact-modal.component.scss']
})
export class EditContactModalComponent extends EnterSubmitListener implements OnInit {
  @Input() id: number;
  contactForm: FormGroup;
  types: SelectItem[] = [];
  isLoading: boolean = true;
  notification = {
    warning: 'Warning',
    error: 'Error',
    success: 'Success',
    contactUpdate: 'Contact has been successfully updated!',
    validationFail: 'Validation failed!',
    serverError: 'Server error occured!'
  }
  
  constructor(private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private reference: ReferenceService,
    private contactService: ContactService,
    private notificationService: NotificationsService,
    public translate: I18nService) {
      super();
      this.callback = this.submit;
     }

  ngOnInit(): void {
    this.initDropdownData();
    this.retrieveContact();

    this.translateData();
    this.subscribeForLanguageChange();
  }

  retrieveContact(): void {
    this.contactService.retrieveContact(this.id).subscribe(response => {
      if (response.success) {
        this.initForm(response.data);
        this.isLoading = false;
      }
    })
  }

  initForm(contact: ContactModel): void {
    this.contactForm = this.fb.group({
      id: this.fb.control(contact.id, []),
      type: this.fb.control(contact.type, [Validators.required]),
      value: this.fb.control(contact.value, [Validators.required]),
      contractorId: this.fb.control(contact.contractorId, [])
    });
  }

  initDropdownData(): void {
    this.reference.listContactTypes().subscribe(response => {
      this.types = response.data || [];
    });
  }

  translateData(): void {
		forkJoin([
      this.translate.get('notification.title.warning'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.title.success'),
      this.translate.get('notification.body.contact-update'),
      this.translate.get('notification.body.validation-fail'),
      this.translate.get('notification.body.server-error'),
		]).subscribe(
			([ warning, error, success, contactUpdate, validationFail, serverError]) => {
        this.notification.warning = warning;
        this.notification.error = error;
        this.notification.success = success;
        this.notification.contactUpdate = contactUpdate;
        this.notification.validationFail = validationFail;
        this.notification.serverError = serverError;
			}
		);
	}

  subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  submit(): void {
    this.isLoading = true;
    this.contactService.updateContact(this.parseData(this.contactForm.value)).subscribe(response => {
      if (response.success) {
        this.notificationService.success(this.notification.success, this.notification.contactUpdate, NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
        this.close();
      }
    }, error => {
      this.isLoading = false;
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    });
  }

  parseData(data) {
    return {
      ...data,
      type: !data.type || data.type === 'null' ? null : +data.type
    }
  }

  close(): void {
    this.activeModal.close();
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
