import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-request-to-employ-data-form',
  templateUrl: './request-to-employ-data-form.component.html',
  styleUrls: ['./request-to-employ-data-form.component.scss']
})
export class RequestToEmployDataFormComponent implements OnInit {
  requestToEmployForm: FormGroup;
  isLoading: boolean;
  files: File[] = [];

  @Input() ContractorId: number;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
  }
   
  initForm(): void {
    if(this.ContractorId)
    {
     this.requestToEmployForm = this.fb.group({
       file: this.fb.control(null, [Validators.required])
     });
    }
    else 
    {
      this.requestToEmployForm = this.fb.group({
        file: this.fb.control(null, [Validators.required]),
      });}
  }

  onSelect(event): void {
    this.files[0] = event.addedFiles[0];
    this.requestToEmployForm.get('file').patchValue(event.addedFiles[0]);
	}

	onRemove(event): void {
		this.files.splice(this.files.indexOf(event), 1);
	}
}
