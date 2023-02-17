import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-import-users-modal',
  templateUrl: './import-users-modal.component.html',
  styleUrls: ['./import-users-modal.component.scss']
})
export class ImportUsersModalComponent implements OnInit {
  uploadForm: FormGroup;
  isLoading: boolean = true;
  
  constructor(private activeModal: NgbActiveModal,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
    this.isLoading = false;
  }

  initForm(): void {
    this.uploadForm = this.fb.group({
      file: this.fb.control(null, [Validators.required])
    })
  }

  close(): void {
    this.activeModal.close(this.uploadForm.value);
  }

  dismiss(): void {
    this.activeModal.dismiss();
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
}
