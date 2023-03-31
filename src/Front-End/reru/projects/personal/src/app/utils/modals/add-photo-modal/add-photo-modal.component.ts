import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationsService } from 'angular2-notifications';
import { ContractorService } from '../../services/contractor.service';
import { NotificationUtil } from '../../util/notification.util';
import { I18nService } from '../../services/i18n.service';
import { forkJoin, Observable } from 'rxjs';


@Component({
  selector: 'app-add-photo-modal',
  templateUrl: './add-photo-modal.component.html',
  styleUrls: ['./add-photo-modal.component.scss']
})
export class AddPhotoModalComponent implements OnInit {

  @Input() contractorId: any;

  uploadForm: FormGroup;
  isLoading: boolean = true;
  fileId: string;
  fileType: string = null;
  attachedFile: File;
  
  constructor(
    private activeModal: NgbActiveModal,
    private contractorService: ContractorService,
    private notificationService: NotificationsService,
    private fb: FormBuilder,
    public translate: I18nService,
    ) { }
    
    ngOnInit(): void {
      this.initForm();
      this.isLoading = false;
      }
      
    initForm(): void {
      this.uploadForm = this.fb.group({
        file: this.fb.control(null, [Validators.required])
      })
    }
    
    dismiss(): void {
      this.activeModal.dismiss();
    }
    
    addPhoto() {
      const request = new FormData();
      
      if (this.attachedFile) {
        this.fileType = '7';
        request.append('Data.FileDto.File', this.attachedFile);
        request.append('Data.FileDto.Type', this.fileType);
      }
      request.append('Data.ContractorId', this.contractorId);
      request.append('Data.MediaFileId', this.uploadForm.value.mediaFileId);

      forkJoin([
        this.translate.get('modal.success'),
        this.translate.get('notification.body.success.picture')
      ]).subscribe(
        ([ success, picture ]) => {
          this.contractorService.editAvatar(request).subscribe(()=>
          this.notificationService.success(success, picture, NotificationUtil.getDefaultMidConfig()));
          this.activeModal.close(this.uploadForm.value);
        });
      }
      
      checkFile(event) {
        if (event != null) 
        this.attachedFile = event;
        else 
        this.fileId = null;
      }
}
