import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationClass, PaginationModel } from '../../models/pagination.model';
import { SelectItem } from '../../models/select-item.model';
import { UserModel } from '../../models/user.model';

enum ModalViewEnum {
  isTable = 0,
  isOrganigram = 1
}

@Component({
  selector: 'erp-shared-attach-user-modal',
  templateUrl: './attach-user-modal.component.html',
  styleUrls: ['./attach-user-modal.component.scss']
})
export class AttachUserModalComponent {
  @Input() departments: SelectItem[] = [];
  @Input() roles: SelectItem[] = [];
  @Input() userStatuses: SelectItem[] = [];
  @Input() functions: SelectItem[] = [];
  paginatedAttachedIds: boolean = false;
  pagination: PaginationModel = new PaginationClass();
  isLoading = true;
  view: ModalViewEnum = ModalViewEnum.isTable;
  modalViewEnum = ModalViewEnum;
  @Input() exceptUserIds: number[] = [];
  @Input() attachedUsers: number[] = [];

  @Input() inputType: 'checkbox' | 'radio';

  @Input() eventId: number;
  @Input() positionId: number;
  @Input() testTemplateId: number;

  constructor(private readonly activeModal: NgbActiveModal) {}

  public dismiss(): void {
    this.activeModal.dismiss();
  }

  public confirm(): void {
    this.activeModal.close({
      selectedUsers: this.inputType === 'checkbox' ? [...this.attachedUsers] : [...this.attachedUsers.filter(el => !!el)],
    });
  }

  handleChangeAttachedUsers(data: { attachedUsers: number[], checked: boolean }): void {
    if (data.checked) {
      this.attachedUsers =  [...data.attachedUsers];
    } else {
      data.attachedUsers.forEach(el => {
        this.attachedUsers.splice(this.attachedUsers.indexOf(el), 1);
      })
    }
  }
}
