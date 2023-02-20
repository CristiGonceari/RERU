import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { SelectItem } from '../../models/select-item.model';
import { ContactService } from '../../services/contact.service';
import { I18nService } from '../../services/i18n.service';
import { ReferenceService } from '../../services/reference.service';
import { NotificationUtil } from '../../util/notification.util';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-contractor-contact-modal',
  templateUrl: './add-contractor-contact-modal.component.html',
  styleUrls: ['./add-contractor-contact-modal.component.scss']
})
export class AddContractorContactModalComponent extends EnterSubmitListener implements OnInit {
  @Input() contractorId: number;
  contactForm: FormGroup;
  isLoading: boolean;
  types: SelectItem[] = [];
  notification = {
    success: 'Success',
    contactAdd: 'Contact has been successfully added!'
  }

  constructor(private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private contactService: ContactService,
    private notificationService: NotificationsService,
    private reference: ReferenceService,
    public translate: I18nService) {
      super();
      this.callback = this.submit;
     }

  ngOnInit(): void {
    this.initDropdownData();
    this.initForm();
    
    this.translateData();
    this.subscribeForLanguageChange();
  }

  initForm(): void {
    this.contactForm = this.fb.group({
      type: this.fb.control(null, [Validators.required]),
      value: this.fb.control(null, [Validators.required, this.isValidEmail.bind(this)]),
      contractorId: this.fb.control(this.contractorId, [])
    });
  }

  isValidEmail(control: AbstractControl):{[key: string]: boolean} | null {
    if (control && this.contactForm && +this.contactForm.get('type').value === 2 && Validators.email(control)) {
        return { email: true };
    }

    return null;
  }

  initDropdownData(): void {
    this.reference.listContactTypes().subscribe(response => {
      this.types = response.data || [];
    });
  }

  translateData(): void {
		forkJoin([
      this.translate.get('notification.title.success'),
      this.translate.get('notification.body.success.contact-add'),
		]).subscribe(
			([ success, contactAdd]) => {
        this.notification.success = success;
        this.notification.contactAdd = contactAdd;
			}
		);
	}

  subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  submit(): void {
    this.isLoading = true;
    this.contactService.createContact(this.parseData(this.contactForm.value)).subscribe(response => {
      if (response.success) {
        this.notificationService.success(this.notification.success, this.notification.contactAdd, NotificationUtil.getDefaultMidConfig());
        this.isLoading = false;
        this.close();
      }
    }, error => {
      this.isLoading = false;
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
