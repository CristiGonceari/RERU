import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-import-employee-function-modal',
  templateUrl: './import-employee-function-modal.component.html',
  styleUrls: ['./import-employee-function-modal.component.scss']
})
export class ImportEmployeeFunctionModalComponent implements OnInit {

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
    const file = event.target.files[0];
    if (file.size === 0) {
      this.uploadForm.get('file').setErrors({fileEmpty: true});
      return;
    }
    this.uploadForm.get('file').setErrors(null);
    this.uploadForm.get('file').patchValue(file);
  }
}
