import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ReferenceService } from '../../services/reference.service';
import { UserProfileService } from '../../services/user-profile.service';
import { EnterSubmitListener } from '../../util/submit.util';

@Component({
  selector: 'app-add-access-modal',
  templateUrl: './add-access-modal.component.html',
  styleUrls: ['./add-access-modal.component.scss']
})
export class AddAccessModalComponent extends EnterSubmitListener implements OnInit {

  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  @Input() contractorId: number;

  accessForm: FormGroup;
  roleModules: any[] = [];
  isLoading: boolean = true;
  contractorEmails: any[] = [];
  contractorEmail: string = null;

  constructor(private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private userProfileService: UserProfileService,
    private referenceService: ReferenceService) {
    super();
    this.callback = this.close;
  }

  ngOnInit(): void {
    this.initForm();
    this.retrieveModules();
    this.retriveEmails();
  }

  retrieveModules(): void {
    this.userProfileService.listModules().subscribe(response => {
      this.roleModules = response.data;
      this.isLoading = false;
    }, () => {
      this.isLoading = false;
    });
  }

  initForm(): void {
    this.accessForm = this.fb.group({
      email: this.fb.control(null, [Validators.required, Validators.email]),
      moduleRoles: this.fb.control(null, [Validators.required])
    });
  }

  isChecked(value): boolean {
    const moduleRoles = this.accessForm.get('moduleRoles').value;
    if (!moduleRoles || !moduleRoles.length) return false;
    return moduleRoles.some(el => +el === +value);
  }

  retriveEmails(): void {
    this.referenceService.listContractorEmails(this.contractorId).subscribe(response => {
      this.contractorEmails = response.data;
      this.isLoading = false;
    }, () => {
      this.isLoading = false; 
    });
  }

  handleChange(event, id: number): void {
    if (!event.target.value) return;

    const selectedRoles = this.accessForm.get('moduleRoles').value && [...this.accessForm.get('moduleRoles').value];
    const requiredModule = this.roleModules.find(el => el.id === id);

    if (selectedRoles && selectedRoles.length) {
      requiredModule.roles.map(el => {
        if (selectedRoles.indexOf(+el.value) > -1) {
          selectedRoles.splice(selectedRoles.indexOf(+el.value), 1);
        }
      });
    }

    if (event.target.checked && selectedRoles && !selectedRoles.includes(+event.target.value)) {
      this.accessForm.get('moduleRoles').patchValue([...selectedRoles, +event.target.value]);
      this.accessForm.get('moduleRoles').updateValueAndValidity();
      return;
    }
    this.accessForm.get('moduleRoles').patchValue([+event.target.value]);
  }

  handleEmailChange(event, label: string): void {
    if (!event.target.value) return;
    this.contractorEmail = label
  }

  clearInput() {
    this.contractorEmail = null;
  }

  close(): void {
    this.activeModal.close(this.accessForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
  }

}
