import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ContactModel } from '../../models/contact.model';
import { Contractor } from '../../models/contractor.model';
import { ContactService } from '../../services/contact.service';
import { NotificationUtil } from '../../util/notification.util';

@Component({
  selector: 'app-view-contact',
  templateUrl: './view-contact.component.html',
  styleUrls: ['./view-contact.component.scss']
})
export class ViewContactComponent{
  @Input() contact: ContactModel;
  @Input() contractor: Contractor;
  @Input() isView: boolean;
  @Output() update: EventEmitter<void> = new EventEmitter<void>();
  constructor(private modalService: NgbModal,
              private contactService: ContactService,
              private notificationService: NotificationsService) { }

  // openContactEditModal(): void {
  //   const modalRef = this.modalService.open(EditContactModalComponent, { centered: true, size: 'lg' });
  //   modalRef.componentInstance.id = this.contact.id;
  //   modalRef.result.then(() => this.update.emit(), () => { });
  // }

  // openConfirmationDeleteModal(): void {
  //   const modalRef = this.modalService.open(ConfirmationContactDeleteModalComponent, { centered: true, size: 'lg' });
  //   modalRef.componentInstance.contact = this.contact;
  //   modalRef.componentInstance.contractor = this.contractor;
  //   modalRef.result.then(() => this.delete(), () => { });
  // }

  delete(): void {
    this.contactService.delete(this.contact.id).subscribe(response => {
      if (response.success) {
        this.notificationService.success('Success', 'Contact has been successfully deleted!', NotificationUtil.getDefaultMidConfig());
        this.update.emit();
      }
    }, error => {
      if (error.status === 400) {
        this.notificationService.warn('Warning', 'Validation error!', NotificationUtil.getDefaultMidConfig());
        return;
      }

      this.notificationService.error('Error', 'Server error occured', NotificationUtil.getDefaultMidConfig());
    })
  }
}
