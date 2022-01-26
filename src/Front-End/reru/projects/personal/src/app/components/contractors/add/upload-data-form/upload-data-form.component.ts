import { Component } from '@angular/core';

@Component({
  selector: 'app-upload-data-form',
  templateUrl: './upload-data-form.component.html',
  styleUrls: ['./upload-data-form.component.scss']
})
export class UploadDataFormComponent {
  files: File[] = [];
  isLoading: boolean;
  constructor() { }

  onSelect(event) {
    this.files.push(...event.addedFiles);
	}

	onRemove(event) {
		this.files.splice(this.files.indexOf(event), 1);
	}

}
