import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { forkJoin } from 'rxjs';
import { ContactModel } from '../../models/contact.model';
import { Contractor } from '../../models/contractor.model';
import { ContactService } from '../../services/contact.service';
import { I18nService } from '../../services/i18n.service';
import { NotificationUtil } from '../../util/notification.util';
import { EditContactModalComponent } from '../../modals/edit-contact-modal/edit-contact-modal.component';
import { ConfirmationContactDeleteModalComponent } from '../../modals/confirmation-contact-delete-modal/confirmation-contact-delete-modal.component';

@Component({
  selector: 'app-view-contact',
  templateUrl: './view-contact.component.html',
  styleUrls: ['./view-contact.component.scss']
})
export class ViewContactComponent implements OnInit{
  @Input() contact: ContactModel;
  @Input() contractor: Contractor;
  @Input() isView: boolean;
  @Output() update: EventEmitter<void> = new EventEmitter<void>();
  notification = {
    warning: 'Warning',
    error: 'Error',
    success: 'Success',
    contactDelete: 'Contact has been successfully deleted!',
    validationFail: 'Validation failed!',
    serverError: 'Server error occured!'
  }

  constructor(private modalService: NgbModal,
              private contactService: ContactService,
              private notificationService: NotificationsService,
              public translate: I18nService) { }

  ngOnInit(): void {
    this.translateData();
    this.subscribeForLanguageChange();
  }

  openContactEditModal(): void {
    const modalRef = this.modalService.open(EditContactModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.id = this.contact.id;
    modalRef.result.then(() => this.update.emit(), () => { });
  }

  openConfirmationDeleteModal(): void {
    const modalRef = this.modalService.open(ConfirmationContactDeleteModalComponent, { centered: true, size: 'lg' });
    modalRef.componentInstance.contact = this.contact;
    modalRef.componentInstance.contractor = this.contractor;
    modalRef.result.then(() => this.delete(), () => { });
  }

  translateData(): void {
		forkJoin([
      this.translate.get('notification.title.warning'),
      this.translate.get('notification.title.error'),
      this.translate.get('notification.title.success'),
      this.translate.get('notification.body.success.contact-delete'),
      this.translate.get('notification.body.success.validation-fail'),
      this.translate.get('notification.body.success.server-error'),
		]).subscribe(
			([ warning, error, success, contactDelete, validationFail, serverError]) => {
        this.notification.warning = warning;
        this.notification.error = error;
        this.notification.success = success;
        this.notification.contactDelete = contactDelete;
        this.notification.validationFail = validationFail;
        this.notification.serverError = serverError;
			}
		);
	}

  subscribeForLanguageChange(): void {
		this.translate.change.subscribe(() => this.translateData());
	}

  delete(): void {
    this.contactService.delete(this.contact.id).subscribe(response => {
      if (response.success) {
        this.notificationService.success(this.notification.success, this.notification.contactDelete, NotificationUtil.getDefaultMidConfig());
        this.update.emit();
      }
    }, error => {
      if (error.status === 400) {
        this.notificationService.warn(this.notification.warning, this.notification.validationFail, NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error(this.notification.error, this.notification.serverError, NotificationUtil.getDefaultMidConfig());
    })
  }
}
