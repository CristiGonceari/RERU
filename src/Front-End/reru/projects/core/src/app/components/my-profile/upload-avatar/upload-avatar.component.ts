import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProfileService } from '../../../utils/services/profile.service';

@Component({
  selector: 'app-upload-avatar',
  templateUrl: './upload-avatar.component.html',
  styleUrls: ['./upload-avatar.component.scss']
})
export class UploadAvatarComponent implements OnInit {
  files: File[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private profileService: ProfileService
  ) { }

  ngOnInit(): void {
  }

  uploadAvatar(): void {
    const formData = new FormData();
    formData.append('file', this.files[0]);
    this.profileService.uploadAvatar(formData).subscribe(
      () => {
        this.activeModal.close();
        window.location.reload();
      }
    )
  };

  onSelect(event) {
    this.files.push(...event.addedFiles);
  }
}
