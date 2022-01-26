import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-picture-data-form',
  templateUrl: './picture-data-form.component.html',
  styleUrls: ['./picture-data-form.component.scss']
})
export class PictureDataFormComponent implements OnInit {
  pictureForm: FormGroup;
  photos: File[] = [];
  isLoading: boolean;
  contractorId: number;

  constructor(private route: ActivatedRoute,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
  }
 
  initForm(): void {
    this.pictureForm = this.fb.group({
      photo: this.fb.control(null, [Validators.required])
    });
  }

  onSelectPhoto(event): void {
    this.photos[0] = event.addedFiles[0];
    this.pictureForm.get('photo').patchValue(event.addedFiles[0]);
  }

  onRemovePhoto(event): void {
    this.photos.splice(this.photos.indexOf(event), 1);
  }
}
